// <copyright file="StrategicActionController.cs" company="WHO">
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
    using WHO.NAPHS.BusinessLogic.Features;
    using WHO.NAPHS.BusinessLogic.Features.DetailedActivity;
    using WHO.NAPHS.BusinessLogic.Features.StrategicAction;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NAPHS.BusinessLogic.ViewModels.DetailedActivity;
    using WHO.NAPHS.BusinessLogic.ViewModels.UserClaims;
    using WHO.NAPHS.Core.ResponseMiddleware;
    using WHO.NAPHS.Core.Wrappers;

    /// <summary>
    /// Controller for handling strategic action related operations.
    /// </summary>
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class StrategicActionController : BaseController
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserClaimsViewModel user;
        private readonly IStrategicActionService strategicActionService;
        private readonly IDetailedActivityService detailedActivityService;
        private readonly IValidator<CreateCustomStrategicActionCommand> createCustomStrategicActionValidator;
        private readonly IValidator<EditStrategicActionCommand> editStrategicActionValidator;
        private readonly IValidator<UpdateStrategicActionCommand> updateStrategicActionValidator;
        private readonly IValidator<DeleteStrategicActionCommand> deleteStrategicActionValidator;
        private readonly IValidator<CreateDetailedActivityCommand> createDetailedActivityValidator;
        private readonly IValidator<DeleteDetailedActivityCommand> deleteDetailedActivityValidator;
        private readonly IValidator<GetDetailedActivityCommand> getDetailedActivityValidator;
        private readonly IValidator<CreateDetailedActivityTypeCommand> createDetailedActivityTypeValidator;
        private readonly IValidator<UpdateDetailedActivityCommand> updateDetailedActivityValidator;
        private readonly IValidator<EditDetailedActivityCommand> editDetailedActivityValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategicActionController"/> class.
        /// </summary>
        /// <param name="contextAccessor"><see cref="IHttpContextAccessor"/> context accessor.</param>
        /// <param name="strategicActionService"><see cref="IStrategicActionService"/> Service.</param>
        /// <param name="detailedActivityService"><see cref="IDetailedActivityService"/> Service.</params>
        /// <param name="createCustomStrategicActionValidator">Validate <see cref="CreateCustomStrategicActionCommand"/> command. </param>
        /// <param name="editStrategicActionValidator">Validate <see cref="EditStrategicActionCommand"/> command. </param>
        /// <param name="updateStrategicActionValidator">Validate <see cref="UpdateStrategicActionCommand"/> command. </param>
        /// <param name="deleteStrategicActionValidator">Validate <see cref="DeleteStrategicActionCommand"/> command.</param>
        /// <param name="createDetailedActivityValidator">Validate <see cref="CreateDetailedActivityCommand"/> command. </params>
        /// <param name="deleteDetailedActivityValidator">Validate <see cref="DeleteDetailedActivityCommand"/> command. </params>
        /// <param name="getDetailedActivityValidator">Validate <see cref="GetDetailedActivityCommand"/> command. </params>
        /// <param name="createDetailedActivityTypeValidator">Validate <see cref="CreateDetailedActivityTypeCommand"/> command. </params>
        /// <param name="updateDetailedActivityValidator">Validate <see cref="UpdateDetailedActivityCommand"/> command. </params>
        /// <param name="editDetailedActivityValidator">Validate <see cref="EditDetailedActivityCommand"/> command. </params>
        public StrategicActionController(
            IHttpContextAccessor contextAccessor,
            IStrategicActionService strategicActionService,
            IDetailedActivityService detailedActivityService,
            IValidator<CreateCustomStrategicActionCommand> createCustomStrategicActionValidator,
            IValidator<EditStrategicActionCommand> editStrategicActionValidator,
            IValidator<UpdateStrategicActionCommand> updateStrategicActionValidator,
            IValidator<DeleteStrategicActionCommand> deleteStrategicActionValidator,
            IValidator<CreateDetailedActivityCommand> createDetailedActivityValidator,
            IValidator<DeleteDetailedActivityCommand> deleteDetailedActivityValidator,
            IValidator<GetDetailedActivityCommand> getDetailedActivityValidator,
            IValidator<CreateDetailedActivityTypeCommand> createDetailedActivityTypeValidator,
            IValidator<UpdateDetailedActivityCommand> updateDetailedActivityValidator,
            IValidator<EditDetailedActivityCommand> editDetailedActivityValidator)
        {
            this.contextAccessor = contextAccessor;
            this.strategicActionService = strategicActionService;
            this.detailedActivityService = detailedActivityService;
            this.createCustomStrategicActionValidator = createCustomStrategicActionValidator;
            this.editStrategicActionValidator = editStrategicActionValidator;
            this.updateStrategicActionValidator = updateStrategicActionValidator;
            this.deleteStrategicActionValidator = deleteStrategicActionValidator;
            this.createDetailedActivityValidator = createDetailedActivityValidator;
            this.deleteDetailedActivityValidator = deleteDetailedActivityValidator;
            this.getDetailedActivityValidator = getDetailedActivityValidator;
            this.createDetailedActivityTypeValidator = createDetailedActivityTypeValidator;
            this.updateDetailedActivityValidator = updateDetailedActivityValidator;
            this.editDetailedActivityValidator = editDetailedActivityValidator;
            this.user = this.contextAccessor.HttpContext!.User.GetUser();
        }

        /// <summary>
        /// Creates a new strategic action.
        /// </summary>
        /// <param name="request"><see cref="CreateCustomStrategicActionCommand"/> Command request to create strategic action.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPost("CreateStrategicAction")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> CreateStrategicAction([FromBody] CreateCustomStrategicActionCommand request)
        {
            request.User = this.user;
            var result = await this.createCustomStrategicActionValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.strategicActionService.CreateCustomStrategicActionAsync(request));
        }

        /// <summary>
        /// Edit a strategic action.
        /// </summary>
        /// <param name="request"><see cref="EditStrategicActionCommand"/> Command request to edit a strategic action.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPut("EditStrategicAction")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> EditStrategicAction([FromBody] EditStrategicActionCommand request)
        {
            request.User = this.user;
            var result = await this.editStrategicActionValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.strategicActionService.EditStrategicAction(request));
        }

        /// <summary>
        /// Update a strategic action.
        /// </summary>
        /// <param name="request"><see cref="UpdateStrategicActionCommand"/> Command request to update a strategic action.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPut("UpdateStrategicAction")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> UpdateStrategicAction([FromBody] UpdateStrategicActionCommand request)
        {
            request.User = this.user;
            var result = await this.updateStrategicActionValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.strategicActionService.UpdateStrategicAction(request));
        }

         /// <summary>
        /// Delete existing strategic action.
        /// </summary>
        /// <param name="strategicActionId">Strategic action id to be deleted.</param>
        /// <param name="countryPlanId">Country plan id.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPut("DeleteStrategicAction/{strategicActionId}/{countryPlanId}")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> DeleteStrategicAction(int strategicActionId, int countryPlanId)
        {
            var request = new DeleteStrategicActionCommand()
            {
                StrategicActionId = strategicActionId,
                CountryPlanId = countryPlanId,
                User = this.user,
            };

            var result = await this.deleteStrategicActionValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.strategicActionService.DeleteStrategicActionAsync(request));
        }

        /// <summary>
        /// Adds new detailed activity.
        /// </summary>
        /// <param name="request"> <see cref="CreateDetailedActivityCommand"/> command request to create detailed activity.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPost("AddDetailedActivity")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> AddDetailedActivity([FromBody] CreateDetailedActivityCommand request)
        {
            request.User = this.user;
            var result = await this.createDetailedActivityValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.detailedActivityService.CreateDetailedActivityAsync(request));
        }

        /// <summary>
        /// Update existing detailed activity.
        /// </summary>
        /// <param name="request"> <see cref="UpdateDetailedActivityCommand"/> command request create detailed activity.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPost("UpdateDetailedActivity")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> UpdateDetailedActivity([FromBody] UpdateDetailedActivityCommand request)
        {
            request.User = this.user;
            var result = await this.updateDetailedActivityValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.detailedActivityService.UpdateDetailedActivityAsync(request));
        }

        /// <summary>
        /// Delete existing detailed activity.
        /// </summary>
        /// <param name="detailedActivityId">Detailed activity id to be deleted.</param>
        /// <param name="countryPlanId">Country plan id.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPut("DeleteDetailedActivity/{detailedActivityId}/{countryPlanId}")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> DeleteDetailedActivity(int detailedActivityId, int countryPlanId)
        {
            var request = new DeleteDetailedActivityCommand()
            {
                DetailedActivityId = detailedActivityId,
                CountryPlanId = countryPlanId,
                User = this.user,
            };

            var result = await this.deleteDetailedActivityValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.detailedActivityService.DeleteDetailedActivityAsync(request));
        }

        /// <summary>
        /// Get detailed activities for country.
        /// </summary>
        /// <param name="countryId">Country id.</param>
        /// <returns>Returns list of <see cref="DetailedActivityTypeViewModel"/> model.</returns>
        [HttpGet("GetDetailedActivityTypes/{countryId}")]
        [ProducesResponseType(typeof(ApiResponse<List<DetailedActivityTypeViewModel>>), 200)]
        public async Task<IActionResult> GetDetailedActivityTypes(int countryId)
        {
            var request = new GetDetailedActivityCommand()
            {
                CountryId = countryId,
                User = this.user,
            };

            var result = await this.getDetailedActivityValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.detailedActivityService.GetDetailedActivityTypesAsync(request));
        }

        /// <summary>
        /// Get detailed activities for country.
        /// </summary>
        /// <param name="request"> <see cref="CreateDetailedActivityTypeCommand"/> command request create detailed activity type.</param>
        /// <returns>Returns the newly created <see cref="DetailedActivityTypeViewModel"/> model.</returns>
        [HttpPost("CreateDetailedActivityType")]
        [ProducesResponseType(typeof(ApiResponse<DetailedActivityTypeViewModel>), 200)]
        public async Task<IActionResult> CreateDetailedActivityTypes([FromBody] CreateDetailedActivityTypeCommand request)
        {
            request.User = this.user;
            var result = await this.createDetailedActivityTypeValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.detailedActivityService.CreateDetailedActivityTypeAsync(request));
        }

        /// <summary>
        /// Edit detailed activity.
        /// </summary>
        /// <param name="request"> <see cref="EditDetailedActivityCommand"/> command request to edit detailed activity.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        [HttpPut("EditDetailedActivity")]
        [ProducesResponseType(typeof(ApiResponse<CompletePlanDetailsViewModel>), 200)]
        public async Task<IActionResult> EditDetailedActivity([FromBody] EditDetailedActivityCommand request)
        {
            request.User = this.user;
            var result = await this.editDetailedActivityValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.detailedActivityService.EditDetailedActivity(request));
        }
    }
}