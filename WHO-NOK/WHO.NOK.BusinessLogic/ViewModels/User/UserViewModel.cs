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
    public class UserViewModel
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Roles"/> enum.
        /// </summary>
        public Roles RoleId { get; set; }

        /// <summary>
        /// Gets or sets first name.
        /// </summary>
        public string FirstName { get; set; } = default!;

        /// <summary>
        ///  Gets or Sets last name.
        /// </summary>
        public string LastName { get; set; } = default!;

        /// <summary>
        /// Gets or sets Email.
        /// </summary>
        public string Email { get; set; } = default!;

        /// <summary>
        /// Gets or sets the url of the profile photo.
        /// </summary>
        public string? ProfilePicture { get; set; }

        /// <summary>
        /// Gets or sets <see cref="CountryDropdownViewModel"/> object.
        /// </summary>
        public CountryDropdownViewModel? Country { get; set; }

        /// <summary>
        /// Gets or sets region.
        /// </summary>
        public string? Region { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Languages"/> enum.
        /// </summary>
        public Languages PreferredLanguageId { get; set; }

        /// <summary>
        /// Gets or sets institution name.
        /// </summary>
        public string? Institution { get; set; }

        /// <summary>
        /// Gets or sets affiliation name.
        /// </summary>
        public string? Affiliation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a user has rights to only read.
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets <see cref="ProfileStatus"/> enum.
        /// </summary>
        public ProfileStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a user is active or not.
        /// </summary>
        public bool IsActive { get; set; }
    }
}