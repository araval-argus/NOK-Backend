// <copyright file="GetReviewsValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="GetReviewsCommand"/>.
    /// </summary>
    public class GetReviewsValidator : AbstractValidator<GetReviewsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetReviewsValidator"/> class.
        /// </summary>
        public GetReviewsValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetReviewsValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer"> Localizer.</param>
        public GetReviewsValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
        }
    }
}