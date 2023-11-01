// <copyright file="TechnicalAreaIndicatorValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="TechnicalAreaIndicatorViewModel"/> .
    /// </summary>
    public class TechnicalAreaIndicatorValidator : AbstractValidator<TechnicalAreaIndicatorViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicalAreaIndicatorValidator"/> class.
        /// </summary>
        public TechnicalAreaIndicatorValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicalAreaIndicatorValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public TechnicalAreaIndicatorValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.IndicatorCodeId).NotNull().NotEmpty().WithMessage(localizer["InvalidIndicatorCodeId"]);
            this.RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(1000).WithMessage(localizer["InvalidIndicatorName"]);
            this.RuleFor(x => x.TechnicalAreaId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidTechnicalAreaId"]);
        }
    }
}