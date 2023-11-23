// <copyright file="UpdateDetailedActivityValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.DetailedActivity.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="UpdateDetailedActivityCommand"/> class.
    /// </summary>s
    public class UpdateDetailedActivityValidator : AbstractValidator<UpdateDetailedActivityCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDetailedActivityValidator"/> class.
        /// </summary>
        public UpdateDetailedActivityValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDetailedActivityValidator"/> class.
        /// </summary>
        /// <param name="commonService">Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer">IStringLocalizer.</param>
        public UpdateDetailedActivityValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.CountryPlanId).NotNull().NotEmpty().GreaterThan(0).WithMessage(localizer["InvalidCountryPlanId"]);
            this.RuleFor(x => x.DetailedActivityId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidDetailedActivityId"]);
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