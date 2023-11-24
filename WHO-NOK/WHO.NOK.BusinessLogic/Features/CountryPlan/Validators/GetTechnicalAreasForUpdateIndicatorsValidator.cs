// <copyright file="GetTechnicalAreasForUpdateIndicatorsValidator.cs" company="WHO">
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
    /// Fluent validator for <see cref="GetTechnicalAreasForUpdatePlanIndicatorsCommand"/>.
    /// </summary>
    public class GetTechnicalAreasForUpdateIndicatorsValidator : AbstractValidator<GetTechnicalAreasForUpdatePlanIndicatorsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTechnicalAreasForUpdateIndicatorsValidator"/> class.
        /// </summary>
        public GetTechnicalAreasForUpdateIndicatorsValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTechnicalAreasForUpdateIndicatorsValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public GetTechnicalAreasForUpdateIndicatorsValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.PlanId).NotNull().GreaterThan(0);
        }
    }
}