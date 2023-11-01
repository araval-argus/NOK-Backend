// <copyright file="GetProfilePictureValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.User.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="GetUserCommand"/>.
    /// </summary>
    public class GetProfilePictureValidator : AbstractValidator<GetProfilePictureCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfilePictureValidator"/> class.
        /// </summary>
        public GetProfilePictureValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfilePictureValidator"/> class.
        /// </summary>
        /// <param name="localizer">Localizer.</param>
        public GetProfilePictureValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.ImagePath).NotNull().NotEmpty();
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
        }
    }
}
