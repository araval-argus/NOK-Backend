// <copyright file="GetExcelSheetDetailsValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.Features.Excel;
    using WHO.NOK.BusinessLogic.Helper;
    using WHO.NOK.Core.Common.Resources;
    using WHO.NOK.Core.Constant;

    /// <summary>
    /// Fluent validator for <see cref="GetExcelSheetDetailsCommand"/>.
    /// </summary>
    public class GetExcelSheetDetailsValidator : AbstractValidator<GetExcelSheetDetailsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetExcelSheetDetailsValidator"/> class.
        /// </summary>
        public GetExcelSheetDetailsValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetExcelSheetDetailsValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public GetExcelSheetDetailsValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.File).Custom((file, context) =>
            {
                if (file == null)
                {
                    return;
                }

                var ext = Path.GetExtension(file.FileName).Replace(".", string.Empty).Trim().ToLower();
                var validExtensions = CommonSettings.ApplicationSettings!.ValidExcelExtensions.Split(',').ToList();

                if (!validExtensions.Contains(ext) && !ExcelValidator.ValidateExcelFile(file))
                {
                    context.AddFailure(localizer["InvalidFileTypeForExcel"]);
                }
            });
        }
    }
}