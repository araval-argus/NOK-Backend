// <copyright file="ExcelImportIndicatorValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.ViewModels.Excel;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="ExcelImportIndicatorViewModel"/> .
    /// </summary>
    public class ExcelImportIndicatorValidator : AbstractValidator<ExcelImportIndicatorViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelImportIndicatorValidator"/> class.
        /// </summary>
        public ExcelImportIndicatorValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelImportIndicatorValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public ExcelImportIndicatorValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.IndicatorName).NotNull().NotEmpty().MaximumLength(1000).WithMessage(localizer["InvalidIndicatorName"]);
            this.RuleFor(x => x.IndicatorCode).NotNull().NotEmpty().WithMessage(localizer["InvalidIndicatorCode"]);
            this.RuleFor(x => x.IndicatorId).NotNull().NotEmpty().WithMessage(localizer["InvalidIndicatorId"]);
        }
    }
}