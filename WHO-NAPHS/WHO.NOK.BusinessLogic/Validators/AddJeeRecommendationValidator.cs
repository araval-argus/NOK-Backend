// <copyright file="AddJeeRecommendationValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.ViewModels.StrategicAction;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="AddJeeRecommendationViewModel"/> .
    /// </summary>
    public class AddJeeRecommendationValidator : AbstractValidator<AddJeeRecommendationViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddJeeRecommendationValidator"/> class.
        /// </summary>
        public AddJeeRecommendationValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddJeeRecommendationValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public AddJeeRecommendationValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.IndicatorId).NotNull().NotEmpty().WithMessage(localizer["InvalidIndicatorId"]);
            this.RuleFor(x => x.JEERecommendation).NotNull().NotEmpty().WithMessage(localizer["InvalidJEERecommendation"]);
        }
    }
}