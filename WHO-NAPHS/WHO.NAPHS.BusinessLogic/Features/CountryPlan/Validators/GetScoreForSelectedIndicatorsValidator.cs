// <copyright file="GetScoreForSelectedIndicatorsValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.Validators;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="GetScoreForSelectedIndicatorsCommand"/>.
    /// </summary>
    public class GetScoreForSelectedIndicatorsValidator : AbstractValidator<GetScoreForSelectedIndicatorsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetScoreForSelectedIndicatorsValidator"/> class.
        /// </summary>
        public GetScoreForSelectedIndicatorsValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetScoreForSelectedIndicatorsValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public GetScoreForSelectedIndicatorsValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleForEach(x => x.Indicators).SetValidator(new TechnicalAreaIndicatorValidator());
            this.RuleFor(x => x.AssessmentType).NotNull().IsInEnum();
            this.RuleFor(x => x.PlanType).NotNull().IsInEnum();
            this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0);
        }
    }
}