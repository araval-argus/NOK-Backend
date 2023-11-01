// <copyright file="CreateCustomStrategicActionValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.StrategicAction.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="CreateCustomStrategicActionCommand"/>.
    /// </summary>
    public class CreateCustomStrategicActionValidator : AbstractValidator<CreateCustomStrategicActionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCustomStrategicActionValidator"/> class.
        /// </summary>
        public CreateCustomStrategicActionValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCustomStrategicActionValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer"> Localizer.</param>
        public CreateCustomStrategicActionValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.CountryPlanId).NotNull().NotEmpty().GreaterThan(0).WithMessage(localizer["InvalidCountryPlanId"]);
            this.RuleFor(x => x.PlanIndicatorId).NotNull().GreaterThan(0);
            this.RuleFor(x => x.Objective).NotNull().NotEmpty().WithMessage(localizer["InvalidObjective"]);
            this.RuleFor(x => x.Action).NotNull().NotEmpty().WithMessage(localizer["InvalidActions"]);
            this.RuleFor(x => x.Feasibility).NotNull().IsInEnum().WithMessage(localizer["InvalidFeasibility"]);
            this.RuleFor(x => x.Impact).NotNull().IsInEnum().WithMessage(localizer["InvalidImpact"]);
            this.RuleFor(x => x.ResponsibleAuthority).MaximumLength(1000).WithMessage(localizer["ResponsibleAuthorityMaxLength"]);
            this.RuleFor(x => x.EstimatedCost).NotNull().WithMessage(localizer["EstimatedCostNull"]);
            this.RuleFor(x => x.Score).NotEmpty().GreaterThan(0).LessThanOrEqualTo(5).WithMessage(localizer["InvalidScore"]);
            this.RuleFor(x => x.Goal).NotNull().LessThanOrEqualTo(5).GreaterThanOrEqualTo(x => x.Score).WithMessage(localizer["InvalidGoal"]);
            this.RuleFor(x => x).MustAsync(async (x, cancellationToken) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.AddOrEditAction,
                    planId: x.CountryPlanId);

                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException();
            });
        }
    }
}