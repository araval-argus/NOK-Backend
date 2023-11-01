// <copyright file="CreateUserCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.User
{
    using WHO.NAPHS.BusinessLogic.ViewModels.Country;
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Command to create user.
    /// </summary>
    public class CreateUserCommand : Request
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
        /// Gets or sets <see cref="CountryDropdownViewModel"/> model.
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

        /// <summary>
        /// Gets or sets Graph token of the user to send an invitation mail.
        /// </summary>
        public string GraphToken { get; set; } = string.Empty;
    }
}
