// <copyright file="ExcelController.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.API.Controllers
{
    using FluentValidation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.API.Helper;
    using WHO.NOK.BusinessLogic.Features.Excel;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.BusinessLogic.ViewModels.Excel;
    using WHO.NOK.BusinessLogic.ViewModels.UserClaims;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Common.Resources;
    using WHO.NOK.Core.ResponseMiddleware;
    using WHO.NOK.Core.Wrappers;

    /// <summary>
    /// Excel controller to perform the excel related operations.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class ExcelController : BaseController
    {
        private readonly IExcelExportImportService excelService;
        private readonly IValidator<GetExcelSheetDetailsCommand> getExcelSheetValidator;
        private readonly IValidator<ImportExcelFileCommand> importFileValidator;
        private readonly IValidator<ImportExcelDataCommand> importExcelDataValidator;
        private readonly IValidator<UploadExcelFileCommand> uploadExcelFileValidator;
        private readonly IValidator<ImportIHRDataCommand> importIHRDataValidator;
        private readonly IValidator<ImportNBWDataCommand> importNBWDataValidator;
        private readonly IValidator<UploadCustomNBWDataCommand> uploadCustomNBWDataValidator;
        private readonly IValidator<GetImportActionHistoryCommand> getImportActionHistoryValidator;
        private readonly IValidator<CreateIHRActionCommand> createIHRActionValidator;
        private readonly IValidator<CreateNBWActionCommand> createNBWActionValidator;
        private readonly IValidator<DownloadFileCommand> downloadFileValidator;
        private readonly UserClaimsViewModel? user;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IStringLocalizer<Resources> localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelController"/> class.
        /// </summary>
        /// <param name="excelService"> <see cref="IExcelExportImportService"/> service.</param>
        /// <param name="localizer">Localizer.</param>
        /// <param name="contextAccessor"> <see cref="IHttpContextAccessor"/> Context accessor.</param>
        /// <param name="getExcelSheetValidator"> Validate <see cref="GetExcelSheetDetailsCommand"/> command. </param>
        /// <param name="importFileValidator"> Validate <see cref="ImportExcelFileCommand"/> command. </param>
        /// <param name="importExcelDataValidator"> Validate <see cref="ImportExcelDataCommand"/> command. </param>
        /// <param name="importIHRDataValidator"> Validate <see cref="ImportIHRDataCommand"/> command. </param>
        /// <param name="importNBWDataValidator"> Validate <see cref="ImportNBWDataCommand"/> command. </param>
        /// <param name="uploadExcelFileValidator"> Validate <see cref="UploadExcelFileCommand"/> command. </param>
        /// <param name="getImportActionHistoryValidator"> Validate <see cref="GetImportActionHistoryCommand"/> command. </param>
        /// <param name="createIHRActionValidator"> Validate <see cref="CreateIHRActionCommand"/> command. </param>
        /// <param name="createNBWActionValidator"> Validate <see cref="CreateNBWActionCommand"/> command. </param>
        /// <param name="uploadCustomNBWDataValidator"> Validate <see cref="UploadCustomNBWDataCommand"/> command. </param>
        /// <param name="downloadFileValidator"> Validate <see cref="DownloadFileCommand"/> command. </param>
        public ExcelController(
            IExcelExportImportService excelService,
            IStringLocalizer<Resources> localizer,
            IHttpContextAccessor contextAccessor,
            IValidator<GetExcelSheetDetailsCommand> getExcelSheetValidator,
            IValidator<ImportExcelFileCommand> importFileValidator,
            IValidator<ImportExcelDataCommand> importExcelDataValidator,
            IValidator<ImportIHRDataCommand> importIHRDataValidator,
            IValidator<ImportNBWDataCommand> importNBWDataValidator,
            IValidator<UploadExcelFileCommand> uploadExcelFileValidator,
            IValidator<GetImportActionHistoryCommand> getImportActionHistoryValidator,
            IValidator<CreateIHRActionCommand> createIHRActionValidator,
            IValidator<CreateNBWActionCommand> createNBWActionValidator,
            IValidator<UploadCustomNBWDataCommand> uploadCustomNBWDataValidator,
            IValidator<DownloadFileCommand> downloadFileValidator)
        {
            this.getExcelSheetValidator = getExcelSheetValidator;
            this.excelService = excelService;
            this.contextAccessor = contextAccessor;
            this.importFileValidator = importFileValidator;
            this.importExcelDataValidator = importExcelDataValidator;
            this.importIHRDataValidator = importIHRDataValidator;
            this.importNBWDataValidator = importNBWDataValidator;
            this.uploadExcelFileValidator = uploadExcelFileValidator;
            this.getImportActionHistoryValidator = getImportActionHistoryValidator;
            this.createIHRActionValidator = createIHRActionValidator;
            this.createNBWActionValidator = createNBWActionValidator;
            this.uploadCustomNBWDataValidator = uploadCustomNBWDataValidator;
            this.downloadFileValidator = downloadFileValidator;
            this.localizer = localizer;
            this.user = this.contextAccessor.HttpContext!.User.GetUser();
        }

        /// <summary>
        /// Get sheet details of the excel file.
        /// </summary>
        /// <param name="file"> File.</param>
        /// <returns> Returns the list of excel sheets of file.</returns>
        [HttpPost("GetExcelSheetDetails")]
        [ProducesResponseType(typeof(ApiResponse<List<string>>), 200)]
        public async Task<IActionResult> GetExcelSheetDetails(IFormFile file)
        {
            var model = new GetExcelSheetDetailsCommand()
            {
                User = this.user,
                File = file,
            };

            var result = await this.getExcelSheetValidator.ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.excelService.GetSheetDetailsAsync(model));
        }

        /// <summary>
        /// Import excel file.
        /// </summary>
        /// <param name="excelType"> <see cref="ExcelTypes"/> enum.</param>
        /// <param name="sheetName"> Sheet name.</param>
        /// <param name="isFirstRowColumnName"> is First row column name.</param>
        /// <param name="file"> File.</param>
        /// <returns> Returns the technical area and indicator details.</returns>
        [HttpPost("ImportExcel/{excelType}")]
        [ProducesResponseType(typeof(ApiResponse<>), 200)]
        public async Task<IActionResult> ImportExcelFile(ExcelTypes excelType, string? sheetName, bool isFirstRowColumnName, IFormFile file)
        {
            var model = new ImportExcelFileCommand()
            {
                ExcelType = excelType,
                File = file,
                IsFirstRowColumnName = isFirstRowColumnName,
                SheetName = sheetName,
                User = this.user,
            };

            var result = await this.importFileValidator.ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            await this.excelService.ImportExcelAsync(model);
            return this.Ok();
        }

        /// <summary>
        /// Import excel data that are being parsed by the front end.
        /// </summary>
        /// <param name="file"> Command request to uploaded excel file by user.</param>
        /// <param name="excelType"> <see cref="ExcelType"/> enum that represents type of excel file that has been uploaded.</param>
        /// <param name="countryId"> Country id for which data is added.</param>
        /// <param name="countryPlanId"> Country plan id for which data is added.</param>
        /// <returns>Returns planning tool id which represents id of the uploaded excel file.</returns>
        [HttpPost("UploadExcelFile/{excelType}")]
        [ProducesResponseType(typeof(ApiResponse<int>), 200)]
        public async Task<IActionResult> UploadExcelFile(IFormFile file, ExcelTypes excelType, [FromForm] int? countryId, [FromForm] int? countryPlanId)
        {
            UploadExcelFileCommand uploadExcelFileCommand = new ()
            {
                File = file,
                ExcelType = excelType,
                CountryId = countryId,
                CountryPlanId = countryPlanId,
                User = this.user,
            };

            var result = await this.uploadExcelFileValidator.ValidateAsync(uploadExcelFileCommand);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.excelService.UploadExcelFileAsync(uploadExcelFileCommand));
        }

        /// <summary>
        /// Import excel data that are being parsed by the front end.
        /// </summary>
        /// <param name="request"> <see cref="ImportExcelDataCommand"/> command request to import excel parsed data from the front end.</param>
        /// <returns>Returns a boolean representing the result of the asynchronous operation.</returns>
        [HttpPost("ImportExcelData/{excelType}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<IActionResult> ImportExcelData([FromBody] ImportExcelDataCommand request)
        {
            request.User = this.user;
            var result = await this.importExcelDataValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.excelService.ImportExcelDataAsync(request));
        }

        /// <summary>
        /// Import excel data that are being parsed by the front end.
        /// </summary>
        /// <param name="request"> <see cref="ImportIHRDataCommand"/> command request to import excel parsed data from the front end..</param>
        /// <returns>Returns a boolean representing the result of the asynchronous operation.</returns>
        [HttpPost("ImportIHRRecommendations")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<IActionResult> ImportIHRRecommendations([FromBody] ImportIHRDataCommand request)
        {
            request.User = this.user;
            var result = await this.importIHRDataValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.excelService.ImportIHRDataAsync(request));
        }

        /// <summary>
        /// Import excel data that are being parsed by the front end.
        /// </summary>
        /// <param name="request"> <see cref="ImportNBWDataCommand"/> command request to import excel parsed data from the front end.</param>
        /// <returns>Returns a boolean representing the result of the asynchronous operation.</returns>
        [HttpPost("ImportNBWRecommendations")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<IActionResult> ImportNBWRecommendations([FromBody] ImportNBWDataCommand request)
        {
            request.User = this.user;
            var result = await this.importNBWDataValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.excelService.ImportNBWDataAsync(request));
        }

        /// <summary>
        /// Import excel data that are being parsed by the front end.
        /// </summary>
        /// <param name="request"> <see cref="UploadCustomNBWDataCommand"/> command request to upload custom nbw data parsed from the front end.</param>
        /// <returns>Returns list of imported <see cref="NBWRecommendationsViewModel"/> model.</returns>
        [HttpPost("UploadCustomNBWActions")]
        [ProducesResponseType(typeof(ApiResponse<List<NBWRecommendationsViewModel>>), 200)]
        public async Task<IActionResult> UploadCustomNBWRecommendations([FromBody] UploadCustomNBWDataCommand request)
        {
            request.User = this.user;
            var result = await this.uploadCustomNBWDataValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            request.User = this.user;

            return this.Ok(await this.excelService.UploadCustomNBWDataAsync(request));
        }

        /// <summary>
        /// Get history of plans that has been uploaded.
        /// </summary>
        /// <param name="excelType"> <see cref="ExcelType"/> enum that represents type of excel file.</param>
        /// <param name="countryId"> Id of the country for which history is needed.</param>
        /// <returns>Returns list of <see cref="GetActionImportHistoryViewModel"/> model.</returns>
        [HttpGet("GetImportPlanHistory/{excelType}")]
        [ProducesResponseType(typeof(ApiResponse<List<GetActionImportHistoryViewModel>>), 200)]
        public async Task<IActionResult> GetUploadHistory(ExcelTypes excelType, [FromQuery] int? countryId)
        {
            var request = new GetImportActionHistoryCommand()
            {
                ExcelType = excelType,
                CountryId = countryId,
                User = this.user,
            };

            var result = await this.getImportActionHistoryValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.excelService.GetActionImportHistoryAsync(request));
        }

        /// <summary>
        /// Get latest uploaded file from system settings.
        /// <param name="planningToolId"> Planning tool id.</param>
        /// </summary>
        /// <returns>Returns <see cref="FileContentResult"/> uploaded file from system settings.</returns>
        [HttpGet("DownloadFile/{planningToolId}")]
        [ProducesResponseType(typeof(ApiResponse<FileContentResult>), 200)]
        public async Task<IActionResult> DownloadLatestExcelFile(int planningToolId)
        {
            var request = new DownloadFileCommand()
            {
                PlanningToolId = planningToolId,
                User = this.user,
            };

            var result = await this.downloadFileValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            var model = await this.excelService.DownloadExcelFileAsync(request);

            FileContentResult file = this.File(
                model.FileBytes,
                model.ContentType,
                model.FileName);

            return this.Ok(file);
        }

        /// <summary>
        /// Get latest uploaded file from system settings.
        /// </summary>
        /// <param name="excelType"> <see cref="ExcelType"/> enum that represents type of excel file.</param>
        /// <param name="countryId"> Country id.</param>
        /// <returns>Returns <see cref="FileContentResult"/> latest uploaded file from system settings.</returns>
        [HttpGet("GetLatestFile/{excelType}")]
        [ProducesResponseType(typeof(ApiResponse<FileContentResult>), 200)]
        public async Task<IActionResult> DownloadLatestExcelFile(ExcelTypes excelType, [FromQuery] int? countryId)
        {
            var request = new GetImportActionHistoryCommand()
            {
                ExcelType = excelType,
                CountryId = countryId,
                User = this.user,
            };

            var result = await this.getImportActionHistoryValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            var model = await this.excelService.DownloadLatestExcelFileAsync(request);

            FileContentResult file = this.File(
                model.FileBytes,
                model.ContentType,
                model.FileName);

            return this.Ok(file);
        }

        /// <summary>
        /// Get data for system settings dashboard.
        /// </summary>
        /// <param name="countryId">Country id if selected for nbw.</param>
        /// <returns>Returns data for system settings dashboard.</returns>
        [HttpGet("GetSystemSettingsDashboardData")]
        [ProducesResponseType(typeof(ApiResponse<Dictionary<ExcelTypes, GetActionImportHistoryViewModel>>), 200)]
        public async Task<IActionResult> GetSystemSettingsDashboardData([FromQuery] int? countryId)
        {
            if (countryId != null && countryId <= 0)
            {
                throw new ApiException(this.localizer["countryIdValidation"]);
            }

            return this.Ok(await this.excelService.GetSystemSettingsDashboardDataAsync(countryId));
        }

        /// <summary>
        /// Create Custom IHR action for plan.
        /// </summary>
        /// <param name="request"> <see cref="CreateIHRActionCommand"/> command request to create IHR action.</param>
        /// <returns>Returns the newly created <see cref="IHRRecommendationsViewModel"/> model.</returns>
        [HttpPost("CreateIHRAction")]
        [ProducesResponseType(typeof(ApiResponse<IHRRecommendationsViewModel>), 200)]
        public async Task<IActionResult> CreateCustomIHRAction([FromBody] CreateIHRActionCommand request)
        {
            request.User = this.user;
            var result = await this.createIHRActionValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.excelService.CreateCustomIHRActionAsync(request));
        }

        /// <summary>
        /// Return all custom nbw recommendations by plan id.
        /// </summary>
        /// <param name="countryPlanId">Country plan id.</param>
        /// <returns>Returns list of <see cref="NBWRecommendationsViewModel"/> model for plan.</returns>
        [HttpGet("GetNBWActionsForPlan/{countryPlanId}")]
        [ProducesResponseType(typeof(ApiResponse<List<NBWRecommendationsViewModel>>), 200)]
        public async Task<IActionResult> GetNBWActionsForPlan(int countryPlanId)
        {
            GetCustomNBWActionsForPlanCommand request = new ()
            {
                CountryPlanId = countryPlanId,
                User = this.user,
            };

            return this.Ok(await this.excelService.GetCustomNBWActionsForPlanAsync(request));
        }

        /// <summary>
        /// Create Custom NBW action for plan.
        /// </summary>
        /// <param name="request"> <see cref="CreateNBWActionCommand"/> command request to create NBW action.</param>
        /// <returns>Returns the newly created <see cref="NBWRecommendationsViewModel"/> model.</returns>
        [HttpPost("CreateNBWAction")]
        [ProducesResponseType(typeof(ApiResponse<NBWRecommendationsViewModel>), 200)]
        public async Task<IActionResult> CreateCustomNBWAction([FromBody] CreateNBWActionCommand request)
        {
            request.User = this.user;
            var result = await this.createNBWActionValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ApiException(result.GetAllValidationErrors());
            }

            return this.Ok(await this.excelService.CreateCustomNBWActionAsync(request));
        }
    }
}