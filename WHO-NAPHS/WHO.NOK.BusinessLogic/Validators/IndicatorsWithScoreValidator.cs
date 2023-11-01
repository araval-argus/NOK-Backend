// <copyright file="IndicatorsWithScoreValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="IndicatorsWithScoreViewModel"/> .
    /// </summary>
    public class IndicatorsWithScoreValidator : AbstractValidator<IndicatorsWithScoreViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndicatorsWithScoreValidator"/> class.
        /// </summary>
        public IndicatorsWithScoreValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndicatorsWithScoreValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public IndicatorsWithScoreValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.IndicatorCodeId).NotNull().NotEmpty().WithMessage(localizer["InvalidIndicatorCodeId"]);
            this.RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(1000).WithMessage(localizer["InvalidIndicatorName"]);
            this.RuleFor(x => x.TechnicalAreaId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidTechnicalAreaId"]);
            this.RuleFor(x => x.Score).NotNull().GreaterThanOrEqualTo(0).WithMessage(localizer["InvalidScore"]);
            this.RuleFor(x => x.Goal).NotNull().GreaterThan(0).WithMessage(localizer["InvalidGoal"]);
        }
    }
}