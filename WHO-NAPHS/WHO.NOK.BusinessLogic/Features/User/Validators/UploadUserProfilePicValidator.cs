// <copyright file="UploadUserProfilePicValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.User.Validators
{
    using System.Collections.Immutable;
    using FluentValidation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="UploadUserProfilePicValidator"/>.
    /// </summary>
    public class UploadUserProfilePicValidator : AbstractValidator<UploadUserProfilePicCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadUserProfilePicValidator"/> class.
        /// </summary>
        public UploadUserProfilePicValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadUserProfilePicValidator"/> class.
        /// </summary>
        /// <param name="localizer">Localizer.</param>
        public UploadUserProfilePicValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull();
            this.RuleFor(x => x.ProfilePicture).Must(this.BeAValidImage).WithMessage(localizer["InvalidImage"]);
        }

        /// <summary>
        /// Check if the uploaded file is valid or not.
        /// </summary>
        /// <param name="file"> <see cref="IFormFile"/> Uploaded file.</param>
        /// <returns> Returns the status of the file weather the file is valid or not.</returns>
        private bool BeAValidImage(IFormFile file)
        {
            if (file == null)
            {
                return false;
            }

            // Define the allowed image file extensions
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            // Check the file extension
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return false;
            }

            // Check the file content type (optional)
            if (!file.ContentType.StartsWith("image/"))
            {
                return false;
            }

            return true;
        }
    }
}
