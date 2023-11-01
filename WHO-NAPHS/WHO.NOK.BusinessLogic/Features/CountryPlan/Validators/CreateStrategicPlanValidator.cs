// <copyright file="CreateStrategicPlanValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan.Validators
{
    using System.Net;
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.Validators;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="CreateStrategicPlanCommand"/>.
    /// </summary>
    public class CreateStrategicPlanValidator : AbstractValidator<CreateStrategicPlanCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStrategicPlanValidator"/> class.
        /// </summary>
        public CreateStrategicPlanValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStrategicPlanValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer"> Localizer.</param>
        public CreateStrategicPlanValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleForEach(x => x.Indicators).SetValidator(new IndicatorsWithScoreValidator());
            this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidCountryId"]);
            this.RuleFor(x => x.StartDate).NotNull().LessThan(x => x.EndDate).WithMessage(localizer["StartDateValidation"]);
            this.RuleFor(x => x.EndDate).NotNull().GreaterThan(x => x.StartDate).WithMessage(localizer["EndDateValidation"]);
            this.RuleFor(x => x.StartDate).Must((x, startDate) => BeExactly5Years(startDate, x.EndDate)).WithMessage(localizer["NotValidDateDiffStrategicPlan"]);
            this.RuleFor(x => x).MustAsync(async (x, cancellation) =>
            {
                var isAuthorized = await commonService.IsUserAuthorizedAsync(
                    (int)x.User?.UserId!,
                    ActivityTypes.CreatePlan,
                    countryId: x.CountryId);
                return isAuthorized ? isAuthorized : throw new UnauthorizedAccessException(localizer["UnauthorizedAccess"]);
            });
        }

        /// <summary>
        /// Check if diff is 5 year or not between dates.
        /// </summary>
        /// <param name="startDate"> Start Date.</param>
        /// <param name="endDate"> End Date.</param>
        /// <returns> Returns the flag if date diff is 5 year.</returns>
        private static bool BeExactly5Years(DateTime startDate, DateTime endDate)
        {
            return startDate.AddYears(5) == endDate;
        }
    }
}