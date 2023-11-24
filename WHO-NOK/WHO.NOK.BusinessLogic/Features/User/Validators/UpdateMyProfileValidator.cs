// <copyright file="UpdateMyProfileValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace Namespace
{
    using FluentValidation;

    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.Features.User;

    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="UpdateMyProfileCommand"/> class.
    /// </summary>
    public class UpdateMyProfileValidator : AbstractValidator<UpdateMyProfileCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMyProfileValidator"/> class.
        /// Initializes a new instance of the <see cref="UpdateMyProfileValidator"/> class.
        /// </summary>
        public UpdateMyProfileValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMyProfileValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public UpdateMyProfileValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage(localizer["UserIdValidation"]);
            this.RuleFor(x => x.FirstName).NotEmpty().WithMessage(localizer["FirstNameValidation"]);
            this.RuleFor(x => x.LastName).NotEmpty().WithMessage(localizer["LastNameValidation"]);
            this.RuleFor(x => x.PreferredLanguageId).NotNull().NotEmpty().IsInEnum().WithMessage(localizer["LanguageNotSelected"]);
        }
    }
}
