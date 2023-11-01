// <copyright file="ImportNBWActionsValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.Excel.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.Features.PlanRecommendations;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="ImportNBWActionsCommand"/>.
    /// </summary>
    public class ImportNBWActionsValidator : AbstractValidator<ImportNBWActionsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportNBWActionsValidator"/> class.
        /// </summary>
        public ImportNBWActionsValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportNBWActionsValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public ImportNBWActionsValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.CountryPlanId).NotNull().GreaterThan(0).WithMessage(localizer["CountryPlanIdValidation"]);
            this.When(x => x.IndicatorCodeId != null, () =>
            {
                this.RuleFor(x => x.IndicatorCode).NotNull().NotEmpty().WithMessage(localizer["InvalidIndicatorCode"]);
            });
            this.When(x => x.IndicatorCode != null, () =>
            {
                this.RuleFor(x => x.IndicatorCodeId).NotNull().NotEmpty().WithMessage(localizer["InvalidIndicatorId"]);
            });
        }
    }
}