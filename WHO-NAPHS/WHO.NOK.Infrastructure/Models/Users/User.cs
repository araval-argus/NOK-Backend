// <copyright file="User.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Infrastructure.Models.Countries;

    /// <summary>
    /// User Database model.
    /// </summary>
    public class User : ITrackable, IDeletable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User() => this.Email = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="roleId"><see cref="Roles"/> enum.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="email">Email.</param>
        /// <param name="profilePicture">Profile picture.</param>
        /// <param name="countryId">Country id.</param>
        /// <param name="region">Region.</param>
        /// <param name="languageId">Language Id.</param>
        /// <param name="institution">Institution.</param>
        /// <param name="affiliation">Affiliation.</param>
        /// <param name="profileStatus">ProfileStatus.</param>
        /// <param name="isReadOnly">IsReadOnly.</param>
        public User(
            Roles roleId,
            string firstName,
            string lastName,
            string email,
            string? profilePicture,
            int? countryId = null,
            string? region = null,
            Languages languageId = Languages.English,
            string? institution = null,
            string? affiliation = null,
            ProfileStatus? profileStatus = null,
            bool isReadOnly = true)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.ProfilePicture = profilePicture;
            if (roleId == Roles.SystemAdmin || roleId == Roles.GlobalViewer)
            {
                this.CountryId = null;
                this.Region = null;
            }
            else if (roleId == Roles.RegionalAdmin)
            {
                this.CountryId = null;
                this.Region = region;
            }
            else
            {
                this.CountryId = countryId;
                this.Region = null;
            }

            this.PreferredLanguageId = languageId;
            this.Institution = institution;
            this.Affiliation = affiliation;
            this.Status = profileStatus;
            this.UserRole = new UserRole(roleId);
            this.IsReadOnly = isReadOnly;
        }

        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        [Key]
        public int UserId { get; protected set; }

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
        /// Gets or sets the url of the profile photo.
        /// </summary>
        public string? ProfilePicture { get; protected set; }

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int? CountryId { get; protected set; }

        /// <summary>
        /// Gets or sets the Region.
        /// </summary>
        public string? Region { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="Languages"/> enum.
        /// </summary>
        public Languages PreferredLanguageId { get; protected set; }

        /// <summary>
        /// Gets or sets institution name.
        /// </summary>
        public string? Institution { get; protected set; }

        /// <summary>
        /// Gets or sets affiliation name.
        /// </summary>
        public string? Affiliation { get; protected set; }

        /// <inheritdoc/>
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc/>
        public int CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime LastUpdatedAt { get; set; }

        /// <inheritdoc/>
        public int LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether request is being made to deactivate account.
        /// </summary>
        public bool DeactivationRequest { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether a user has rights to only read.
        /// </summary>
        public bool IsReadOnly { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether user's profile is deleted or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets <see cref="ProfileStatus"/> enum.
        /// </summary>
        public ProfileStatus? Status { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether user's profile is active or not.
        /// </summary>
        public bool IsActive { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="UserRole"/> of the user.
        /// </summary>
        public virtual UserRole UserRole { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets the <see cref="Country"/> of the user.
        /// </summary>
        public virtual Country Country { get; protected set; } = default!;

        /// <summary>
        /// Update User Details.
        /// </summary>
        /// <param name="firstName">First name of the user.</param>
        /// <param name="lastName">Last name of the user.</param>
        /// <param name="languageId"><see cref="Languages"/> enum.</param>
        /// <param name="institution">Institution.</param>
        /// <param name="affiliation">Affiliation.</param>
        public void UpdateUserDetails(
            string firstName,
            string lastName,
            Languages languageId = Languages.English,
            string? institution = null,
            string? affiliation = null)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PreferredLanguageId = languageId;
            this.Institution = institution;
            this.Affiliation = affiliation;
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        public void DeleteUser()
        {
            this.Status = null;
            this.IsActive = false;
            this.IsDeleted = true;
        }

        /// <summary>
        /// Activates the user.
        /// </summary>
        public void ActivateUser()
        {
            this.IsActive = true;
            this.Status = null;
        }

        /// <summary>
        /// Deactivates the user.
        /// </summary>
        public void DeactivateUser()
        {
            this.IsActive = false;
            this.Status = null;
        }

        /// <summary>
        /// Sets the deactivation request to true.
        /// </summary>
        public void EnableDeactivationRequest()
        {
            this.DeactivationRequest = true;
        }

        /// <summary>
        /// Changes the role of the user.
        /// </summary>
        /// <param name="newRole"><see cref="Roles"/> New role.</param>
        public void ChangeRole(Roles newRole)
        {
            this.UserRole.ChangeRole(newRole);
        }

        /// <summary>
        /// Changes the status of this user.
        /// </summary>
        /// <param name="newStatus"><see cref="ProfileStatus"/> enum.</param>
        public void ChangeStatus(ProfileStatus newStatus)
        {
            this.Status = newStatus;
        }

        /// <summary>
        /// Update the profile pic for the user.
        /// </summary>
        /// <param name="profilePic"> Profile picture.</param>
        public void UpdateProfilePicture(string profilePic)
        {
            this.ProfilePicture = profilePic;
        }
    }
}