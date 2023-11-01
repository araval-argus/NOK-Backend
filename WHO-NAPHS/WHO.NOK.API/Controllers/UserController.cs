// <copyright file="UserController.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8618

namespace WHO.NAPHS.API.Controllers
{
    using FluentValidation;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.API.Helper;
    using WHO.NAPHS.BusinessLogic.Features.User;
    using WHO.NAPHS.BusinessLogic.Helper;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.ViewModels.PaginatedResponses;
    using WHO.NAPHS.BusinessLogic.ViewModels.User;
    using WHO.NAPHS.BusinessLogic.ViewModels.UserClaims;
    using WHO.NAPHS.Core.Common.Resources;
    using WHO.NAPHS.Core.ResponseMiddleware;
    using WHO.NAPHS.Core.Wrappers;

    /// <summary>
    /// Controller for User Management.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IStringLocalizer<Resources> localizer;
        private readonly UserClaimsViewModel user;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IValidator<CreateUserCommand> createUserValidator;
        private readonly IValidator<UpdateUserDetailsCommand> updateUserDetailsValidator;
        private readonly IValidator<GetUserCommand> getUserCommandValidator;
        private readonly IValidator<ActivateUserCommand> activateUserCommandValidator;
        private readonly IValidator<DeactivateUserCommand> deactivateUserCommandValidator;
        private readonly IValidator<UploadUserProfilePicCommand> uploadProfilePicValidator;
        private readonly IValidator<GetProfilePictureCommand> profilePictureValidator;
        private readonly IValidator<UpdateMyProfileCommand> updateMyProfileValidator;
        private readonly IValidator<GetUserClaimsCommand> userClaimsValidator;
        private readonly IValidator<DeleteUserCommand> deleteUserValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService"><see cref="IUserService"/> service.</param>
        /// <param name="localizer">Localizer.</param>
        /// <param name="contextAccessor">HttpContextAccessor.</param>
        /// <param name="createUserValidator"> Validate <see cref="CreateUserCommand"/> command. </param>
        /// <param name="updateUserDetailsValidator"> Validate <see cref="UpdateUserDetailsCommand"/> command. </param>
        /// <param name="getUserCommandValidator"> Validate <see cref="GetUserCommand"/> command. </param>
        /// <param name="activateUserCommandValidator"> Validate <see cref="ActivateUserCommand"/> command. </param>
        /// <param name="deactivateUserCommandValidator"> Validate <see cref="DeactivateUserCommand"/> command. </param>
        /// <param name="uploadProfilePicValidator"> Validate <see cref="UploadUserProfilePicCommand"/> command. </param>
        /// <param name="profilePictureValidator"> Validate <see cref="GetProfilePictureCommand"/> command. </param>
        /// <param name="updateMyProfileValidator"> Validate <see cref="UpdateMyProfileCommand"/> command. </param>
        /// <param name="userClaimsValidator"> Validate <see cref="GetUserClaimsCommand"/> command. </param>
        /// <param name="searchUsersValidator"> Validate <see cref="SearchUsersCommand"/> command. </param>
        /// <param name="deleteUserValidator"> Validate <see cref="DeleteUserCommand"/> command. </param>
        public UserController(
            IUserService userService,
            IStringLocalizer<Resources> localizer,
            IHttpContextAccessor contextAccessor,
            IValidator<CreateUserCommand> createUserValidator,
            IValidator<UpdateUserDetailsCommand> updateUserDetailsValidator,
            IValidator<GetUserCommand> getUserCommandValidator,
            IValidator<ActivateUserCommand> activateUserCommandValidator,
            IValidator<DeactivateUserCommand> deactivateUserCommandValidator,
            IValidator<UploadUserProfilePicCommand> uploadProfilePicValidator,
            IValidator<GetProfilePictureCommand> profilePictureValidator,
            IValidator<UpdateMyProfileCommand> updateMyProfileValidator,
            IValidator<GetUserClaimsCommand> userClaimsValidator,
            IValidator<DeleteUserCommand> deleteUserValidator)
        {
            this.userService = userService;
            this.localizer = localizer;
            this.createUserValidator = createUserValidator;
            this.updateUserDetailsValidator = updateUserDetailsValidator;
            this.getUserCommandValidator = getUserCommandValidator;
            this.activateUserCommandValidator = activateUserCommandValidator;
            this.deactivateUserCommandValidator = deactivateUserCommandValidator;
            this.uploadProfilePicValidator = uploadProfilePicValidator;
            this.profilePictureValidator = profilePictureValidator;
            this.updateMyProfileValidator = updateMyProfileValidator;
            this.contextAccessor = contextAccessor;
            this.getUserCommandValidator = getUserCommandValidator;
            this.userClaimsValidator = userClaimsValidator;
            this.deleteUserValidator = deleteUserValidator;
            this.user = this.contextAccessor.HttpContext!.User!.GetUser() !;
        }

        /// <summary>
        /// Get all the users.
        /// </summary>
        /// <param name="request"><see cref="GetUsersCommand"/> command to get users.</param>
        /// <returns>Returns paginated list of <see cref="UserViewModel"/> model.</returns>
        // GET: User/GetUsers
        [HttpPost("GetUsers")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResponse<UserViewModel>>), 200)]
        public async Task<ActionResult<PaginatedResponse<UserViewModel>>> GetUsers([FromBody] GetUsersCommand request)
        {
            request.User = this.user;
            return this.Ok(await this.userService.GetAllPaginatedAndFilteredUsers(request));
        }

        /// <summary>
        /// Get all the users to be exported in front end.
        /// </summary>
        /// <returns>Returns list of <see cref="UserViewModel"/> model to be exported in front end.</returns>
        [HttpGet("GetExportUsers")]
        [ProducesResponseType(typeof(ApiResponse<List<UserViewModel>>), 200)]
        public async Task<ActionResult<PaginatedResponse<UserViewModel>>> GetExportUsers()
        {
            return this.Ok(await this.userService.GetExportUsersAsync(this.user.UserId));
        }

        /// <summary>
        /// Get a user.
        /// </summary>
        /// <param name="request">command to get user id the given user id.</param>
        /// <returns> Returns the <see cref="UserViewModel"/> object by the Id.</returns>
        // GET: User/{id}
        [HttpPost("GetUser")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), 200)]
        public async Task<ActionResult<UserViewModel>> GetUser([FromBody] GetUserCommand request)
        {
            request.User = this.user;
            ValidationResult result = await this.getUserCommandValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.userService.GetByIdAsync(request));
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="request"><see cref="CreateUserCommand"/> command request to create a new user.</param>
        /// <returns>Returns the newly created <see cref="UserViewModel"/> model.</returns>
        // POST: User
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), 200)]
        public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserCommand request)
        {
            request.User = this.user;
            request.GraphToken = this.HttpContext.Request.Headers["graphToken"];
            ValidationResult result = await this.createUserValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.userService.CreateUserAsync(request));
        }

        /// <summary>
        /// Update the details of the user.
        /// </summary>
        /// <param name="request"> <see cref="UpdateUserDetailsCommand"/> model to update user details.</param>
        /// <returns>Returns updated <see cref="UserViewModel"/> model.</returns>
        // PUT: User
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), 200)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDetailsCommand request)
        {
            request.User = this.user;
            ValidationResult result = await this.updateUserDetailsValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.userService.UpdateUserDetailsAsync(request));
        }

        /// <summary>
        /// Deactivates a user's profile.
        /// </summary>
        /// <param name="request"><see cref="DeactivateUserCommand"/> command request to deactivate a user.</param>
        /// <returns>Returns a boolean representing the result of the asynchronous operation.</returns>
        [HttpPut("DeactivateUser")]
        [ProducesResponseType(typeof(ApiResponse<int>), 200)]
        public async Task<IActionResult> DeactivateUser([FromBody] DeactivateUserCommand request)
        {
            request.User = this.user;
            var result = await this.deactivateUserCommandValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.userService.DeactivateUserAsync(request));
        }

        /// <summary>
        /// Deactivates a user's profile.
        /// </summary>
        /// <param name="request"><see cref="ActivateUserCommand"/> command request to activate a user.</param>
        /// <returns>Returns a boolean representing the result of the asynchronous operation.</returns>
        [HttpPut("ActivateUser")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<IActionResult> ActivateUser([FromBody] ActivateUserCommand request)
        {
            request.User = this.user;

            var result = await this.activateUserCommandValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.userService.ActivateUserAsync(request));
        }

        /// <summary>
        /// Get user information from the claims.
        /// </summary>
        /// <returns> Returns the <see cref="UserClaimsViewModel"/> model after removing some unnecessary props from the modal.</returns>
        [HttpGet("GetClaimsUserDetails")]
        [ProducesResponseType(typeof(ApiResponse<UserClaimsViewModel>), 200)]
        public async Task<IActionResult> GetClaimsUserDetails()
        {
            GetUserClaimsCommand request = new ()
            {
                User = this.user,
            };

            var result = await this.userClaimsValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.userService.GetUserFromClaimsAsync(this.user));
        }

        /// <summary>
        /// Upload user's profile picture.
        /// </summary>
        /// <param name="file"> <see cref="IFormFile"/> Command request to upload profile picture.</param>
        /// <returns> Returns the path of the profile picture.</returns>
        [HttpPost("UploadProfilePicture")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            UploadUserProfilePicCommand request = new ()
            {
                ProfilePicture = file,
                User = this.user,
            };

            var result = await this.uploadProfilePicValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.userService.UploadUserProfilePicAsync(request));
        }

        /// <summary>
        /// Upload user's profile picture from the user management.
        /// </summary>
        /// <param name="file"> <see cref="IFormFile"/> Command request to upload profile picture.</param>
        /// <returns> Returns the path of the profile picture.</returns>
        [HttpPost("UploadPictureForUser")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> UploadPictureForUser(IFormFile file)
        {
            UploadUserProfilePicCommand request = new ()
            {
                ProfilePicture = file,
                User = this.user,
            };

            var result = await this.uploadProfilePicValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.userService.UploadUserProfilePicForUserManagementAsync(request));
        }

        /// <summary>
        /// Get user profile image.
        /// </summary>
        /// <param name="imagePath"> Image path of the user image.</param>
        /// <returns> Returns the image of the user profile pic.</returns>
        [HttpGet("GetUserImage/{imagePath}")]
        public async Task<IActionResult> GetUserImage(string imagePath)
        {
            GetProfilePictureCommand request = new GetProfilePictureCommand()
            {
                ImagePath = imagePath,
                User = this.user,
            };

            var result = await this.profilePictureValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            var fileDetails = await this.userService.GetProfilePictureAsync(request);

            Dictionary<string, string> customHeaders = new Dictionary<string, string>();
            customHeaders.Add("content-disposition", "fileName=" + imagePath);
            return this.FileWithCustomHeaders(fileDetails.FileBytes, fileDetails.ContentType, customHeaders);
        }

        /// <summary>
        /// Get login user profile details.
        /// </summary>
        /// <returns> Returns the <see cref="UserViewModel"/> object by the Id.</returns>
        [HttpGet("GetMyProfileDetails")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), 200)]
        public async Task<ActionResult<UserViewModel>> GetMyProfileDetailsUser()
        {
            return this.Ok(await this.userService.GetByIdAsync(new GetUserCommand
            {
                UserId = this.user.UserId,
                User = this.user,
            }));
        }

        /// <summary>
        /// Update login user profile details.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <returns> Returns the <see cref="UserViewModel"/> object by the Id.</returns>
        [HttpPut("UpdateMyProfileDetails")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), 200)]
        public async Task<ActionResult<UserViewModel>> UpdateMyProfileDetailsUser(UpdateMyProfileCommand request)
        {
            if (request.UserId != this.user.UserId)
            {
                return this.Unauthorized();
            }

            request.User = this.user;
            ValidationResult result = await this.updateMyProfileValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.userService.UpdateMyProfileAsync(request));
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="request"> <see cref="DeleteUserCommand"/>Command request to delete a user.</param>
        /// <returns>Returns the <see cref="UserViewModel"/> object which has been deleted successfully.</returns>
        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), 200)]
        public async Task<IActionResult> DeleteUser(DeleteUserCommand request)
        {
            request.User = this.user;
            var result = await this.deleteUserValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.userService.DeleteUserAsync(request));
        }
    }
}