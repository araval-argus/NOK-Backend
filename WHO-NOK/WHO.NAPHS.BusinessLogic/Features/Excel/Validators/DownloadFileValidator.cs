// <copyright file="DownloadFileValidator.cs" company="WHO">
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
    /// Fluent validator for <see cref="DownloadFileCommand"/>.
    /// </summary>
    public class DownloadFileValidator : AbstractValidator<DownloadFileCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadFileValidator"/> class.
        /// </summary>
        public DownloadFileValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadFileValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public DownloadFileValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.PlanningToolId).NotNull().NotEmpty().GreaterThan(0).WithMessage(localizer["InvalidPlanningToolId"]);
        }
    }
}