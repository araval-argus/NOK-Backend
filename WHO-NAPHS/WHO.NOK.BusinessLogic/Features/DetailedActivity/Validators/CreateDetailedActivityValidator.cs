// <copyright file="CreateDetailedActivityValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.DetailedActivity.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="CreateDetailedActivityCommand"/> class.
    /// </summary>
    public class CreateDetailedActivityValidator : AbstractValidator<CreateDetailedActivityCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDetailedActivityValidator"/> class.
        /// </summary>
        public CreateDetailedActivityValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDetailedActivityValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer">IStringLocalizer.</param>
        public CreateDetailedActivityValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.CountryPlanId).NotNull().NotEmpty().GreaterThan(0).WithMessage(localizer["InvalidCountryPlanId"]);
            this.RuleFor(x => x.StrategicActionId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidStrategicActionId"]);
            this.RuleFor(x => x.StartDate).NotNull().LessThan(x => x.EndDate).WithMessage(localizer["StartDateValidation"]);
            this.RuleFor(x => x.Feasibility).NotNull().IsInEnum().WithMessage(localizer["InvalidFeasibility"]);
            this.RuleFor(x => x.Impact).NotNull().IsInEnum().WithMessage(localizer["InvalidImpact"]);
            this.RuleFor(x => x.Priority).NotNull().IsInEnum().WithMessage(localizer["PriorityValidation"]);
            this.RuleForEach(x => x.ActivityTypes).NotNull().GreaterThan(0).WithMessage(localizer["InvalidActivityTypeId"]);
            this.RuleFor(x => x).MustAsync(async (x, cancellation) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.AddOrEditAction,
                    strategicActionId: x.StrategicActionId);
                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException(localizer["UnauthorizedAccess"]);
            });
        }
    }
}