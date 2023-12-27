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
    using WHO.NOKS.BusinessLogic.Features.User;

    /// <summary>
    /// Interface definitions for the User.
    /// </summary>
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// Get user by email to fill the user claims.
        /// </summary>
        /// <param name="email"> Email address.</param>
        /// <returns>Returns <see cref="UserClaimsViewModel"/> model.</returns>
        Task<UserClaimsViewModel> GetUserByEmailForClaimsAsync(string email);
        
        /// <summary>
        /// Get user by email to fill the user claims.
        /// </summary>
        /// <param name="model"> UserViewModel.</param>
        /// <returns>Returns <see cref="UserViewModel"/> model.</returns>
        Task<CurrentUserViewModel> CreateUserAsync(CreateUserCommand model);

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
        /// Update the details of the user.
        /// </summary>
        /// <param name="model"> <see cref="UpdateUserDetailsCommand"/> model to update user details.</param>
        /// <returns>Returns updated <see cref="UserViewModel"/> model.</returns>
        Task<CurrentUserViewModel> UpdateUserDetailsAsync(UpdateUserDetailsCommand model);

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="request"> <see cref="DeleteUserCommand"/> command to delete a user.</param>
        /// <returns> Returns the <see cref="CurrentUserViewModel"/> object which has been deleted successfully.</returns>
        Task<CurrentUserViewModel> DeleteUserAsync(DeleteUserCommand request);

        /// <summary>
        /// Fetches the paginated and filtered list of users.
        /// </summary>
        /// <param name="request"><see cref="GetUsersCommand"/> command to get users.</param>
        /// <returns>Returns paginated list of <see cref="CurrentUserViewModel"/> model.</returns>
        Task<PaginatedResponse<CurrentUserViewModel>> GetAllPaginatedAndFilteredUsers(GetUsersCommand request);
    }

}