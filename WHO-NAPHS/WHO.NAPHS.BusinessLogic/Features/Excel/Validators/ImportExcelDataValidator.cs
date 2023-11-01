// <copyright file="ImportExcelDataValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.Excel.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.Validators;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="ImportExcelDataCommand"/>.
    /// </summary>
    public class ImportExcelDataValidator : AbstractValidator<ImportExcelDataCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExcelDataValidator"/> class.
        /// </summary>
        public ImportExcelDataValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExcelDataValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public ImportExcelDataValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.ExcelType).NotNull().IsInEnum().WithMessage(localizer["InvalidExcelType"]);
            this.RuleFor(x => x.TechnicalAreas).NotEmpty().WithMessage(localizer["InvalidTechnicalArea"]);
            this.RuleForEach(x => x.TechnicalAreas).SetValidator(new ExcelImportTechnicalAreaValidator()).WithMessage(localizer["InvalidTechnicalArea"]);
            this.RuleFor(x => x.PlanningToolId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidPlanningToolId"]);
        }
    }
}