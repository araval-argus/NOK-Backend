// <copyright file="AddIHRActionsToPlanValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="AddIHRActionsToPlanCommand"/>.
    /// </summary>
    public class AddIHRActionsToPlanValidator : AbstractValidator<AddIHRActionsToPlanCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddIHRActionsToPlanValidator"/> class.
        /// </summary>
         public AddIHRActionsToPlanValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddIHRActionsToPlanValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer"> Localizer.</param>
         public AddIHRActionsToPlanValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleForEach(x => x.IHRActions).SetValidator(new IHRActionListValidator());
            this.RuleFor(x => x.CountryPlanId).NotNull().NotEmpty().GreaterThan(0).WithMessage(localizer["InvalidCountryPlanId"]);
            this.RuleFor(x => x).MustAsync(async (x, cancellation) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.AddOrEditAction,
                    planId: x.CountryPlanId);
                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException(localizer["UnauthorizedAccess"]);
            });
        }
    }
}