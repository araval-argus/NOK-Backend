// <copyright file="DeleteDetailedActivityValidator.cs" company="WHO">
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
    /// Fluent validator for <see cref="DeleteDetailedActivityCommand"/> class.
    /// </summary>
    public class DeleteDetailedActivityValidator : AbstractValidator<DeleteDetailedActivityCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDetailedActivityValidator"/> class.
        /// </summary>
        public DeleteDetailedActivityValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDetailedActivityValidator"/> class.
        /// </summary>
        /// <param name="commonService">Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer">IStringLocalizer.</param>
        public DeleteDetailedActivityValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.DetailedActivityId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidStrategicActionId"]);
            this.RuleFor(x => x.CountryPlanId).NotNull().NotEmpty().GreaterThan(0).WithMessage(localizer["InvalidCountryPlanId"]);
            this.RuleFor(x => x).MustAsync(async (x, context) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.AddOrEditAction,
                    detailedActivityId: x.DetailedActivityId);

                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException();
            });
        }
    }
}