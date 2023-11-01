// <copyright file="ICountryPlanService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ServiceInterfaces
{
    using WHO.NOK.BusinessLogic.Features.CountryPlan;
    using WHO.NOK.BusinessLogic.ViewModels.Assessments;
    using WHO.NOK.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NOK.BusinessLogic.ViewModels.PaginatedResponses;

    /// <summary>
    /// Country service plan service interface.
    /// </summary>
    public interface ICountryPlanService : IBaseService
    {
        /// <summary>
        /// Get all strategic plans for country.
        /// </summary>
        /// <param name="countryId"> Country Id.</param>
        /// <returns>Returns list of <see cref="CountryPlanListViewModel"/> model.</returns>
        Task<List<CountryPlanListViewModel>> GetAllPlansForCountryAsync(int countryId);

        /// <summary>
        /// Create new strategic plan.
        /// </summary>
        /// <param name="request"> <see cref="CreateStrategicPlanCommand"/> command request to create new strategic plan.</param>
        /// <returns> Returns integer representing id of the newly created plan.</returns>
        Task<int> CreateNewStrategicPlanAsync(CreateStrategicPlanCommand request);

        /// <summary>
        /// Get initial plan details.
        /// </summary>
        /// <param name="request"> <see cref="GetCountryInitialPlanDetailCommand"/> command request to get initial plan details. </param>
        /// <returns> Returns <see cref="InitialPlanDetailsViewModel"/> representing initial plan details.</returns>
        Task<InitialPlanDetailsViewModel> GetInitialPlanDetailsAsync(GetCountryInitialPlanDetailCommand request);

        /// <summary>
        /// Get score details for selected indicators while creating a plan.
        /// </summary>
        /// <param name="request"> <see cref="GetScoreForSelectedIndicatorsCommand"/> command request to get score details.</param>
        /// <returns>Returns list of <see cref="IndicatorsWithScoreViewModel"/> model.</returns>
        Task<List<IndicatorsWithScoreViewModel>> GetScoreForSelectedIndicatorsAsync(GetScoreForSelectedIndicatorsCommand request);

        /// <summary>
        /// Get plan details for the plan.
        /// </summary>
        /// <param name="request"> <see cref="GetPlanDetailsCommand"/> command request to get plan details.</param>
        /// <returns>Returns <see cref="CompletePlanDetailsViewModel"/> model representing plan details for the plan.</returns>
        Task<CompletePlanDetailsViewModel> GetPlanDetailsAsync(GetPlanDetailsCommand request);

        /// <summary>
        /// Get technical areas and indicators list for plan.
        /// </summary>
        /// <param name="request"> Command request for get technical areas and indicators for plan.</param>
        /// <returns> Returns country plan with list of technical areas and technical area indicators of plan.</returns>
        Task<CompletePlanDetailsViewModel> GetTechnicalAreaAndIndicatorsForPlanAsync(GetPlanDetailsCommand request);

        /// <summary>
        /// Command to check the updated score for plan indicator.
        /// </summary>
        /// <param name="request"> <see cref="CheckUpdatedScoreCommand"/> command request to check the updated score for plan indicator.</param>
        /// <returns>Returns the list of <see cref="UpdatedScoreViewModel"/> who's score has been updated from the plan created.</returns>
        Task<List<UpdatedScoreViewModel>> CheckForUpdatedScoreAsync(CheckUpdatedScoreCommand request);

        /// <summary>
        /// Create operational plan.
        /// </summary>
        /// <param name="request"> <see cref="CreateOperationalPlanCommand"/> command request to create operational plan.</param>
        /// <returns>Returns the newly inserted plan id.</returns>
        Task<int> CreateOperationalPlanAsync(CreateOperationalPlanCommand request);

        /// <summary>
        /// Update the indicators for the plan.
        /// </summary>
        /// <param name="request"> <see cref="GetTechnicalAreasForUpdatePlanIndicatorsCommand"/> command request to update country plan indicators.</param>
        /// <returns>Returns the list of <see cref="TechnicalAreaViewModel"/> with state is exists in plan.</returns>
        Task<List<TechnicalAreaViewModel>> GetTechnicalAreasForUpdateIndicatorsAsync(GetTechnicalAreasForUpdatePlanIndicatorsCommand request);

        /// <summary>
        /// Get dashboard details.
        /// </summary>
        /// <param name="request"> <see cref="GetCountryDashboardDetailsCommand"/> command request to get the dashboard details.</param>
        /// <returns> Returns the <see cref="DashboardViewModel"/> model.</returns>
        Task<DashboardViewModel> GetCountryDashboardDetailsAsync(GetCountryDashboardDetailsCommand request);

        /// <summary>
        /// Update indicators for the plan.
        /// </summary>
        /// <param name="request"> <see cref="UpdateIndicatorsForPlanCommand"/> command request to update the indicators.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> UpdatePlanIndicatorsAsync(UpdateIndicatorsForPlanCommand request);

        /// <summary>
        /// Update plan indicator goal.
        /// </summary>
        /// <param name="request"> <see cref="UpdatePlanIndicatorGoalCommand"/> command request to update the indicator goal.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> UpdatePlanIndicatorGoalAsync(UpdatePlanIndicatorGoalCommand request);

        /// <summary>
        /// Get JEE recommendation for plans.
        /// </summary>
        /// <param name="request"> <see cref="GetJEERecommendationsCommand"/> command request to get the JEE recommendations for plan. </param>
        /// <returns>Returns list of <see cref="JEERecommendationsTechnicalAreasViewModel"/> model.</returns>
        Task<List<JEERecommendationsTechnicalAreasViewModel>> GetJEERecommendationsAsync(GetJEERecommendationsCommand request);

        /// <summary>
        /// Cancel the draft/active plan.
        /// </summary>
        /// <param name="request"> <see cref="CancelPlanCommand"/> command request to cancel the plan.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> CancelPlanAsync(CancelPlanCommand request);

        /// <summary>
        /// Create technical area.
        /// </summary>
        /// <param name="request"> <see cref="CustomTechnicalAreasCommand"/> command request to create technical areas.</param>
        /// <returns>Returns newly created list of <see cref="TechnicalAreaViewModel"/> model.</returns>
        Task<List<TechnicalAreaViewModel>> CreateTechnicalAreasAsync(CustomTechnicalAreasCommand request);

        /// <summary>
        /// Get last four active plans for dashboard chart.
        /// </summary>
        /// <param name="request"> <see cref="GetDataForDashboardChartCommand"/> command request to get data for dashboard chart.</param>
        /// <returns>Returns list of <see cref="CountryPlanViewModel"/> that has last four active country plans.</returns>
        Task<List<CountryPlanViewModel>> GetDashboardChartDetailsAsync(GetDataForDashboardChartCommand request);

        /// <summary>
        /// Add ihr actions to country plan.
        /// </summary>
        /// <param name="request"> <see cref="AddIHRActionsToPlanCommand"/> command request to add IHR action to plan.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> AddIHRActionsToPlanAsync(AddIHRActionsToPlanCommand request);

        /// <summary>
        /// Add nbw actions to country plan.
        /// </summary>
        /// <param name="request"> <see cref="AddNBWActionsToPlanCommand"/> command request to add NBW action to plan.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> AddNBWActionsToPlanAsync(AddNBWActionsToPlanCommand request);

        /// <summary>
        /// Activate the plan.
        /// </summary>
        /// <param name="request"> <see cref="ActivatePlanCommand"/> command request to activate the plan.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> ActivatePlanAsync(ActivatePlanCommand request);

        /// <summary>
        /// Get actions that requires financial/technical assistance.
        /// </summary>
        /// <param name="request"><see cref="GetAssistanceDataCommand"/> command request to  Get actions that requires financial/technical assistance.</param>
        /// <returns>Returns <see cref="GetAssistanceDashboardDataViewModel"/> model.</returns>
        Task<GetAssistanceDashboardDataViewModel> GetAssistanceDashboardDataAsync(GetAssistanceDataCommand request);

        /// <summary>
        /// Gets the data to download the plan.
        /// </summary>
        /// <param name="request"> <see cref="DownloadPlanCommand"/> command request to get the data to download the plan.</param>
        /// <returns> List of plan details.</returns>
        Task<PlanDetailsViewModel> GetDetailsForDownloadPlanAsync(DownloadPlanCommand request);

        /// <summary>
        /// Clone strategic or operational plan.
        /// </summary>
        /// <param name="request"> <see cref="ClonePlanCommand"/> command request to clone the plan.</param>
        /// <returns> Returns <see cref="CountryPlanViewModel"/> the new plan details.</returns>
        Task<CountryPlanViewModel> ClonePlanAsync(ClonePlanCommand request);

        /// <summary>
        /// Gets the Action Count For SP or OP.
        /// </summary>
        /// <param name="request"> <see cref="GetPlanDetailsCommand"/> command request to clone the plan.</param>
        /// <returns> Returns the Count of Strategic or Operational Actions.</returns>
        Task<int> GetActionsCount(GetPlanDetailsCommand request);

        /// <summary>
        /// Gets all the public plans.
        /// </summary>
        /// <param name="request"> <see cref="GetSharedPlansCommand"/> command request to get the shared plans.</param>
        /// <returns> Returns list of <see cref="CountryPlanViewModel"/>.</returns>
        Task<DashboardViewModel> GetSharedPlansAsync(GetSharedPlansCommand request);

        /// <summary>
        /// Gets the shared plan.
        /// </summary>
        /// <param name="planId">Id of the plan to be returned.</param>
        /// <returns>Returns the <see cref="CompletePlanDetailsViewModel"/> model.</returns>
        Task<CompletePlanDetailsViewModel> GetSharedPlanAsync(int planId);

        /// <summary>
        /// Gets the reviews of plans.
        /// </summary>
        /// <param name="request"><see cref="GetReviewsCommand"/> command request to get the reviews.</param>
        /// <returns>Returns the <see cref="CountryPlanReviewViewModel"/> model.</returns>
        Task<List<CountryPlanReviewViewModel>> GetReviews(GetReviewsCommand request);

        /// <summary>
        /// Upload plan from the excel file and update the plan according the excel file.
        /// </summary>
        /// <param name="request"> <see cref="UploadPlanCommand"/> Command to upload the plan and update.</param>
        /// <returns> Returns the list of <see cref="UploadPlanResponseViewModel"/>.</returns>
        Task<List<UploadPlanResponseViewModel>> UploadPlanAsync(UploadPlanCommand request);
    }
}
