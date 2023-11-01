// <copyright file="CreateNBWActionValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.Excel.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="CreateNBWActionCommand"/> class.
    /// </summary>
    public class CreateNBWActionValidator : AbstractValidator<CreateNBWActionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNBWActionValidator"/> class.
        /// </summary>
        public CreateNBWActionValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNBWActionValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public CreateNBWActionValidator(IStringLocalizer<Resources> localizer)
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
            this.RuleFor(x => x.Feasibility).NotNull().WithMessage(localizer["InvalidFeasibility"]);
            this.RuleFor(x => x.Impact).NotNull().WithMessage(localizer["InvalidImpact"]);
            this.RuleFor(x => x.CountryPlanId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidPlanId"]);
            this.When(x => x.StartDate != null && x.EndDate != null, () =>
            {
                this.RuleFor(x => x.StartDate).Must((obj, startDate) => startDate < obj.EndDate).WithMessage(localizer["StartDateValidation"]);
            });
        }
    }
}