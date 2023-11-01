// <copyright file="CreateTechnicalAreaIndicatorValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="CreateTechnicalAreaIndicatorCommand"/>.
    /// </summary>
    public class CreateTechnicalAreaIndicatorValidator : AbstractValidator<CreateTechnicalAreaIndicatorCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTechnicalAreaIndicatorValidator"/> class.
        /// </summary>
        public CreateTechnicalAreaIndicatorValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTechnicalAreaIndicatorValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public CreateTechnicalAreaIndicatorValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(localizer["NameValidation"]);
        }
    }
}