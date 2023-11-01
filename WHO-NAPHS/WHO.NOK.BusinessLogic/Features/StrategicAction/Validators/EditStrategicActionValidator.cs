// <copyright file="EditStrategicActionValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.StrategicAction.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="EditStrategicActionCommand"/> command.
    /// </summary>
    public class EditStrategicActionValidator : AbstractValidator<EditStrategicActionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditStrategicActionValidator"/> class.
        /// </summary>
        public EditStrategicActionValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditStrategicActionValidator"/> class.
        /// </summary>
        /// <param name="localizer">String localizer.</param>
        public EditStrategicActionValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.StrategicActionId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidStrategicActionId"]);
            this.RuleFor(x => x.Objective).NotNull().NotEmpty().WithMessage(localizer["StrategicActionObjectiveNull"]);
            this.RuleFor(x => x.Feasibility).NotNull().IsInEnum().WithMessage(localizer["StrategicActionFeasibilityNull"]);
            this.RuleFor(x => x.ImplementationStatus).NotNull().IsInEnum().WithMessage(localizer["InvalidImplementationStatus"]);
            this.RuleFor(x => x.Impact).NotNull().IsInEnum().WithMessage(localizer["StrategicActionImpactNull"]);
            this.RuleFor(x => x.ResponsibleAuthority).MaximumLength(1000).WithMessage(localizer["ResponsibleAuthorityMaxLength"]);
            this.RuleFor(x => x.EstimatedCost).NotNull().WithMessage(localizer["EstimatedCostNull"]);
        }
    }
}