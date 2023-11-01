// <copyright file="UserClaimsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.UserClaims
{
    /// <summary>
    /// User claims view model.
    /// </summary>
    public class UserClaimsViewModel
    {
        /// <summary>
        /// Gets or sets CountryId.
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user is active or not.
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// Gets or sets RoleId.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets PrimaryEmail.
        /// </summary>
        public string PrimaryEmail { get; set; } = default!;

        /// <summary>
        /// Gets or sets a value indicating whether user has readonly right.
        /// </summary>
        public bool? ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets LanguageId.
        /// </summary>
        public int? LanguageId { get; set; }

        /// <summary>
        /// Gets or sets first name.
        /// </summary>
        public string FirstName { get; set; } = default!;

        /// <summary>
        /// Gets or sets Last name.
        /// </summary>
        public string LastName { get; set; } = default!;

        /// <summary>
        /// Gets or sets display name.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the region of the user if he is the regional admin.
        /// </summary>
        public string? Region { get; set; }

        /// <summary>
        /// Gets or sets country name.
        /// </summary>
        public string? CountryName { get; set; }

        /// <summary>
        /// Gets or sets profile picture.
        /// </summary>
        public string? ProfilePicture { get; set; }

        /// <summary>
        /// Gets or sets currency.
        /// </summary>
        public string? Currency { get; set; }
    }
}