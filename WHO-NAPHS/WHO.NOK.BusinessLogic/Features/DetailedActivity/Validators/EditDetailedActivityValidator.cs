// <copyright file="EditDetailedActivityValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.DetailedActivity.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for update detailed activity command.
    /// </summary>
    public class EditDetailedActivityValidator : AbstractValidator<EditDetailedActivityCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditDetailedActivityValidator"/> class.
        /// </summary>
        public EditDetailedActivityValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditDetailedActivityValidator"/> class.
        /// </summary>
        /// <param name="localizer">IStringLocalizer.</param>
        public EditDetailedActivityValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.DetailedActivityId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidDetailedActivityId"]);
            this.RuleFor(x => x.StartDate).NotNull().LessThan(x => x.EndDate).WithMessage(localizer["StartDateValidation"]);
            this.RuleFor(x => x.Feasibility).NotNull().IsInEnum().WithMessage(localizer["InvalidFeasibility"]);
            this.RuleFor(x => x.Impact).NotNull().IsInEnum().WithMessage(localizer["InvalidImpact"]);
            this.RuleFor(x => x.Priority).NotNull().IsInEnum().WithMessage(localizer["PriorityValidation"]);
        }
    }
}