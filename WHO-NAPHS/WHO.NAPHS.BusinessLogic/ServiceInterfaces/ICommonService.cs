// <copyright file="ICommonService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ServiceInterfaces
{
    using WHO.NAPHS.BusinessLogic.Features.User;
    using WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NAPHS.BusinessLogic.ViewModels.StrategicAction;
    using WHO.NAPHS.BusinessLogic.ViewModels.UserClaims;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Common service for common methods that can be used between multiple repositories.
    /// </summary>
    public interface ICommonService : IBaseService
    {
        /// <summary>
        /// Get country Id from the country ISO code.
        /// </summary>
        /// <param name="countryISOCode"> Country ISO code.</param>
        /// <returns> Returns the country Id from the ISO Code.</returns>
        Task<int> GetCountryIdFromISOCodeAsync(string countryISOCode);

        /// <summary>
        /// Get country id from the ISO code.
        /// </summary>
        /// <param name="countryId"> Country Id.</param>
        /// <returns> Returns the country ISO code.</returns>
        Task<string> GetCountryISOCodeFromIdAsync(int countryId);

        /// <summary>
        /// Get country id from the iso3 code.
        /// </summary>
        /// <param name="iso3Code"> Country ISO3 code.</param>
        /// <returns> Returns the country id associated with iso3 code.</returns>
        Task<int> GetCountryIdFromISO3CodeAsync(string iso3Code);

        /// <summary>
        /// Get country iso3 code from the country id.
        /// </summary>
        /// <param name="countryId"> Country id.</param>
        /// <returns> Returns the country iso3 code that is associated with country id.</returns>
        Task<string> GetCountryISO3CodeFromIdAsync(int countryId);

        /// <summary>
        /// Calculate action priority.
        /// </summary>
        /// <param name="feasibility"> <see cref="ActionFeasibility"/> enum.</param>
        /// <param name="impact"> <see cref="ActionImpact"/> enum.</param>
        /// <returns> Returns the <see cref="ActionPriority"/> enum from feasibility and impact.</returns>
        Task<ActionPriority> GetActionPriorityAsync(ActionFeasibility feasibility, ActionImpact impact);

        /// <summary>
        /// Get plan details.
        /// </summary>
        /// <param name="planId"> Plan id.</param>
        /// <param name="needCountryPlanInfo"> Need country plan info.</param>
        /// <param name="includeAreas"> Include areas.</param>
        /// <param name="includeIndicators"> Include indicators.</param>
        /// <param name="includeStrategicActions"> Include strategic action.</param>
        /// <param name="includeDetailedActivity"> Include detailed activity.</param>
        /// <returns> Returns the complete plan info.</returns>
        Task<CompletePlanDetailsViewModel> GetPlanDetailsAsync(
            int planId,
            bool needCountryPlanInfo = true,
            bool includeAreas = true,
            bool includeIndicators = true,
            bool includeStrategicActions = true,
            bool includeDetailedActivity = true);

        /// <summary>
        /// Build complete plan model.
        /// </summary>
        /// <param name="countryPlan"> <see cref="CountryPlanViewModel"/> model.</param>
        /// <param name="technicalAreas"> <see cref="TechnicalAreaViewModel"/> model.</param>
        /// <param name="indicators"> List of <see cref="TechnicalAreaIndicatorViewModel"/> model.</param>
        /// <param name="strategicActions"> List of <see cref="StrategicActionViewModel"/> model.</param>
        /// <param name="detailActivities"> List of <see cref="DetailActivityViewModel"/> model. </param>
        /// <param name="planStats"> <see cref="PlanStatisticsViewModel"/>. </param>
        /// <param name="includeIndicators"> Include indicators.</param>
        /// <param name="includeStrategicActions"> Include strategic action.</param>
        /// <param name="includeDetailedActivity"> Include detailed activities. </param>
        /// <returns>Returns the newly created <see cref="CompletePlanDetailsViewModel"/> model.</returns>
        Task<CompletePlanDetailsViewModel> BuildCompletePlanModelAsync(
            CountryPlanViewModel countryPlan,
            List<TechnicalAreaViewModel> technicalAreas,
            List<TechnicalAreaIndicatorViewModel> indicators,
            List<StrategicActionViewModel> strategicActions,
            List<DetailActivityViewModel> detailActivities,
            PlanStatisticsViewModel planStats,
            bool includeIndicators = true,
            bool includeStrategicActions = true,
            bool includeDetailedActivity = true);

        /// <summary>
        /// Build indicators for technical areas.
        /// </summary>
        /// <param name="area"> <see cref="TechnicalAreaViewModel"/> model.</param>
        /// <param name="indicators"> List of <see cref="TechnicalAreaIndicatorViewModel"/> model.</param>
        /// <param name="strategicActions"> List of <see cref="StrategicActionViewModel"/> model.</param>
        /// <param name="detailActivities"> List of <see cref="DetailActivityViewModel"/> model. </param>
        /// <param name="includeStrategicActions"> Include strategic action.</param>
        /// <param name="includeDetailedActivity"> Include detailed activities. </param>
        /// <returns> Returns list of <see cref="TechnicalAreaIndicatorViewModel"/> view model.</returns>
        Task<List<TechnicalAreaIndicatorViewModel>> BuildIndicatorsForTechnicalAreaAsync(
            TechnicalAreaViewModel area,
            List<TechnicalAreaIndicatorViewModel> indicators,
            List<StrategicActionViewModel> strategicActions,
            List<DetailActivityViewModel> detailActivities,
            bool includeStrategicActions = true,
            bool includeDetailedActivity = true);

        /// <summary>
        /// Build strategic action model for indicator.
        /// </summary>
        /// <param name="indicator"> <see cref="TechnicalAreaIndicatorViewModel"/> model.</param>
        /// <param name="strategicActions"> List of <see cref="StrategicActionViewModel"/> model.</param>
        /// <param name="detailActivities"> List of <see cref="DetailActivityViewModel"/> model. </param>
        /// <param name="includeDetailedActivity"> Include detailed activities. </param>
        /// <returns> Returns list of <see cref="StrategicActionViewModel"/> view model.</returns>
        Task<List<StrategicActionViewModel>> BuildStrategicActionModelAsync(
            TechnicalAreaIndicatorViewModel indicator,
            List<StrategicActionViewModel> strategicActions,
            List<DetailActivityViewModel> detailActivities,
            bool includeDetailedActivity = true);

        /// <summary>
        /// Build detailed activity model for strategic action.
        /// </summary>
        /// <param name="strategicAction"> <see cref="StrategicActionViewModel"/> model.</param>
        /// <param name="detailActivities"> List of <see cref="DetailActivityViewModel"/> model. </param>
        /// <returns> Returns list of <see cref="DetailActivityViewModel"/> view model.</returns>
        Task<List<DetailActivityViewModel>> BuildDetailedActivitiesAsync(
            StrategicActionViewModel strategicAction,
            List<DetailActivityViewModel> detailActivities);

        /// <summary>
        /// Generate the random string.
        /// </summary>
        /// <param name="length"> Length of the string.</param>
        /// <param name="numericOnly"> Consider only numeric values or not.</param>
        /// <returns> Returns the random string.</returns>
        Task<string> GenerateRandomStringAsync(int length, bool numericOnly = true);

        /// <summary>
        /// Check logged in user authorization status.
        /// </summary>
        /// <param name="model"> <see cref="UserClaimsViewModel"/> user claims model.</param>
        /// <returns> Returns the status of logged in user.</returns>
        bool CheckLoggedInUserAuthorizationAsync(UserClaimsViewModel? model);

        /// <summary>
        /// Checks whether a user is authorized to perform the activity or not.
        /// </summary>
        /// <param name="currentUserId">Id of currently logged in user.</param>
        /// <param name="activity">Name of the activity.</param>
        /// <param name="countryId">Id of the country.</param>
        /// <param name="planId">Id of plan.</param>
        /// <param name="strategicActionId">Id of Strategic Action.</param>
        /// <param name="detailedActivityId">Id of Detailed Activity.</param>
        /// <param name="userId">Id of the user to be updated.</param>
        /// <param name="region">region for the activity will be performed.</param>
        /// <returns>A value indicating whether the user is authorized or not.</returns>
        Task<bool> IsUserAuthorizedAsync(
            int currentUserId,
            string activity,
            int? countryId = null,
            int? planId = null,
            int? strategicActionId = null,
            int? detailedActivityId = null,
            int? userId = null,
            string? region = null);
    }
}