// <copyright file="GetImportActionHistoryValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.Excel.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="GetImportActionHistoryCommand"/>.
    /// </summary>
    public class GetImportActionHistoryValidator : AbstractValidator<GetImportActionHistoryCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetImportActionHistoryValidator"/> class.
        /// </summary>
        public GetImportActionHistoryValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetImportActionHistoryValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public GetImportActionHistoryValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.ExcelType).NotNull().IsInEnum().NotEqual(ExcelTypes.Custom).WithMessage(localizer["InvalidExcelType"]);

            this.When(x => x.ExcelType == ExcelTypes.NBWAssessments, () =>
            {
                this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0).WithMessage(localizer["CountryIdValidation"]);
            });
        }
    }
}