// <copyright file="UpdateIndicatorsForPlanValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.Validators;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="UpdateIndicatorsForPlanCommand"/>.
    /// </summary>
    public class UpdateIndicatorsForPlanValidator : AbstractValidator<UpdateIndicatorsForPlanCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateIndicatorsForPlanValidator"/> class.
        /// </summary>
        public UpdateIndicatorsForPlanValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateIndicatorsForPlanValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer"> Localizer.</param>
        public UpdateIndicatorsForPlanValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.Indicators).Must(x => x != null && x.Any());
            this.RuleForEach(x => x.Indicators).SetValidator(new TechnicalAreaIndicatorValidator());
            this.RuleFor(x => x.PlanId).NotNull().GreaterThan(0);
            this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0);
            this.RuleFor(x => x.AssessmentType).NotNull().IsInEnum();
            this.RuleFor(x => x.PlanType).NotNull().IsInEnum();
            this.RuleFor(x => x).MustAsync(async (x, cancellationToken) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.UpdatePlan,
                    planId: x.PlanId);
                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException();
            });
        }
    }
}