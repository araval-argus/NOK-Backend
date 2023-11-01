// <copyright file="GetCountryInitialPlanDetailValidator.cs" company="WHO">
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
    /// Fluent validator for <see cref="GetCountryInitialPlanDetailCommand"/>.
    /// </summary>
    public class GetCountryInitialPlanDetailValidator : AbstractValidator<GetCountryInitialPlanDetailCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCountryInitialPlanDetailValidator"/> class.
        /// </summary>
        public GetCountryInitialPlanDetailValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCountryInitialPlanDetailValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer"> Localizer.</param>
        public GetCountryInitialPlanDetailValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidCountryId"]);
            this.RuleFor(x => x.PlanType).NotNull().NotEmpty().IsInEnum();
            this.RuleFor(x => x).MustAsync(async (x, cancellationToken) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.ViewPlan,
                    countryId: x.CountryId);
                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException();
            });
        }
    }
}