// <copyright file="ImportExcelFileValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.Excel.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.Helper;
    using WHO.NOK.Core.Common.Resources;
    using WHO.NOK.Core.Constant;

    /// <summary>
    /// Fluent validator for <see cref="ImportExcelFileCommand"/>.
    /// </summary>
    public class ImportExcelFileValidator : AbstractValidator<ImportExcelFileCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExcelFileValidator"/> class.
        /// </summary>
        public ImportExcelFileValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExcelFileValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public ImportExcelFileValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.File).Custom((file, context) =>
            {
                if (file == null)
                {
                    context.AddFailure(localizer["NoFileFound"]);
                    return;
                }

                var ext = Path.GetExtension(file.FileName).Trim().ToLower();
                var validExtensions = CommonSettings.ApplicationSettings!.ValidExcelExtensions.Split(',').ToList();

                if (!validExtensions.Contains(ext) && !ExcelValidator.ValidateExcelFile(file))
                {
                    context.AddFailure(localizer["InvalidFileTypeForExcel"]);
                }
            });
            this.RuleFor(x => x.SheetName).NotEmpty().NotNull().When(x => !string.IsNullOrEmpty(x.SheetName)).WithMessage(localizer["InvalidSheetName"]);
            this.RuleFor(x => x.ExcelType).NotNull().IsInEnum().WithMessage(localizer["InvalidExcelType"]);
        }
    }
}