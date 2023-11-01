// <copyright file="UserClaimsTransformation.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.API.Helper
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authentication;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.ViewModels.UserClaims;

    /// <summary>
    /// User claims transformation.
    /// </summary>
    public class UserClaimsTransformation : IClaimsTransformation
    {
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimsTransformation"/> class.
        /// </summary>
        /// <param name="userService"> <see cref="IUserService"/> service.</param>
        public UserClaimsTransformation(IUserService userService)
        {
            this.userService = userService;
        }

        /// <inheritdoc/>
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // Clone current identity
            var clone = principal.Clone();
            var newIdentity = clone.Identity as ClaimsIdentity;

            // Support AD and local accounts
            var email = principal.GetPreferredUsername();
            if (email == null)
            {
                return principal;
            }

            // Get user from database
            UserClaimsViewModel user = await this.userService.GetUserByEmailForClaimsAsync(email);
            if (user == null)
            {
                return principal;
            }

            newIdentity?.AddUserDetails(user);

            return clone;
        }
    }
}
