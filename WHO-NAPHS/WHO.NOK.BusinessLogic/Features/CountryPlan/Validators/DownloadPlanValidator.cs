// <copyright file="DownloadPlanValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validation for <see cref="DownloadPlanCommand"/> command.
    /// </summary>
    public class DownloadPlanValidator : AbstractValidator<DownloadPlanCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadPlanValidator"/> class.
        /// </summary>
        public DownloadPlanValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadPlanValidator"/> class.
        /// </summary>
        /// <param name="commonService">Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer">Instance of <see cref="IStringLocalizer"/>.</param>
        public DownloadPlanValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleFor(x => x.CountryPlanId).GreaterThan(0).WithMessage(localizer["InvalidPlanId"]);
            this.RuleFor(x => x).MustAsync(async (x, context) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    x.User!.UserId,
                    ActivityTypes.DownloadPlan,
                    planId: x.CountryPlanId);

                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException();
            });
        }
    }
}