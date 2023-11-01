// <copyright file="UpdatePlanIndicatorGoalValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validation for <see cref="UpdatePlanIndicatorGoalCommand"/> class.
    /// </summary>
    public class UpdatePlanIndicatorGoalValidator : AbstractValidator<UpdatePlanIndicatorGoalCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePlanIndicatorGoalValidator"/> class.
        /// </summary>
        public UpdatePlanIndicatorGoalValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePlanIndicatorGoalValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer">localizer.</param>
        public UpdatePlanIndicatorGoalValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.PlanIndicatorId).NotNull().GreaterThan(0);
            this.RuleFor(x => x.CountryPlanId).NotNull().GreaterThan(0);
            this.RuleFor(x => x.Score).NotNull().GreaterThan(0).WithMessage(localizer["InvalidScore"]);
            this.RuleFor(x => x.Goal).NotNull().LessThanOrEqualTo(5).GreaterThanOrEqualTo(x => x.Score).WithMessage(localizer["InvalidGoal"]);
            this.RuleFor(x => x).MustAsync(async (x, cancellationToken) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.UpdatePlan,
                    planId: x.CountryPlanId);
                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException();
            });
        }
    }
}