// <copyright file="ExcelImportTechnicalAreaValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.ViewModels.Excel;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="ExcelImportTechnicalAreaViewModel"/> .
    /// </summary>
    public class ExcelImportTechnicalAreaValidator : AbstractValidator<ExcelImportTechnicalAreaViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelImportTechnicalAreaValidator"/> class.
        /// </summary>
        public ExcelImportTechnicalAreaValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelImportTechnicalAreaValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public ExcelImportTechnicalAreaValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.TechnicalArea).NotNull().NotEmpty().MaximumLength(1000).WithMessage(localizer["InvalidTechnicalArea"]);
            this.RuleFor(x => x.AreaCode).NotNull().NotEmpty().WithMessage(localizer["InvalidAreaCode"]);
            this.RuleForEach(x => x.TechnicalAreaIndicators).SetValidator(new ExcelImportIndicatorValidator()).WithMessage(localizer["InvalidTechnicalAreaIndicator"]);
        }
    }
}