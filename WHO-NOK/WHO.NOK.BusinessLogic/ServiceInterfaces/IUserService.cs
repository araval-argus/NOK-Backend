// <copyright file="IUserService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ServiceInterfaces
{
    using WHO.NOK.BusinessLogic.ViewModels.PaginatedResponses;
    using WHO.NOK.BusinessLogic.ViewModels.User;
    using WHO.NOK.BusinessLogic.ViewModels.UserClaims;

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
    }
}