// <copyright file="CreateDetailedActivityTypeValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.DetailedActivity.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="CreateDetailedActivityTypeCommand"/> class.
    /// </summary>
    public class CreateDetailedActivityTypeValidator : AbstractValidator<CreateDetailedActivityTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDetailedActivityTypeValidator"/> class.
        /// </summary>
        public CreateDetailedActivityTypeValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDetailedActivityTypeValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer">IStringLocalizer.</param>
        public CreateDetailedActivityTypeValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0).WithMessage(localizer["CountryIdValidation"]);
            this.RuleFor(x => x.Activity).NotNull().NotEmpty().WithMessage(localizer["ActivityValidation"]);
            this.RuleFor(x => x).MustAsync(async (x, cancellation) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.AddOrEditAction,
                    countryId: x.CountryId);
                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException(localizer["UnauthorizedAccess"]);
            });
        }
    }
}