// <copyright file="CurrentUserService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8602

namespace WHO.NAPHS.Infrastructure.ServiceImplementation
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;

    /// <summary>
    /// Implement the <see cref="ICurrentUserService"/>.
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUserService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor"> http accessor.</param>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public string UserId => this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}