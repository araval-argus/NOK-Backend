// <copyright file="IHRActionListValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="IHRActionsListCommand"/>.
    /// </summary>
    public class IHRActionListValidator : AbstractValidator<IHRActionsListCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IHRActionListValidator"/> class.
        /// </summary>
        public IHRActionListValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IHRActionListValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public IHRActionListValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.IHRRecommendationId).NotNull().NotEmpty().GreaterThan(0).WithMessage(localizer["InvalidIHRActionId"]);
            this.RuleFor(x => x.PlanIndicatorId).NotNull().NotEmpty().GreaterThan(0).WithMessage(localizer["InvalidPlanIndicatorId"]);
        }
    }
}