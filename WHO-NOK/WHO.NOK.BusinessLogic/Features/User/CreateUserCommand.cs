// <copyright file="ActivateUserCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.User
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Request command to activate a user.
    /// </summary>
    public class CreateUserCommand
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
        /// Gets or sets country id.
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets Access Reason.
        /// </summary>
        public string? AccessReason { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Languages"/> enum.
        /// </summary>
        public Languages PreferredLanguageId { get; set; }

        /// <summary>
        /// Gets or sets Jobstitle.
        /// </summary>
        public string? JobTitle { get; set; }

        /// <summary>
        /// Gets or sets institution name.
        /// </summary>
        public string? Institution { get; set; }

        /// <summary>
        /// Gets or sets affiliation name.
        /// </summary>
        public int? AffiliationId { get; set; }
        
        /// <summary>
        /// Gets or sets <see cref="ProfileStatus"/> enum.
        /// </summary>
        public ProfileStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a user has rights to only read.
        /// </summary>
        
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a user is active or not.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets Graph token of the user to send an invitation mail.
        /// </summary>
    }
}