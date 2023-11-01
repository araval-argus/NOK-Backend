// <copyright file="GetDashboardDetailsValidator.cs" company="WHO">
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
    /// Fluent validator for <see cref="GetCountryDashboardDetailsCommand"/>.
    /// </summary>
    public class GetDashboardDetailsValidator : AbstractValidator<GetCountryDashboardDetailsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDashboardDetailsValidator"/> class.
        /// </summary>
        public GetDashboardDetailsValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDashboardDetailsValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public GetDashboardDetailsValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull();
        }
    }
}