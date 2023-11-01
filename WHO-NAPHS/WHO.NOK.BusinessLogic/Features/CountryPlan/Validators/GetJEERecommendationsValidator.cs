// <copyright file="GetJEERecommendationsValidator.cs" company="WHO">
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
    /// Fluent validator for <see cref="GetJEERecommendationsCommand"/>.
    /// </summary>
    public class GetJEERecommendationsValidator : AbstractValidator<GetJEERecommendationsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetJEERecommendationsValidator"/> class.
        /// </summary>
        public GetJEERecommendationsValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetJEERecommendationsValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer"> Localizer.</param>
        public GetJEERecommendationsValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.PlanId).NotNull().GreaterThan(0);
            this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0);
            this.RuleFor(x => x).MustAsync(async (x, cancellationToken) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.AddOrEditAction,
                    planId: x.PlanId);
                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException();
            });
        }
    }
}