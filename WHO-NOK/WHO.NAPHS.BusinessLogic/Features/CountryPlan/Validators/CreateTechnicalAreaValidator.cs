// <copyright file="CreateTechnicalAreaValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="CreateTechnicalAreaCommand"/>.
    /// </summary>
    public class CreateTechnicalAreaValidator : AbstractValidator<CreateTechnicalAreaCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTechnicalAreaValidator"/> class.
        /// </summary>
        public CreateTechnicalAreaValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTechnicalAreaValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public CreateTechnicalAreaValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(localizer["NameValidation"]);
            this.RuleForEach(x => x.Indicators).SetValidator(new CreateTechnicalAreaIndicatorValidator());
        }
    }
}