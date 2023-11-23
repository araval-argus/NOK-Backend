// <copyright file="CreateOperationalPlanValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan.Validators
{
    using FluentValidation;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.BusinessLogic.Validators;
    using WHO.NOK.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Common.Resources;

    /// <summary>
    /// Fluent validator for <see cref="CreateOperationalPlanCommand"/>.
    /// </summary>
    public class CreateOperationalPlanValidator : AbstractValidator<CreateOperationalPlanCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOperationalPlanValidator"/> class.
        /// </summary>
        public CreateOperationalPlanValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOperationalPlanValidator"/> class.
        /// </summary>
        /// <param name="commonService"> Instance of <see cref="ICommonService"/>.</param>
        /// <param name="localizer"> Localizer.</param>
        public CreateOperationalPlanValidator(ICommonService commonService, IStringLocalizer<Resources> localizer)
        {
            this.RuleFor(x => x.User).NotNull().WithMessage(localizer["UserCantBeNull"]);
            this.RuleForEach(x => x.Indicators).SetValidator(new IndicatorsWithScoreValidator());
            this.RuleFor(x => x.CountryId).NotNull().GreaterThan(0).WithMessage(localizer["InvalidCountryId"]);
            this.RuleFor(x => x.StartDate).NotNull().LessThan(x => x.EndDate).WithMessage(localizer["StartDateValidation"]);
            this.RuleFor(x => x.EndDate).NotNull().GreaterThan(x => x.StartDate).WithMessage(localizer["EndDateValidation"]);
            this.RuleFor(x => x.StartDate).Must((x, startDate) => ShouldBeBetweenOneAndTwoYears(startDate, x.EndDate)).WithMessage(localizer["NotValidDateDiffOperationalPlan"]);
            this.RuleFor(x => x.StartDate).Must((x, startDate) => CompareWithStrategicPlanStartDate(startDate, x.StrategicPlan)).WithMessage(localizer["OPStartDateGreaterThanSPStartDate"]);
            this.RuleFor(x => x.EndDate).Must((x, endDate) => CompareWithStrategicPlanEndDate(endDate, x.StrategicPlan)).WithMessage(localizer["OPEndDateLessThanSPEndDate"]);
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
        /// Checks whether the date difference is less than two years or not.
        /// </summary>
        /// <param name="startDate">Start date.</param>
        /// <param name="endDate">End date.</param>
        /// <returns>Returns a value indicating whether the difference is less than two years or not.</returns>
        public static bool ShouldBeBetweenOneAndTwoYears(DateTime startDate, DateTime endDate)
        {
            return endDate >= startDate.AddYears(1) && endDate <= startDate.AddYears(2);
        }

        /// <summary>
        /// Check whether the operational plan's start date is less than or equal to the strategic plan's start date or not.
        /// </summary>
        /// <param name="startDate">Start date.</param>
        /// <param name="strategicPlan">Strategic plan.</param>
        /// <returns>Return a value indicating whether the operational plan's start date is less than or equal to the strategic plan's start date or not.</returns>
        public static bool CompareWithStrategicPlanStartDate(DateTime startDate, CountryPlanViewModel? strategicPlan)
        {
            return strategicPlan == null || startDate >= strategicPlan.PlanStartDate;
        }

        /// <summary>
        /// Check whether the operational plan's end date is greater than or equal to the strategic plan's end date or not.
        /// </summary>
        /// <param name="endDate">End date.</param>
        /// <param name="strategicPlan">Strategic plan.</param>
        /// <returns>Return a value indicating whether the operational plan's start date is less than or equal to the strategic plan's start date or not.</returns>
        public static bool CompareWithStrategicPlanEndDate(DateTime endDate, CountryPlanViewModel? strategicPlan)
        {
            return strategicPlan == null || endDate <= strategicPlan.PlanEndDate;
        }
    }
}