// <copyright file="ExcelImportNBWRecommendationValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.ViewModels.Excel;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="NBWRecommendationsViewModel"/> .
    /// </summary>
    public class ExcelImportNBWRecommendationValidator : AbstractValidator<NBWRecommendationsViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelImportNBWRecommendationValidator"/> class.
        /// </summary>
        public ExcelImportNBWRecommendationValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelImportNBWRecommendationValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public ExcelImportNBWRecommendationValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.Source).NotNull().NotEmpty().WithMessage(localizer["InvalidSource"]);
            this.RuleFor(x => x.SetOfIndicator).NotNull().NotEmpty().WithMessage(localizer["InvalidSetOfIndicator"]);
            this.RuleFor(x => x.GroupingInRoadMap).NotNull().NotEmpty().WithMessage(localizer["InvalidGroupingInRoadMap"]);
            this.RuleFor(x => x.TechnicalArea).NotNull().NotEmpty().WithMessage(localizer["InvalidTechnicalArea"]);
            this.RuleFor(x => x.IndicatorId).NotNull().NotEmpty().WithMessage(localizer["InvalidIndicatorId"]);
            this.RuleFor(x => x.IndicatorCode).NotNull().NotEmpty().WithMessage(localizer["InvalidIndicatorCode"]);
            this.RuleFor(x => x.IndicatorName).NotNull().NotEmpty().WithMessage(localizer["InvalidIndicatorName"]);
            this.RuleFor(x => x.Objective).NotNull().NotEmpty().WithMessage(localizer["InvalidObjective"]);
            this.RuleFor(x => x.StrategicAction).NotNull().NotEmpty().WithMessage(localizer["InvalidStrategicAction"]);
            this.RuleFor(x => x.DetailedActivity).NotNull().NotEmpty().WithMessage(localizer["InvalidDetailedActivity"]);
            this.RuleFor(x => x.Objective).NotNull().NotEmpty().WithMessage(localizer["InvalidObjective"]);
            this.RuleFor(x => x.Feasibility).NotNull().NotEmpty().IsInEnum().WithMessage(localizer["InvalidFeasibility"]);
            this.RuleFor(x => x.Impact).NotNull().NotEmpty().IsInEnum().WithMessage(localizer["InvalidImpact"]);
        }
    }
}