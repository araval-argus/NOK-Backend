// <copyright file="ImportIHRDataValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.Excel.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.Validators;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="ImportIHRDataCommand"/>.
    /// </summary>
    public class ImportIHRDataValidator : AbstractValidator<ImportIHRDataCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportIHRDataValidator"/> class.
        /// </summary>
        public ImportIHRDataValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportIHRDataValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public ImportIHRDataValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleForEach(x => x.IHRRecommendations).SetValidator(new ExcelImportIHRRecommendationValidator());
            this.RuleFor(x => x.PlanningToolId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidPlanningToolId"]);

            // this.RuleFor(x => x.CountryId).NotNull().GreaterThanOrEqualTo(0).WithMessage(localizer["CountryIdValidation"]);
        }
    }
}