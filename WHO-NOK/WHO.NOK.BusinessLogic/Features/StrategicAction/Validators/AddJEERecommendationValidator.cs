// <copyright file="AddJEERecommendationValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.StrategicAction.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.BusinessLogic.Validators;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="AddJEERecommendationCommand"/>.
    /// </summary>
    public class AddJEERecommendationValidator : AbstractValidator<AddJEERecommendationCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddJEERecommendationValidator"/> class.
        /// </summary>
        public AddJEERecommendationValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddJEERecommendationValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer"> Localizer.</param>
        public AddJEERecommendationValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleForEach(x => x.JeeRecommendations).SetValidator(new AddJeeRecommendationValidator());
            this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0);
            this.RuleFor(x => x.PlanId).NotNull().GreaterThan(0);
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