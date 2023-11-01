// <copyright file="UploadExcelFileValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.Excel.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.Helper;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;
    using WHO.NAPHS.Core.Constant;

    /// <summary>
    /// Fluent validator for <see cref="UploadExcelFileCommand"/>.
    /// </summary>
    public class UploadExcelFileValidator : AbstractValidator<UploadExcelFileCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadExcelFileValidator"/> class.
        /// </summary>
        public UploadExcelFileValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadExcelFileValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public UploadExcelFileValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.File).Custom((file, context) =>
            {
                if (file == null)
                {
                    return;
                }

                var ext = Path.GetExtension(file.FileName).Trim().ToLower();
                var validExtensions = CommonSettings.ApplicationSettings!.ValidExcelExtensions.Split(',').ToList();

                if (!validExtensions.Contains(ext) && !ExcelValidator.ValidateExcelFile(file))
                {
                    context.AddFailure(localizer["InvalidFileTypeForExcel"]);
                }
            });
            this.RuleFor(x => x.ExcelType).NotNull().IsInEnum();
            this.When(x => x.ExcelType == ExcelTypes.Custom, () =>
            {
                this.RuleFor(x => x.CountryPlanId).NotNull().GreaterThan(0).WithMessage(localizer["NotAnOperationalPlan"]);
            });
            this.When(x => x.ExcelType == ExcelTypes.NBWAssessments, () =>
            {
                this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0).WithMessage(localizer["CountryIdValidation"]);
            });
        }
    }
}