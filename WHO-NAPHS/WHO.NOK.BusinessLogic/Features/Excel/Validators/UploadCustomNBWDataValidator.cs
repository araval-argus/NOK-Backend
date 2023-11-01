// <copyright file="UploadCustomNBWDataValidator.cs" company="WHO">
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
    /// Fluent validator for <see cref="UploadCustomNBWDataCommand"/>.
    /// </summary>
    public class UploadCustomNBWDataValidator : AbstractValidator<UploadCustomNBWDataCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadCustomNBWDataValidator"/> class.
        /// </summary>
        public UploadCustomNBWDataValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadCustomNBWDataValidator"/> class.
        /// </summary>
        /// <param name="localizer"> Localizer.</param>
        public UploadCustomNBWDataValidator(IStringLocalizer<Resources> localizer)
        {
            this.RuleForEach(x => x.NBWRecommendations).SetValidator(new ExcelImportNBWRecommendationValidator());
            this.RuleFor(x => x.PlanningToolId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidPlanningToolId"]);
            this.RuleFor(x => x.CountryPlanId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidPlanId"]);
        }
    }
}