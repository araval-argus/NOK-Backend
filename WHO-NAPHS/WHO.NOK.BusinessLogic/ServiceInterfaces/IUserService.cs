// <copyright file="IUserService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ServiceInterfaces
{
    using WHO.NOK.BusinessLogic.Features.User;
    using WHO.NOK.BusinessLogic.ViewModels.PaginatedResponses;
    using WHO.NOK.BusinessLogic.ViewModels.User;
    using WHO.NOK.BusinessLogic.ViewModels.UserClaims;

    /// <summary>
    /// Interface definitions for the User.
    /// </summary>
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// Get By Id.
        /// </summary>
        /// <param name="model"> Command to get the user of given user id.</param>
        /// <returns> Returns the <see cref="UserViewModel"/> object by the Id.</returns>
        Task<UserViewModel> GetByIdAsync(GetUserCommand model);

        /// <summary>
        /// Update the details of the user.
        /// </summary>
        /// <param name="model"> <see cref="UpdateUserDetailsCommand"/> model to update user details.</param>
        /// <returns>Returns updated <see cref="UserViewModel"/> model.</returns>
        Task<UserViewModel> UpdateUserDetailsAsync(UpdateUserDetailsCommand model);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="model"><see cref="CreateUserCommand"/> command request to create a new user.</param>
        /// <returns>Returns the newly created <see cref="UserViewModel"/> model.</returns>
        Task<UserViewModel> CreateUserAsync(CreateUserCommand model);

        /// <summary>
        /// Finds a user having the email Id.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>Returns a <see cref="UserViewModel"/> object.</returns>
        Task<UserViewModel> GetUserByEmailAsync(string email);

        /// <summary>
        /// Get user by email to fill the user claims.
        /// </summary>
        /// <param name="email"> Email address.</param>
        /// <returns>Returns <see cref="UserClaimsViewModel"/> model.</returns>
        Task<UserClaimsViewModel> GetUserByEmailForClaimsAsync(string email);

        /// <summary>
        /// Deactivates user.
        /// </summary>
        /// <param name="request">Command to deactivate a user.</param>
        /// <returns>Returns a boolean representing the result of the asynchronous operation.</returns>
        Task<bool> DeactivateUserAsync(DeactivateUserCommand request);

        /// <summary>
        /// Activates user.
        /// </summary>
        /// <param name="request">Command to activate a user.</param>
        /// <returns>Returns a boolean representing the result of the asynchronous operation.</returns>
        Task<bool> ActivateUserAsync(ActivateUserCommand request);

        /// <summary>
        /// Fetches the paginated and filtered list of users.
        /// </summary>
        /// <param name="request"><see cref="GetUsersCommand"/> command to get users.</param>
        /// <returns>Returns paginated list of <see cref="UserViewModel"/> model.</returns>
        Task<PaginatedResponse<UserViewModel>> GetAllPaginatedAndFilteredUsers(GetUsersCommand request);

        /// <summary>
        /// Fetches the paginated and filtered list of users.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Returns list of <see cref="UserViewModel"/> model to be exported in front end.</returns>
        Task<List<UserViewModel>> GetExportUsersAsync(int userId);

        /// <summary>
        /// Get users from the claims.
        /// </summary>
        /// <param name="user"> <see cref="UserClaimsViewModel"/> model to get claims.</param>
        /// <returns> Returns the <see cref="UserClaimsViewModel"/> model after removing some unnecessary props from the modal.</returns>
        Task<UserClaimsViewModel> GetUserFromClaimsAsync(UserClaimsViewModel user);

        /// <summary>
        /// Upload the user's profile picture.
        /// </summary>
        /// <param name="request"> <see cref="UploadUserProfilePicCommand"/> command to upload  the user's profile pic.</param>
        /// <returns> Returns the path of the profile picture.</returns>
        Task<string?> UploadUserProfilePicAsync(UploadUserProfilePicCommand request);

        /// <summary>
        /// Get profile picture for the user.
        /// </summary>
        /// <param name="request"> <see cref="GetProfilePictureCommand"/> command to get the profile image. </param>
        /// <returns> Returns the user profile picture file.</returns>
        Task<ProfilePictureViewModel> GetProfilePictureAsync(GetProfilePictureCommand request);

        /// <summary>
        /// Update the details of the user.
        /// </summary>
        /// <param name="model"> <see cref="UpdateMyProfileCommand"/> model to update user details.</param>
        /// <returns>Returns updated <see cref="UserViewModel"/> model.</returns>
        Task<UserViewModel> UpdateMyProfileAsync(UpdateMyProfileCommand model);

        /// <summary>
        /// Upload the user's profile picture for user management.
        /// </summary>
        /// <param name="request"> <see cref="UploadUserProfilePicCommand"/> command to upload  the user's profile pic.</param>
        /// <returns> Returns the path of the profile picture.</returns>
        Task<string?> UploadUserProfilePicForUserManagementAsync(UploadUserProfilePicCommand request);

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="request"> <see cref="DeleteUserCommand"/> command to delete a user.</param>
        /// <returns> Returns the <see cref="UserViewModel"/> object which has been deleted successfully.</returns>
        Task<UserViewModel> DeleteUserAsync(DeleteUserCommand request);
    }
}