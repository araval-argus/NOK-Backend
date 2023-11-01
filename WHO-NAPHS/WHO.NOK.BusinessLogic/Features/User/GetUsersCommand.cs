// <copyright file="GetUsersCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.User
{
    using WHO.NAPHS.BusinessLogic.Helper;
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to get users.
    /// </summary>
    public class GetUsersCommand : Request
    {
        /// <summary>
        /// Gets or sets the term to be searched in the user details.
        /// </summary>
        public string SearchTerm { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets <see cref="PaginationFilter"/> for pagination.
        /// </summary>
        public PaginationFilter? PaginationFilter { get; set; }
    }
}