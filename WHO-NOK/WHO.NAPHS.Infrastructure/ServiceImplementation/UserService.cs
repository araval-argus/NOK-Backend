// <copyright file="UserService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8600,CS8604

namespace WHO.NOK.Infrastructure.ServiceImplementation
{
    using System.Collections.Generic;
    using System.Data;
    using AutoMapper;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.Features.User;
    using WHO.NOK.BusinessLogic.Helper;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.BusinessLogic.ViewModels.PaginatedResponses;
    using WHO.NOK.BusinessLogic.ViewModels.User;
    using WHO.NOK.BusinessLogic.ViewModels.UserClaims;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Common.Resources;
    using WHO.NOK.Core.Constant;
    using WHO.NOK.Core.Wrappers;
    using WHO.NOK.Infrastructure.Helper;
    using WHO.NOK.Infrastructure.Models.DatabaseContext;
    using WHO.NOK.Infrastructure.Models.Users;

    /// <summary>
    /// Implement the <see cref="IUserService"/> interface.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        private readonly IStringLocalizer<Resources> localizer;
        private readonly IMapper mapper;
        private readonly ICommonService commonService;
        private readonly IEmailService emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="context"> <see cref="ApplicationDbContext"/> Database context.</param>
        /// <param name="mapper"> <see cref="IMapper"/> Auto mapper.</param>
        /// <param name="localizer"> <see cref="IStringLocalizer"/> Localizer.</param>
        /// <param name="commonService"> <see cref="ICommonService"/> Common service. </param>
        /// <param name="emailService"> <see cref="IEmailService"/> Email service. </param>
        public UserService(
            ApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<Resources> localizer,
            ICommonService commonService,
            IEmailService emailService)
        {
            this.context = context;
            this.mapper = mapper;
            this.localizer = localizer;
            this.commonService = commonService;
            this.emailService = emailService;
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> GetByIdAsync(GetUserCommand request)
        {
            var user = await this.GetUserByIdAsync(request.UserId);
            return this.mapper.Map<UserViewModel>(user);
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> UpdateUserDetailsAsync(UpdateUserDetailsCommand model)
        {
            var user = await this.GetUserByIdAsync(model.UserId);

            user.UpdateUserDetails(model.FirstName, model.LastName, model.PreferredLanguageId, model.Institution, model.Affiliation);

            if (model.User!.RoleId == Roles.SystemAdmin.GetHashCode())
            {
                user.ChangeRole(model.RoleId);
            }

            this.context.Update(user);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<UserViewModel>(user);
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> GetUserByEmailAsync(string email)
        {
            var user = await this.context.Users.AsNoTracking()
                                            .Include(x => x.UserRole)
                                            .Include(x => x.Country)
                                            .FirstOrDefaultAsync(x =>
                                                        x.Email.ToLower().Trim() == email.ToLower().Trim() &&
                                                        !x.IsDeleted);

            return this.mapper.Map<UserViewModel>(user);
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> CreateUserAsync(CreateUserCommand model)
        {
            if (!IsUserHasAccessToPerformTheAction((Roles)model.User!.RoleId, model.RoleId))
            {
                throw new Exception(this.localizer["CantPerformOperation"]);
            }

            if (this.context.Users.Any(x => x.Email.Trim().ToLower() == model.Email.Trim().ToLower()))
            {
                throw new Exception(this.localizer["UserExists"]);
            }

            User user;

            if ((Roles)model.User!.RoleId == Roles.CountryAdmin)
            {
                user = new (model.RoleId, model.FirstName, model.LastName, model.Email, model.ProfilePicture, model.Country!.CountryId, model.Region,
                            model.PreferredLanguageId, model.Institution, model.Affiliation);
                user.ChangeStatus(ProfileStatus.PendingFromAdmin);
                user.ActivateUser();

                // TODO: Send an email to the regional and system admin for the approval of this user.
            }
            else
            {
                user = new (model.RoleId, model.FirstName, model.LastName, model.Email, model.ProfilePicture, model.Country?.CountryId, model.Region,
                            model.PreferredLanguageId, model.Institution, model.Affiliation);
                user.ChangeStatus(ProfileStatus.PendingFromUser);
                user.ActivateUser();

                await this.emailService.SendMicrosoftInvitationMail(model.Email, model.GraphToken);
            }

            var result = await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<UserViewModel>(result.Entity);
        }

        /// <inheritdoc/>
        public async Task<UserClaimsViewModel> GetUserByEmailForClaimsAsync(string email)
        {
            UserClaimsViewModel? model = new ();

            List<SqlParameter> parameters = new ()
            {
                new () { ParameterName = "@UserEmail", Value = email },
            };

            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_GetUserClaimsDetails",
                (ds) =>
                {
                    model = ds.Tables[0].ConvertToList<UserClaimsViewModel>().FirstOrDefault();
                },
                parameters,
                1800);

            return model;
        }

        /// <inheritdoc/>
        public async Task<bool> DeactivateUserAsync(DeactivateUserCommand request)
        {
            var userToBeDeactivated = await this.GetUserByIdAsync(request.UserId);

            if (request.User!.RoleId == (int)Roles.SystemAdmin && request.User.UserId != request.UserId)
            {
                userToBeDeactivated.DeactivateUser();
                this.context.Update(userToBeDeactivated);
                return await this.context.SaveChangesAsync() > 0;
            }
            else if (userToBeDeactivated.UserId == request.User.UserId)
            {
                userToBeDeactivated.EnableDeactivationRequest();
                this.context.Update(userToBeDeactivated);
                return await this.context.SaveChangesAsync() > 0;

                // TODO: send a mail to the system admin to deactivate this user.
            }

            throw new Exception(this.localizer["CantPerformOperation"]);
        }

        /// <inheritdoc/>
        public async Task<bool> ActivateUserAsync(ActivateUserCommand request)
        {
            var user = await this.GetUserByIdAsync(request.UserId);

            if (request.User!.RoleId == (int)Roles.SystemAdmin)
            {
                user.ActivateUser();
                this.context.Update(user);
                return await this.context.SaveChangesAsync() > 0;
            }

            throw new Exception(this.localizer["CantPerformOperation"]);
        }

        /// <inheritdoc/>
        public async Task<PaginatedResponse<UserViewModel>> GetAllPaginatedAndFilteredUsers(GetUsersCommand request)
        {
            var users = await this.GetAllUsersAsync(request.User!.UserId);

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.ToLower().Trim();
                users = await Task.FromResult(
                             users.Where(x =>
                                        string.Concat(x.FirstName, " ", x.LastName).ToLower().Contains(searchTerm) ||
                                        x.Email.ToLower().Contains(searchTerm))
                             .ToList());
            }

            List<FilterParams<UserViewModel>> filterParamsList = new ();

            foreach (KeyValuePair<string, HashSet<string>> item in request.PaginationFilter!.ColumnsAndValuesForFilter)
            {
                FilterParams<UserViewModel> filterParams = new ()
                {
                    Column = item.Key,
                    Values = item.Value,
                };

                switch (item.Key)
                {
                    case "Status":
                        foreach (string statusValue in item.Value)
                        {
                            switch (statusValue)
                            {
                                case "Active":
                                    filterParams.FilterFunc.Add(user => user.Status == null && user.IsActive);
                                    break;

                                case "Inactive":
                                    filterParams.FilterFunc.Add(user => user.Status == null && !user.IsActive);
                                    break;

                                case "Pending":
                                    filterParams.FilterFunc.Add(user => user.Status == ProfileStatus.PendingFromAdmin || user.Status == ProfileStatus.PendingFromUser);
                                    break;

                                case "Rejected":
                                    filterParams.FilterFunc.Add(user => user.Status == ProfileStatus.RejectedByAdmin || user.Status == ProfileStatus.RejectedByUser);
                                    break;

                                default:
                                    throw new Exception(this.localizer["InvalidValue"]);
                            }
                        }

                        break;
                    case "FirstName":
                        foreach (var firstName in item.Value)
                        {
                            filterParams.FilterFunc.Add(user => user.FirstName.ToLower().Trim().Contains(firstName.ToLower().Trim()) || user.FirstName.ToLower().Trim().Contains(firstName.ToLower().Trim()));
                        }

                        break;
                    case "LastName":
                        foreach (var lastName in item.Value)
                        {
                            filterParams.FilterFunc.Add(user => user.LastName.ToLower().Trim().Contains(lastName.ToLower().Trim()) || user.FirstName.ToLower().Trim().Contains(lastName.ToLower().Trim()));
                        }

                        break;
                    case "Email":
                        foreach (var email in item.Value)
                        {
                            filterParams.FilterFunc.Add(user => user.Email.Contains(email));
                        }

                        break;
                    default:
                        break;
                }

                filterParamsList.Add(filterParams);
            }

            PaginatedResponse<UserViewModel> response = await users.SortAndPaginateAsync(request.PaginationFilter, filterParamsList);

            return response;
        }

        /// <inheritdoc/>
        public async Task<List<UserViewModel>> GetExportUsersAsync(int userId)
        {
            return await this.GetAllUsersAsync(userId);
        }

        /// <inheritdoc/>
        public async Task<UserClaimsViewModel> GetUserFromClaimsAsync(UserClaimsViewModel user)
        {
            UserClaimsViewModel model = user;
            int? countryId = user.CountryId;
            string region = user.Region;
            string countryName = user.CountryName;

            model.UserId = 0;
            model.Region = null;

            if (model.RoleId == (int)Roles.SystemAdmin || model.RoleId == (int)Roles.RegionalAdmin)
            {
                model.CountryId = countryId;
                model.CountryName = countryName;

                if (model.RoleId == (int)Roles.RegionalAdmin)
                {
                    model.Region = region;
                }
            }

            return await Task.FromResult(model);
        }

        /// <inheritdoc/>
        public async Task<string?> UploadUserProfilePicAsync(UploadUserProfilePicCommand request)
        {
            User user = await this.context.Users
                .FirstOrDefaultAsync(x => x.UserId == request.User!.UserId && !x.IsDeleted) ??
                throw new ApiException(this.localizer["NoProfileFound"]);

            var file = request.ProfilePicture;
            string folderPath = CommonSettings.ApplicationSettings!.ProfilePicturePath;

            string fileName = $"{DateTime.Now.Ticks}_{await this.commonService.GenerateRandomStringAsync(5)}";
            var extension = Path.GetExtension(file.FileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullFileName = $"{fileName + extension}";
            var filePath = $"{folderPath}/{fullFileName}";
            using (var fileStreams = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }

            user.UpdateProfilePicture(fullFileName);
            await this.context.SaveChangesAsync();

            return user.ProfilePicture;
        }

        /// <inheritdoc/>
        public async Task<ProfilePictureViewModel> GetProfilePictureAsync(GetProfilePictureCommand request)
        {
            ProfilePictureViewModel model = new ();
            var filePath = $"{CommonSettings.ApplicationSettings.ProfilePicturePath}/{request.ImagePath}";

            byte[] fileBytes = null;

            if (File.Exists(filePath))
            {
                fileBytes = await File.ReadAllBytesAsync(filePath);

                string contentType = new FileExtensionContentTypeProvider().Mappings
                        .FirstOrDefault(mapping => mapping.Key.Equals(Path.GetExtension(filePath), StringComparison.OrdinalIgnoreCase))
                        .Value;

                model.FileBytes = fileBytes;
                model.ContentType = contentType;
            }
            else
            {
                throw new ApiException($"No image found with file name: {request.ImagePath}");
            }

            return model;
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> UpdateMyProfileAsync(UpdateMyProfileCommand model)
        {
            var user = await this.GetUserByIdAsync(model.UserId);

            user.UpdateUserDetails(
                model.FirstName,
                model.LastName,
                model.PreferredLanguageId,
                model.Institution,
                model.Affiliation);

            this.context.Update(user);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<UserViewModel>(user);
        }

        /// <inheritdoc/>
        public async Task<string?> UploadUserProfilePicForUserManagementAsync(UploadUserProfilePicCommand request)
        {
            var file = request.ProfilePicture;
            string folderPath = CommonSettings.ApplicationSettings!.ProfilePicturePath;

            string fileName = $"{DateTime.Now.Ticks}_{await this.commonService.GenerateRandomStringAsync(5)}";
            var extension = Path.GetExtension(file.FileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullFileName = $"{fileName + extension}";
            var filePath = $"{folderPath}/{fullFileName}";
            using (var fileStreams = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }

            return filePath;
        }

        /// <inheritdoc/>
        public async Task<UserViewModel> DeleteUserAsync(DeleteUserCommand request)
        {
            var user = this.context.Users.FirstOrDefault(x => x.UserId == request.UserId && !x.IsDeleted) ??
                        throw new Exception(this.localizer["UserNotFound"]);

            user.DeleteUser();
            await this.context.SaveChangesAsync();
            return this.mapper.Map<UserViewModel>(user);
        }

        /// <summary>
        /// Checks whether logged in user has access to perform the action.
        /// </summary>
        /// <param name="loggedInUserRole"> Currently logged in user's <see cref="Roles"/> value.</param>
        /// <param name="role"><see cref="Roles"/> enum.</param>
        /// <returns>Returns a value indicating whether the user has access or not.</returns>
        private static bool IsUserHasAccessToPerformTheAction(Roles loggedInUserRole, Roles role)
        {
            switch (loggedInUserRole)
            {
                case Roles.CountryAdmin:

                    if (role == Roles.SystemAdmin ||
                        role == Roles.RegionalAdmin ||
                        role == Roles.GlobalViewer)
                    {
                        return false;
                    }

                    break;

                case Roles.RegionalAdmin:

                    if (role == Roles.SystemAdmin)
                    {
                        return false;
                    }

                    break;

                case Roles.SystemAdmin:
                    return true;

                default:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Fetches the user from the database.
        /// </summary>
        /// <param name="userId">Id of the user to be fetched.</param>
        /// <returns>Returns <see cref="User"/> by id from the database.</returns>
        private async Task<User> GetUserByIdAsync(int userId)
        {
            return await this.context.Users.AsNoTracking()
                                .Include(x => x.UserRole)
                                .Include(x => x.Country)
                                .FirstOrDefaultAsync(x => x.UserId == userId && !x.IsDeleted) ??
                                throw new Exception(this.localizer["UserNotFound"]);
        }

        /// <summary>
        /// Fetches all the users according to the role of the logged in user.
        /// </summary>
        /// <param name="loggedInUserId">user id of logged in user.</param>
        /// <returns>Returns list of <see cref="UserViewModel"/> model according to the role of the logged in user.</returns>
        private async Task<List<UserViewModel>> GetAllUsersAsync(int loggedInUserId)
        {
            var loggedInUser = await this.GetUserByIdAsync(loggedInUserId);

            var users = loggedInUser.UserRole.RoleId switch
            {
                Roles.SystemAdmin => this.context.Users.AsNoTracking().Include(x => x.UserRole).Include(x => x.Country)
                                                        .Where(x => !x.IsDeleted)
                                                        .ToList(),
                Roles.RegionalAdmin => this.context.Users.AsNoTracking().Include(x => x.UserRole).Include(x => x.Country)
                                                        .Where(x => x.UserRole.RoleId != Roles.SystemAdmin &&
                                                                    (x.Region == loggedInUser.Region ||
                                                                    (x.Country != null &&
                                                                     x.Country.Region == loggedInUser.Region)) &&
                                                                    !x.IsDeleted)
                                                        .ToList(),
                Roles.CountryAdmin => this.context.Users.AsNoTracking().Include(x => x.UserRole).Include(x => x.Country)
                                                        .Where(x => x.UserRole.RoleId != Roles.SystemAdmin &&
                                                                    x.UserRole.RoleId != Roles.RegionalAdmin &&
                                                                    x.CountryId == loggedInUser.CountryId &&
                                                                    !x.IsDeleted)
                                                        .ToList(),
                _ => throw new Exception(this.localizer["CantPerformOperation"]),
            };

            return this.mapper.Map<List<UserViewModel>>(users);
        }
    }
}