// <copyright file="UserViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.User
{
    using WHO.NOK.BusinessLogic.ViewModels.Country;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// View model for the User.
    /// </summary>
    public class CurrentUserViewModel
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        ///  Gets or sets first name.
        /// </summary>
        public string FirstName { get; protected set; } = default!;

        /// <summary>
        ///  Gets or sets last name.
        /// </summary>
        public string LastName { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets Email.
        /// </summary>
        public string Email { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets Jobstitle.
        /// </summary>
        public string? JobTitle { get; protected set; }

        /// <summary>
        /// Gets or sets institution name.
        /// </summary>
        public string? Institution { get; protected set; }

        /// <summary>
        /// Gets or sets affiliation Id.
        /// </summary>
        public int? AffiliationId { get; protected set; }

        /// <summary>
        /// Gets or sets Access Reason.
        /// </summary>
        public string? AccessReason { get; protected set; }

        /// <summary>
        /// Gets or sets Id of country.
        /// </summary>
        public string? Country { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets <see cref="Roles"/> enum.
        /// </summary>
        public Roles RoleId { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="ProfileStatus"/> enum.
        /// </summary>
        public ProfileStatus? Status { get; protected set; }
    }
}