// <copyright file="CountryPlanController.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.API.Controllers
{
    using FluentValidation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WHO.NAPHS.API.Helper;
    using WHO.NAPHS.BusinessLogic.Features.CountryPlan;
    using WHO.NAPHS.BusinessLogic.Features.StrategicAction;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.ViewModels.Assessments;
    using WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NAPHS.BusinessLogic.ViewModels.PaginatedResponses;
    using WHO.NAPHS.BusinessLogic.ViewModels.UserClaims;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.ResponseMiddleware;
    using WHO.NAPHS.Core.Wrappers;

    /// <summary>
    /// Controller to add the Country Plans.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class CountryPlanController : ControllerBase
    {
        private readonly ICountryPlanService planService;
        private readonly IStrategicActionService strategicActionService;
        private readonly IValidator<CreateStrategicPlanCommand> strategicPlanValidator;
        private readonly IValidator<CreateOperationalPlanCommand> operationalPlanValidator;
        private readonly IValidator<GetCountryInitialPlanDetailCommand> getCountryInitialPlanDetailValidator;
        private readonly IValidator<GetScoreForSelectedIndicatorsCommand> getScoreForSelectedIndicatorsValidator;
        private readonly IValidator<GetPlanDetailsCommand> getPlanDetailsValidator;
        private readonly IValidator<AddJEERecommendationCommand> addJEERecommendationValidator;
        private readonly IValidator<GetTechnicalAreasForUpdatePlanIndicatorsCommand> getTechnicalAreasForUpdatePlanIndicators;
        private readonly IValidator<CheckUpdatedScoreCommand> checkUpdatedScoreValidator;
        private readonly IValidator<GetCountryDashboardDetailsCommand> getDashboardValidator;
        private readonly IValidator<GetDataForDashboardChartCommand> getDashboardChartDetailsValidator;
        private readonly IValidator<UpdateIndicatorsForPlanCommand> updatePlanIndicators;
        private readonly IValidator<UpdatePlanIndicatorGoalCommand> updatePlanIndicatorGoalValidator;
        private readonly IValidator<GetJEERecommendationsCommand> getJEERecommendationValidator;
        private readonly IValidator<CancelPlanCommand> cancelPlanValidator;
        private readonly IValidator<CustomTechnicalAreasCommand> customTechnicalAreasValidator;
        private readonly IValidator<AddIHRActionsToPlanCommand> addIHRActionsToPlanValidator;
        private readonly IValidator<AddNBWActionsToPlanCommand> addNBWActionsToPlanValidator;
        private readonly IValidator<ActivatePlanCommand> activatePlanValidator;
        private readonly IValidator<DownloadPlanCommand> downloadPlanValidator;
        private readonly IValidator<ClonePlanCommand> clonePlanValidator;
        private readonly IValidator<GetReviewsCommand> getReviewsValidator;
        private readonly IValidator<UploadPlanCommand> uploadPlanValidator;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserClaimsViewModel user;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryPlanController"/> class.
        /// </summary>
        /// <param name="planService"> <see cref="ICountryPlanService"/> service.</param>
        /// <param name="strategicActionService"> <see cref="IStrategicActionService"/> service.</param>
        /// <param name="strategicPlanValidator"> Validate <see cref="GetExcelSheetDetailsCommand"/> command.</param>
        /// <param name="contextAccessor"> <see cref="IHttpContextAccessor"/> Context accessor. </param>
        /// <param name="getCountryInitialPlanDetailValidator"> Validate <see cref="GetCountryInitialPlanDetailCommand"/> command.</param>
        /// <param name="getScoreForSelectedIndicatorsValidator"> Validate <see cref="GetScoreForSelectedIndicatorsCommand"/> command.</param>
        /// <param name="getPlanDetailsValidator"> Validate <see cref="GetPlanDetailsCommand"/> command.</param>
        /// <param name="addJEERecommendationValidator"> Validate <see cref="AddJEERecommendationCommand"/> command.</param>
        /// <param name="operationalPlanValidator"> Validate <see cref="CreateOperationalPlanCommand"/> command.</param>
        /// <param name="getTechnicalAreasForUpdatePlanIndicators"> Validate <see cref="GetTechnicalAreasForUpdatePlanIndicatorsCommand"/> command.</param>
        /// <param name="checkUpdatedScoreValidator"> Validate <see cref="CheckUpdatedScoreCommand"/> command.</param>
        /// <param name="getDashboardValidator"> Validate <see cref="GetCountryDashboardDetailsCommand"/> command. </param>
        /// <param name="updatePlanIndicators"> Validate <see cref="UpdateIndicatorsForPlanCommand"/> command. </param>
        /// <param name="updatePlanIndicatorGoalValidator">Validate <see cref="UpdatePlanIndicatorGoalCommand"/> command. </param>
        /// <param name="getJEERecommendationValidator"> Validate <see cref="GetJEERecommendationsCommand"/> command.</param>
        /// <param name="cancelPlanValidator"> Validate <see cref="CancelPlanCommand"/> command. </param>
        /// <param name="customTechnicalAreasValidator"> Validate <see cref="CustomTechnicalAreasCommand"/> command. </param>
        /// <param name="getDashboardChartDetailsValidator"> Validate <see cref="GetDataForDashboardChartCommand"/> command. </param>
        /// <param name="addIHRActionsToPlanValidator"> Validate <see cref="AddIHRActionsToPlanCommand"/> command. </param>
        /// <param name="addNBWActionsToPlanValidator"> Validate <see cref="AddNBWActionsToPlanCommand"/> command. </param>
        /// <param name="activatePlanValidator"> Validate <see cref="ActivatePlanCommand"/> command. </param>
        /// <param name="downloadPlanValidator"> Validate <see cref="DownloadPlanCommand"/> command. </param>
        /// <param name="clonePlanValidator"> Validate <see cref="ClonePlanCommand"/> command. </param>
        /// <param name="getReviewsValidator"> Validate <see cref="GetReviewsCommand"/> command. </param>
        /// <param name="uploadPlanValidator"> Validate <see cref="UploadPlanCommand"/> command. </param>
        public CountryPlanController(
            ICountryPlanService planService,
            IStrategicActionService strategicActionService,
            IValidator<CreateStrategicPlanCommand> strategicPlanValidator,
            IHttpContextAccessor contextAccessor,
            IValidator<GetCountryInitialPlanDetailCommand> getCountryInitialPlanDetailValidator,
            IValidator<GetScoreForSelectedIndicatorsCommand> getScoreForSelectedIndicatorsValidator,
            IValidator<GetPlanDetailsCommand> getPlanDetailsValidator,
            IValidator<AddJEERecommendationCommand> addJEERecommendationValidator,
            IValidator<CreateOperationalPlanCommand> operationalPlanValidator,
            IValidator<GetTechnicalAreasForUpdatePlanIndicatorsCommand> getTechnicalAreasForUpdatePlanIndicators,
            IValidator<CheckUpdatedScoreCommand> checkUpdatedScoreValidator,
            IValidator<GetCountryDashboardDetailsCommand> getDashboardValidator,
            IValidator<UpdateIndicatorsForPlanCommand> updatePlanIndicators,
            IValidator<UpdatePlanIndicatorGoalCommand> updatePlanIndicatorGoalValidator,
            IValidator<GetJEERecommendationsCommand> getJEERecommendationValidator,
            IValidator<CancelPlanCommand> cancelPlanValidator,
            IValidator<CustomTechnicalAreasCommand> customTechnicalAreasValidator,
            IValidator<GetDataForDashboardChartCommand> getDashboardChartDetailsValidator,
            IValidator<AddIHRActionsToPlanCommand> addIHRActionsToPlanValidator,
            IValidator<AddNBWActionsToPlanCommand> addNBWActionsToPlanValidator,
            IValidator<ActivatePlanCommand> activatePlanValidator,
            IValidator<DownloadPlanCommand> downloadPlanValidator,
            IValidator<ClonePlanCommand> clonePlanValidator,
            IValidator<GetReviewsCommand> getReviewsValidator,
            IValidator<UploadPlanCommand> uploadPlanValidator)
        {
            this.planService = planService;
            this.strategicActionService = strategicActionService;
            this.strategicPlanValidator = strategicPlanValidator;
            this.contextAccessor = contextAccessor;
            this.getCountryInitialPlanDetailValidator = getCountryInitialPlanDetailValidator;
            this.getScoreForSelectedIndicatorsValidator = getScoreForSelectedIndicatorsValidator;
            this.getPlanDetailsValidator = getPlanDetailsValidator;
            this.addJEERecommendationValidator = addJEERecommendationValidator;
            this.operationalPlanValidator = operationalPlanValidator;
            this.getTechnicalAreasForUpdatePlanIndicators = getTechnicalAreasForUpdatePlanIndicators;
            this.checkUpdatedScoreValidator = checkUpdatedScoreValidator;
            this.getDashboardValidator = getDashboardValidator;
            this.updatePlanIndicators = updatePlanIndicators;
            this.updatePlanIndicatorGoalValidator = updatePlanIndicatorGoalValidator;
            this.getJEERecommendationValidator = getJEERecommendationValidator;
            this.cancelPlanValidator = cancelPlanValidator;
            this.customTechnicalAreasValidator = customTechnicalAreasValidator;
            this.getDashboardChartDetailsValidator = getDashboardChartDetailsValidator;
            this.addIHRActionsToPlanValidator = addIHRActionsToPlanValidator;
            this.addNBWActionsToPlanValidator = addNBWActionsToPlanValidator;
            this.activatePlanValidator = activatePlanValidator;
            this.downloadPlanValidator = downloadPlanValidator;
            this.clonePlanValidator = clonePlanValidator;
            this.getReviewsValidator = getReviewsValidator;
            this.uploadPlanValidator = uploadPlanValidator;
            this.user = this.contextAccessor.HttpContext!.User.GetUser();
        }

        /// <summary>
        /// Create strategic plan.
        /// </summary>
        /// <param name="plan"> <see cref="GetScoreForSelectedIndicatorsCommand"/> command to get plan details.</param>
        /// <returns> Returns integer representing id of the newly created plan.</returns>
        [HttpPost("CreateStrategicPlan")]
        [ProducesResponseType(typeof(ApiResponse<int>), 200)]
        public async Task<IActionResult> CreateStrategicPlan([FromBody] CreateStrategicPlanCommand plan)
        {
            plan.User = this.user;
            var result = await this.strategicPlanValidator.ValidateAsync(plan);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.CreateNewStrategicPlanAsync(plan));
        }

        /// <summary>
        /// Get country plan details while creating a new plan.
        /// </summary>
        /// <param name="planType"> <see cref="PlanType"/> enum.</param>
        /// <param name="countryId"> Country Id.</param>
        /// <returns> Returns <see cref="InitialPlanDetailsViewModel"/> representing initial plan details.</returns>
        [HttpGet("GetCountryInitialPlanDetail/{planType}/{countryId}")]
        [ProducesResponseType(typeof(ApiResponse<InitialPlanDetailsViewModel>), 200)]
        public async Task<IActionResult> GetCountryInitialPlanDetail(PlanType planType, int countryId)
        {
            var model = new GetCountryInitialPlanDetailCommand()
            {
                CountryId = countryId,
                PlanType = planType,
                User = this.user,
            };

            var result = await this.getCountryInitialPlanDetailValidator.ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.GetInitialPlanDetailsAsync(model));
        }

        /// <summary>
        /// Get score details for the selected indicators while creating a new plan.
        /// </summary>
        /// <param name="request"> <see cref="GetScoreForSelectedIndicatorsCommand"/> command for get score details for selected indicators.</param>
        /// <returns> Returns list of <see cref="IndicatorsWithScoreViewModel"/> representing the score and goal details for selected indicator of assessment.</returns>
        [HttpPost("GetScoreDetailsForSelectedIndicators")]
        [ProducesResponseType(typeof(ApiResponse<IndicatorsWithScoreViewModel>), 200)]
        public async Task<IActionResult> GetScoreDetailsForSelectedIndicators([FromBody] GetScoreForSelectedIndicatorsCommand request)
        {
            request.User = this.user;
            var result = await this.getScoreForSelectedIndicatorsValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(this.planService.GetScoreForSelectedIndicatorsAsync(request));
        }

        /// <summary>
        /// Get plan details.
        /// </summary>
        /// <param name="planId"> Plan Id.</param>
        /// <returns>Returns <see cref="CompletePlanDetailsViewModel"/> model representing plan details for the plan.</returns>
        [HttpGet("GetPlanDetails/{planId}")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> GetPlanDetails(int planId)
        {
            GetPlanDetailsCommand request = new ()
            {
                PlanId = planId,
                User = this.user,
            };

            var result = await this.getPlanDetailsValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.GetPlanDetailsAsync(request));
        }

        /// <summary>
        /// Save JEE recommendation for plan indicators.
        /// </summary>
        /// <param name="request"><see cref="AddJEERecommendationCommand"/> Command request to add JEE recommendations for indicator.</param>
        /// <returns>Returns updated <see cref="CompletePlanDetailsViewModel"/> model.</returns>
        [HttpPost("SaveJEERecommendationsForPlan")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> SaveJEERecommendationsForPlan([FromBody] AddJEERecommendationCommand request)
        {
            request.User = this.user;
            var result = await this.addJEERecommendationValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(this.strategicActionService.AddJEERecommendationAsync(request));
        }

        /// <summary>
        /// Create strategic plan.
        /// </summary>
        /// <param name="request"> <see cref="CreateOperationalPlanCommand"/> command request to create operational plan.</param>
        /// <returns>Returns the newly created plan id.</returns>
        [HttpPost("CreateOperationalPlan")]
        [ProducesResponseType(typeof(ApiResponse<int>), 200)]
        public async Task<IActionResult> CreateOperationalPlan([FromBody] CreateOperationalPlanCommand request)
        {
            request.User = this.user;
            var result = await this.operationalPlanValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.CreateOperationalPlanAsync(request));
        }

        /// <summary>
        /// Create strategic plan.
        /// </summary>
        /// <param name="planId"> Plan Id.</param>
        /// <returns>Returns the list of <see cref="TechnicalAreaViewModel"/> with state is exists in plan.</returns>
        [HttpGet("GetTechnicalAreasForUpdateIndicators/{planId}")]
        [ProducesResponseType(typeof(ApiResponse<List<TechnicalAreaViewModel>>), 200)]
        public async Task<IActionResult> GetTechnicalAreasForUpdateIndicators(int planId)
        {
            GetTechnicalAreasForUpdatePlanIndicatorsCommand request = new ()
            {
                PlanId = planId,
                User = this.user,
            };

            var result = await this.getTechnicalAreasForUpdatePlanIndicators.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.GetTechnicalAreasForUpdateIndicatorsAsync(request));
        }

        /// <summary>
        /// Create strategic plan.
        /// </summary>
        /// <param name="planId"> Plan id.</param>
        /// <returns>Returns the list of <see cref="UpdatedScoreViewModel"/> who's score has been updated from the plan created.</returns>
        [HttpPost("CheckUpdatedScore/{planId}")]
        [ProducesResponseType(typeof(ApiResponse<List<UpdatedScoreViewModel>>), 200)]
        public async Task<IActionResult> CheckUpdatedScore(int planId)
        {
            CheckUpdatedScoreCommand request = new ()
            {
                PlanId = planId,
                User = this.user,
            };

            var result = await this.checkUpdatedScoreValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.CheckForUpdatedScoreAsync(request));
        }

        /// <summary>
        /// Get plan details.
        /// </summary>
        /// <param name="planId"> Plan Id.</param>
        /// <returns> Returns technical areas and technical area indicators plan.</returns>
        [HttpGet("GetTechnicalAreaAndIndicatorsForPlan/{planId}")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> GetTechnicalAreaAndIndicatorsForPlan(int planId)
        {
            GetPlanDetailsCommand request = new ()
            {
                PlanId = planId,
                User = this.user,
            };

            var result = await this.getPlanDetailsValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.GetTechnicalAreaAndIndicatorsForPlanAsync(request));
        }

        /// <summary>
        /// Get dashboard details.
        /// </summary>
        /// <param name="request"> <see cref="GetCountryDashboardDetailsCommand"/> command request to get the dashboard details.</param>
        /// <returns> Returns the <see cref="DashboardViewModel"/> model.</returns>
        [HttpPost("GetDashboardDetails")]
        [ProducesResponseType(typeof(ApiResponse<DashboardViewModel>), 200)]
        public async Task<IActionResult> GetDashboardDetails([FromBody] GetCountryDashboardDetailsCommand request)
        {
            request.User = this.user;

            var result = await this.getDashboardValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.GetCountryDashboardDetailsAsync(request));
        }

        /// <summary>
        /// Get dashboard chart details.
        /// </summary>
        /// <param name="countryId"> Country id to get dashboard chart details.</param>
        /// <returns>Returns list of <see cref="CountryPlanViewModel"/> that has last four active country plans.</returns>
        [HttpGet("GetDashboardChartDetails/{CountryId}")]
        [ProducesResponseType(typeof(ApiResponse<List<CountryPlanViewModel>>), 200)]
        public async Task<IActionResult> GetDashboardChartDetails(int countryId)
        {
            var request = new GetDataForDashboardChartCommand()
            {
                CountryId = countryId,
                User = this.user,
            };

            var result = await this.getDashboardChartDetailsValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.GetDashboardChartDetailsAsync(request));
        }

        /// <summary>
        /// Update the plan indicators.
        /// </summary>
        /// <param name="request"> <see cref="UpdateIndicatorsForPlanCommand"/> command request to update the indicators.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPut("UpdatePlanIndicators")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> UpdatePlanIndicators([FromBody] UpdateIndicatorsForPlanCommand request)
        {
            request.User = this.user;

            var result = await this.updatePlanIndicators.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.UpdatePlanIndicatorsAsync(request));
        }

        /// <summary>
        /// Update the plan indicator goal.
        /// </summary>
        /// <param name="request"> <see cref="UpdatePlanIndicatorGoalCommand"/> command request to update the indicator goal.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPut("UpdateIndicatorGoal")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> UpdatePlanIndicatorGoal([FromBody] UpdatePlanIndicatorGoalCommand request)
        {
            request.User = this.user;

            var result = await this.updatePlanIndicatorGoalValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.UpdatePlanIndicatorGoalAsync(request));
        }

        /// <summary>
        /// Get JEE Recommendations.
        /// </summary>
        /// <param name="countryId"> Country id.</param>
        /// <param name="planId"> Plan id.</param>
        /// <returns> Returns list of <see cref="JEERecommendationsTechnicalAreasViewModel"/> representing the JEE recommendations and strengths and challenges for the technical areas and indicators.</returns>
        [HttpGet("GetJEERecommendations/{countryId}/{planId}")]
        [ProducesResponseType(typeof(ApiResponse<List<JEERecommendationsTechnicalAreasViewModel>>), 200)]
        public async Task<IActionResult> GetJEERecommendations(int countryId, int planId)
        {
            GetJEERecommendationsCommand request = new ()
            {
                CountryId = countryId,
                PlanId = planId,
                User = this.user,
            };

            var result = await this.getJEERecommendationValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.GetJEERecommendationsAsync(request));
        }

        /// <summary>
        /// Cancel active/draft plan.
        /// </summary>
        /// <param name="planId"> Plan Id.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPut("CancelPlan/{planId}")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> CancelPlan(int planId)
        {
            CancelPlanCommand request = new ()
            {
                User = this.user,
                PlanId = planId,
            };

            var result = await this.cancelPlanValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.CancelPlanAsync(request));
        }

        /// <summary>
        /// Cancel active/draft plan.
        /// </summary>
        /// <param name="request"> <see cref="CustomTechnicalAreasCommand"/> command request to create technical areas.</param>
        /// <returns>Returns newly created list of <see cref="TechnicalAreaViewModel"/> model.</returns>
        [HttpPost("CreateCustomTechnicalAreas")]
        [ProducesResponseType(typeof(ApiResponse<List<TechnicalAreaViewModel>>), 200)]
        public async Task<IActionResult> CreateCustomTechnicalAreas([FromBody] CustomTechnicalAreasCommand request)
        {
            request.User = this.user;
            var result = await this.customTechnicalAreasValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.CreateTechnicalAreasAsync(request));
        }

        /// <summary>
        /// Save IHR actions to plan.
        /// </summary>
        /// <param name="request"> <see cref="AddIHRActionsToPlanCommand"/> command request to add IHR action to plan.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPost("AddIHRActionToPlan")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> AddIHRActionsToPlan([FromBody] AddIHRActionsToPlanCommand request)
        {
            request.User = this.user;
            var result = await this.addIHRActionsToPlanValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.AddIHRActionsToPlanAsync(request));
        }

        /// <summary>
        /// Save NBW actions to plan.
        /// </summary>
        /// <param name="request"> <see cref="AddNBWActionsToPlanCommand"/> command request to add NBW action to plan.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPost("AddNBWActionsToPlan")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> AddNBWActionsToPlan([FromBody] AddNBWActionsToPlanCommand request)
        {
            request.User = this.user;
            var result = await this.addNBWActionsToPlanValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            request.User = this.user;

            return this.Ok(await this.planService.AddNBWActionsToPlanAsync(request));
        }

        /// <summary>
        /// Activate the plan.
        /// </summary>
        /// <param name="request"> <see cref="ActivatePlanCommand"/> command request to activate the plan.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPost("ActivatePlan")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> ActivatePlan([FromBody] ActivatePlanCommand request)
        {
            request.User = this.user;
            var result = await this.activatePlanValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.ActivatePlanAsync(request));
        }

        /// <summary>
        /// Download plan details.
        /// </summary>
        /// <param name="request"> <see cref="DownloadPlanCommand"/> command to download plan details.</param>
        /// <returns>List of <see cref="OperationalPlanDetailsViewModel"/>.</returns>
        [HttpPost("DownloadPlan")]
        [ProducesResponseType(typeof(ApiResponse<PlanDetailsViewModel>), 200)]
        public async Task<IActionResult> DownloadPlan(DownloadPlanCommand request)
        {
            request.User = this.user;
            var result = await this.downloadPlanValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.GetDetailsForDownloadPlanAsync(request));
        }

        /// <summary>
        /// Clone plan.
        /// </summary>
        /// <param name="request"> <see cref="ClonePlanCommand"/> Command request to clone the plan.</param>
        /// <returns> Returns the <see cref="CountryPlanViewModel"/> new created plan details.</returns>
        [HttpPost("ClonePlan")]
        [ProducesResponseType(typeof(ApiResponse<CountryPlanViewModel>), 200)]
        public async Task<IActionResult> ClonePlan(ClonePlanCommand request)
        {
            request.User = this.user;
            var result = await this.clonePlanValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.ClonePlanAsync(request));
        }

        /// <summary>
        /// Get plan details.
        /// </summary>
        /// <param name="planId"> Plan Id.</param>
        /// <returns>Returns the count of actions as per planType.</returns>
        [HttpGet("GetActionsCount/{planId}")]
        [ProducesResponseType(typeof(ApiResponse<int>), 200)]
        public async Task<IActionResult> Get(int planId)
        {
            GetPlanDetailsCommand request = new ()
            {
                PlanId = planId,
                User = this.user,
            };

            var result = await this.getPlanDetailsValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.GetActionsCount(request));
        }

        /// <summary>
        /// Get shared plans.
        /// </summary>
        /// <param name="request"><see cref="GetSharedPlansCommand"/> command request to get shared plans.</param>
        /// <returns> Returns list of <see cref="CountryPlanViewModel"/> containing only shared plans.</returns>
        [HttpPost("GetSharedPlans")]
        [ProducesResponseType(typeof(ApiResponse<DashboardViewModel>), 200)]
        public async Task<IActionResult> GetSharedPlans(GetSharedPlansCommand request)
        {
            return this.Ok(await this.planService.GetSharedPlansAsync(request));
        }

        /// <summary>
        /// Get a single plan complete details.
        /// </summary>
        /// <param name="planId">Id of the plan.</param>
        /// <returns>Returns the <see cref="CompletePlanDetailsViewModel"/> model.</returns>
        [HttpGet("SharedPlans/{planId}")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> GetSharedPlan(int planId)
        {
            return this.Ok(await this.planService.GetSharedPlanAsync(planId));
        }

        /// <summary>
        /// Get actions that requires financial/technical assistance.
        /// </summary>
        /// <param name="request"><see cref="GetAssistanceDataCommand"/> command request to  Get actions that requires financial/technical assistance.</param>
        /// <returns>Returns <see cref="GetAssistanceDashboardDataViewModel"/> model.</returns>
        [HttpPost("GetAssistanceData")]
        [ProducesResponseType(typeof(ApiResponse<PaginatedResponse<GetAssistanceDashboardDataViewModel>>), 200)]
        public async Task<IActionResult> GetAssistanceDashboardData([FromBody] GetAssistanceDataCommand request)
        {
            request.User = this.user;
            return this.Ok(await this.planService.GetAssistanceDashboardDataAsync(request));
        }

        /// <summary>
        /// Upload Plan.
        /// </summary>
        /// <param name="request"> <see cref="UploadViewModel"/> command to upload Plan.</param>
        /// <returns> Returns the list of <see cref="UploadPlanResponseViewModel"/> that has status of each rows uploaded.</returns>
        [HttpPost("UploadPlan")]
        [ProducesResponseType(typeof(ApiResponse<List<UploadPlanResponseViewModel>>), 200)]
        public async Task<IActionResult> UploadPlan(UploadPlanCommand request)
        {
            request.User = this.user;
            var result = await this.uploadPlanValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(this.planService.UploadPlanAsync(request));
        }

        /// <summary>
        /// Get reviews.
        /// </summary>
        /// <param name="months">months which is to be added into current date.</param>
        /// <returns>Returns list of <see cref="CountryPlanReviewViewModel"/> model containing review details.</returns>
        [HttpGet("GetReviews")]
        [ProducesResponseType(typeof(ApiResponse<List<CountryPlanReviewViewModel>>), 200)]
        public async Task<IActionResult> GetReviews()
        {
            GetReviewsCommand request = new ()
            {
                User = this.user,
            };

            var result = await this.getReviewsValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.GetReviews(request));
        }

        [HttpGet("GetPlanForReview/{planId}")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> GetPlanForReview(int planId)
        {
            GetPlanDetailsCommand request = new ()
            {
                PlanId = planId,
                User = this.user,
            };

            var result = await this.getPlanDetailsValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planService.GetPlanDetailsAsync(request));
        }
    }
}