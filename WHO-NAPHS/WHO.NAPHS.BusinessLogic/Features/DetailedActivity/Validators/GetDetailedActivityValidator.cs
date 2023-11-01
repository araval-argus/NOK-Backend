// <copyright file="GetDetailedActivityValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.DetailedActivity.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="GetDetailedActivityCommand"/> class.
    /// </summary>
    public class GetDetailedActivityValidator : AbstractValidator<GetDetailedActivityCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDetailedActivityValidator"/> class.
        /// </summary>
        public GetDetailedActivityValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDetailedActivityValidator"/> class.
        /// </summary>
        /// <param name="commonService">Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer">IStringLocalizer.</param>
        public GetDetailedActivityValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0).WithMessage(localizer["CountryIdValidation"]);
        }
    }
}