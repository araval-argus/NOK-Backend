// <copyright file="UpdateUserDetailsCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.User
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Command for update user details.
    /// </summary>
    public class UpdateUserDetailsCommand : Request
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets Role Id.
        /// </summary>
        public Roles RoleId { get; set; }

        /// <summary>
        /// Gets or sets first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        ///  Gets or Sets last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets institution name.
        /// </summary>
        public string? Institution { get; set; }

        /// <summary>
        /// Gets or sets affiliation name.
        /// </summary>
        public string? Affiliation { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Languages"/> enum.
        /// </summary>
        public Languages PreferredLanguageId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a user is active or not.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
