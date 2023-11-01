// <copyright file="CustomTechnicalAreaValidator.cs" company="WHO">
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
    /// Fluent validator for <see cref="CustomTechnicalAreasCommand"/>.
    /// </summary>
    public class CustomTechnicalAreaValidator : AbstractValidator<CustomTechnicalAreasCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTechnicalAreaValidator"/> class.
        /// </summary>
        public CustomTechnicalAreaValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTechnicalAreaValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public CustomTechnicalAreaValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidCountryId"]);
            this.RuleForEach(x => x.TechnicalAreas).SetValidator(new CreateTechnicalAreaValidator());
        }
    }
}