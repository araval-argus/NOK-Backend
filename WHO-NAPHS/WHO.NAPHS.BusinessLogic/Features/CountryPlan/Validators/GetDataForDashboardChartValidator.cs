// <copyright file="GetDataForDashboardChartValidator.cs" company="WHO">
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
    /// Fluent validator for <see cref="GetDataForDashboardChartCommand"/>.
    /// </summary>
    public class GetDataForDashboardChartValidator : AbstractValidator<GetDataForDashboardChartCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDataForDashboardChartValidator"/> class.
        /// </summary>
        public GetDataForDashboardChartValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDataForDashboardChartValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public GetDataForDashboardChartValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidCountryId"]);
        }
    }
}