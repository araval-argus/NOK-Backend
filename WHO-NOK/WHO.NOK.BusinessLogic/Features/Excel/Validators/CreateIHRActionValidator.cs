// <copyright file="CreateIHRActionValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.Excel.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.Features.Excel;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="CreateIHRActionCommand"/> class.
    /// </summary>
    public class CreateIHRActionValidator : AbstractValidator<CreateIHRActionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateIHRActionValidator"/> class.
        /// </summary>
        public CreateIHRActionValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateIHRActionValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public CreateIHRActionValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.IndicatorId).NotNull().NotEmpty().WithMessage(localizer["InvalidIndicatorId"]);
            this.RuleFor(x => x.BenchMark).NotNull().NotEmpty().WithMessage(localizer["InvalidBenchMark"]);
            this.RuleFor(x => x.Objectives).NotNull().NotEmpty().WithMessage(localizer["InvalidObjective"]);
            this.RuleFor(x => x.Capacity).NotNull().NotEmpty().WithMessage(localizer["InvalidCapacity"]);
            this.RuleFor(x => x.PreviousScore).NotNull().GreaterThanOrEqualTo(0).WithMessage(localizer["InvalidPreviousScore"]);
            this.RuleFor(x => x.TargetScore).NotNull().GreaterThan(0).WithMessage(localizer["InvalidTargetScore"]);
            this.RuleFor(x => x.Actions).NotNull().NotEmpty().WithMessage(localizer["InvalidActions"]);
            this.RuleFor(x => x.CountryPlanId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidPlanId"]);
        }
    }
}