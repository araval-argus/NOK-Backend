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
    using WHO.NOKS.BusinessLogic.Features.User;

    /// <summary>
    /// Implement the <see cref="IUserService"/> interface.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        private readonly IStringLocalizer<Resources> localizer;
        private readonly IMapper mapper;

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
            IStringLocalizer<Resources> localizer)
        {
            this.context = context;
            this.mapper = mapper;
            this.localizer = localizer;
        }

        /// <inheritdoc/>
        public async Task<CurrentUserViewModel> CreateUserAsync(CreateUserCommand model)
        {
            // if (!IsUserHasAccessToPerformTheAction((Roles)model.User!.RoleId, model.RoleId))
            // {
            //     throw new Exception(this.localizer["CantPerformOperation"]);
            // }

            // if (this.context.Users.Any(e => e.Email.Trim().ToLower() == model.User.Email.Trim().ToLower()))
            // {
            //    throw new Exception(this.localizer["UserExists"]);
            // }

            User user = new User(
                model.RoleId,
                model.FirstName,
                model.LastName,
                model.Email,
                model.ProfilePicture,
                model.CountryId,
                model.PreferredLanguageId,
                model.Institution,
                model.AffiliationId,
                model.AccessReason,
                model.JobTitle,
                model.Status,
                model.IsActive,
                model.IsReadOnly
            );

            await this.context.AddAsync(user);
            await this.context.SaveChangesAsync();
            return this.mapper.Map<CurrentUserViewModel>(user);
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
                case Roles.SystemAdmin:
                    return true;

                default:
                    return false;
            }

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
            var user = await this.GetUserByIdAsync((int)request.UserId!);

            // if ( request.User!.RoleId != Roles.SystemAdmin){
            //     throw new Exception(this.localizer["CantPerformOperation"]);
            // }
            user.DeactivateUser();
            this.context.Update(user);
            return await this.context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> ActivateUserAsync(ActivateUserCommand request)
        {
            var user = await this.GetUserByIdAsync((int)request.UserId!);
            // if ( request.User!.RoleId != Roles.SystemAdmin){
            //     throw new Exception(this.localizer["CantPerformOperation"]);
            // }
            user.ActivateUser();
            this.context.Update(user);
            return await this.context.SaveChangesAsync() > 0;
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
                                .FirstOrDefaultAsync(x => x.UserId == userId) ??
                                throw new Exception(this.localizer["UserNotFound"]);
        }

        /// <inheritdoc/>
        public async Task<CurrentUserViewModel> UpdateUserDetailsAsync(UpdateUserDetailsCommand model)
        {
            var user = await this.GetUserByIdAsync(model.UserId);

            user.UpdateUserDetails(model.FirstName, model.LastName, model.PreferredLanguageId, model.Institution, model.AffiliationId);

            // if (model.User!.RoleId == Roles.SystemAdmin)
            // {
            //     user.ChangeRole(model.RoleId);
            // }

            this.context.Update(user);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<CurrentUserViewModel>(user);
        }

        /// <inheritdoc/>
        public async Task<CurrentUserViewModel> DeleteUserAsync(DeleteUserCommand request)
        {
           var user = this.context.Users.Include(u => u.UserRole).FirstOrDefault(x => x.UserId == request.UserId) ??
                        throw new Exception(this.localizer["UserNotFound"]);

            // if (!IsUserHasAccessToPerformTheAction((Roles)request.User!.RoleId, request.RoleId))
            // {
            //     throw new Exception(this.localizer["CantPerformOperation"]);
            // }

            user.DeleteUser();
            await this.context.SaveChangesAsync();
            return this.mapper.Map<CurrentUserViewModel>(user);

        }

        /// <inheritdoc/>
        public async Task<PaginatedResponse<CurrentUserViewModel>> GetAllPaginatedAndFilteredUsers(GetUsersCommand request)
        {
            var users = await this.GetAllUsersAsync((int)request.UserId);

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.ToLower().Trim();
                users = await Task.FromResult(
                             users.Where(x =>
                                        string.Concat(x.FirstName, " ", x.LastName).ToLower().Contains(searchTerm) ||
                                        x.Email.ToLower().Contains(searchTerm) ||
                                        (x.Institution != null && x.Institution.ToLower().Contains(searchTerm)) ||
                                        x.RoleId.ToString().ToLower().Contains(searchTerm))
                             .ToList());
            }

            // No Filter parameter is used currently
            List<FilterParams<CurrentUserViewModel>> filterParamsList = new ();

            PaginatedResponse<CurrentUserViewModel> response = await users.SortAndPaginateAsync(request.PaginationFilter, filterParamsList);

            return response;
        }

        /// <summary>
        /// Fetches all the users according to the role of the logged in user.
        /// </summary>
        /// <param name="loggedInUserId">user id of logged in user.</param>
        /// <returns>Returns list of <see cref="UserViewModel"/> model according to the role of the logged in user.</returns>
        private async Task<List<CurrentUserViewModel>> GetAllUsersAsync(int loggedInUserId)
        {
            var loggedInUser = await this.GetUserByIdAsync(loggedInUserId);
            var users = this.context.Users.AsNoTracking().Include(x => x.UserRole).Include(x => x.Country).ToList();
            // var users = loggedInUser.UserRole.RoleId switch
            // {
            //     Roles.SystemAdmin => this.context.Users.AsNoTracking().Include(x => x.UserRole).Include(x => x.Country).ToList(),

            //     _ => throw new Exception(this.localizer["CantPerformOperation"]),
            // };

            return this.mapper.Map<List<CurrentUserViewModel>>(users);
        }

    }
}