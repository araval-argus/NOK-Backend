// <copyright file="PlanRecommendationsController.cs" company="WHO">
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
    using WHO.NAPHS.BusinessLogic.Features.PlanRecommendations;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.ViewModels.PlanRecommendations;
    using WHO.NAPHS.BusinessLogic.ViewModels.UserClaims;
    using WHO.NAPHS.Core.ResponseMiddleware;
    using WHO.NAPHS.Core.Wrappers;

    /// <summary>
    /// Controller for Plan Recommendations.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class PlanRecommendationsController : ControllerBase
    {
        private readonly IPlanRecommendationService planRecommendationService;
        private readonly IValidator<ImportIHRActionsCommand> importIHRActionsValidator;
        private readonly IValidator<ImportNBWActionsCommand> importNBWActionsValidator;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserClaimsViewModel? user;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanRecommendationsController"/> class.
        /// </summary>
        /// <param name="contextAccessor"> <see cref="IHttpContextAccessor"/> Context accessor.</param>
        /// <param name="importIHRActionsValidator">Validate <see cref="ImportIHRActionsCommand"/> command.</param>
        /// <param name="importNBWActionsValidator">Validate <see cref="ImportNBWActionsCommand"/> command.</param>
        /// <param name="planRecommendationService"><see cref="IPlanRecommendationService"/> service.</param>
        public PlanRecommendationsController(
            IHttpContextAccessor contextAccessor,
            IValidator<ImportIHRActionsCommand> importIHRActionsValidator,
            IValidator<ImportNBWActionsCommand> importNBWActionsValidator,
            IPlanRecommendationService planRecommendationService)
        {
            this.planRecommendationService = planRecommendationService;
            this.importIHRActionsValidator = importIHRActionsValidator;
            this.importNBWActionsValidator = importNBWActionsValidator;
            this.contextAccessor = contextAccessor;
            this.user = this.contextAccessor!.HttpContext!.User.GetUser();
        }

        /// <summary>
        /// Create Custom NBW action for plan.
        /// </summary>
        /// <param name="countryPlanId">Country plan id.</param>
        /// <param name="indicatorCodeId">Indicator code id.</param>
        /// <param name="indicatorCode">Indicator code.</param>
        /// <returns>Returns list of <see cref="ImportIHRActionsViewModel"/> model.</returns>
        [HttpGet("ImportIHRActions/{countryPlanId}")]
        [ProducesResponseType(typeof(ApiResponse<ImportIHRActionsViewModel>), 200)]
        public async Task<IActionResult> ImportIHRActionsByPlanId(int countryPlanId, [FromQuery] string? indicatorCodeId, [FromQuery] string? indicatorCode)
        {
            ImportIHRActionsCommand request = new ()
            {
                CountryPlanId = countryPlanId,
                IndicatorCode = indicatorCode,
                IndicatorCodeId = indicatorCodeId,
                User = this.user,
            };

            var result = await this.importIHRActionsValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planRecommendationService.ImportIHRActionsAsync(request));
        }

        /// <summary>
        /// Create Custom NBW action for plan.
        /// </summary>
        /// <param name="countryPlanId">Country plan id.</param>
        /// <param name="indicatorCodeId">Indicator code id.</param>
        /// <param name="indicatorCode">Indicator code.</param>
        /// <returns>Returns list of <see cref="ImportNBWActionsViewModel"/> model.</returns>
        [HttpGet("ImportNBWActions/{countryPlanId}")]
        [ProducesResponseType(typeof(ApiResponse<ImportNBWActionsViewModel>), 200)]
        public async Task<IActionResult> ImportNBWActionsByPlanId(int countryPlanId, [FromQuery] string? indicatorCodeId, [FromQuery] string? indicatorCode)
        {
            ImportNBWActionsCommand request = new ()
            {
                CountryPlanId = countryPlanId,
                IndicatorCode = indicatorCode,
                IndicatorCodeId = indicatorCodeId,
                User = this.user,
            };

            var result = await this.importNBWActionsValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.planRecommendationService.ImportNBWActionsAsync(request));
        }
    }
}