// <copyright file="CheckUpdatedScoreValidator.cs" company="WHO">
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
    /// Fluent validator for <see cref="CheckUpdatedScoreCommand"/>.
    /// </summary>
    public class CheckUpdatedScoreValidator : AbstractValidator<CheckUpdatedScoreCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckUpdatedScoreValidator"/> class.
        /// </summary>
        public CheckUpdatedScoreValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckUpdatedScoreValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public CheckUpdatedScoreValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.PlanId).NotNull().GreaterThan(0);
        }
    }
}