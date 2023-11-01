// <copyright file="AddNBWActionsToPlanValidator.cs" company="WHO">
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
    /// Fluent validator for <see cref="AddIHRActionsToPlanCommand"/>.
    /// </summary>
    public class AddNBWActionsToPlanValidator : AbstractValidator<AddNBWActionsToPlanCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddNBWActionsToPlanValidator"/> class.
        /// </summary>
        public AddNBWActionsToPlanValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddNBWActionsToPlanValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer"> Localizer.</param>
        public AddNBWActionsToPlanValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleForEach(x => x.NBWActions).NotNull().NotEmpty().GreaterThan(0).WithMessage(localizer["InvalidNBWRecommendationId"]);
            this.RuleFor(x => x.CountryPlanId).NotNull().NotEmpty().GreaterThan(0).WithMessage(localizer["InvalidCountryPlanId"]);
            this.RuleFor(x => x).MustAsync(async (x, cancellation) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.AddOrEditAction,
                    planId: x.CountryPlanId);
                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException(localizer["UnauthorizedAccess"]);
            });
        }
    }
}