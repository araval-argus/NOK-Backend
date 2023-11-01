// <copyright file="ClonePlanValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="ClonePlanValidator"/>.
    /// </summary>
    public class ClonePlanValidator : AbstractValidator<ClonePlanCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClonePlanValidator"/> class.
        /// </summary>
        public ClonePlanValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClonePlanValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer"> Localizer.</param>
        public ClonePlanValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.PlanId).NotNull().GreaterThan(0);
            this.RuleFor(x => x.StartDate).NotNull().NotEmpty();
            this.RuleFor(x => x.EndDate).NotNull().NotEmpty().GreaterThan(x => x.StartDate);
            this.RuleFor(x => x).MustAsync(async (x, cancellation) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.ClonePlan,
                    planId: x.PlanId);
                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException(localizer["UnauthorizedAccess"]);
            });
        }
    }
}