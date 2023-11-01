// <copyright file="IExcelExportImportService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ServiceInterfaces
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using WHO.NAPHS.BusinessLogic.Features.Excel;
    using WHO.NAPHS.BusinessLogic.ViewModels.Excel;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Excel export and import service.
    /// </summary>
    public interface IExcelExportImportService : IBaseService
    {
        /// <summary>
        /// Import excel file.
        /// </summary>
        /// <param name="request"> <see cref="ImportExcelFileCommand"/> command request to import excel file.</param>
        /// <returns>Returns the technical area and indicator details.</returns>
        Task ImportExcelAsync(ImportExcelFileCommand request);

        /// <summary>
        /// Get list of sheets of excel file.
        /// </summary>
        /// <param name="request"> <see cref="GetExcelSheetDetailsCommand"/> command request to get excel file sheet details.</param>
        /// <returns>Returns the list of sheet names in excel file.</returns>
        Task<List<string>> GetSheetDetailsAsync(GetExcelSheetDetailsCommand request);

        /// <summary>
        /// Check if uploaded file is valid file or not.
        /// </summary>
        /// <param name="file"> File.</param>
        /// <returns>Returns a boolean representing the whether file is valid or not.</returns>
        Task<bool> IsValidExcelFileAsync(IFormFile file);

        /// <summary>
        /// Import excel data that are being parsed by the front end.
        /// </summary>
        /// <param name="request"> <see cref="ImportExcelDataCommand"/> command request to import excel parsed data from the front end.</param>
        /// <returns>Returns a boolean representing the result of the asynchronous operation.</returns>
        Task<bool> ImportExcelDataAsync(ImportExcelDataCommand request);

        /// <summary>
        /// Import excel data that are being parsed by the front end.
        /// </summary>
        /// <param name="request"> <see cref="UploadExcelFileCommand"/> command request to save file uploaded by user.</param>
        /// <returns>Returns planning tool id which represents id of the uploaded excel file.</returns>
        Task<int> UploadExcelFileAsync(UploadExcelFileCommand request);

        /// <summary>
        /// Import IHR recommendation data that are being parsed by the front end.
        /// </summary>
        /// <param name="request"> <see cref="ImportIHRDataCommand"/> command request to import excel parsed data from the front end..</param>
        /// <returns>Returns a boolean representing the result of the asynchronous operation.</returns>
        Task<bool> ImportIHRDataAsync(ImportIHRDataCommand request);

        /// <summary>
        /// Import IHR recommendation data that are being parsed by the front end.
        /// </summary>
        /// <param name="request"> <see cref="ImportNBWDataCommand"/> command request to import excel parsed data from the front end.</param>
        /// <returns>Returns a boolean representing the result of the asynchronous operation.</returns>
        Task<bool> ImportNBWDataAsync(ImportNBWDataCommand request);

        /// <summary>
        /// Import IHR recommendation data that are being parsed by the front end.
        /// </summary>
        /// <param name="request"> <see cref="UploadCustomNBWDataCommand"/> command request to upload custom nbw data parsed from the front end.</param>
        /// <returns>Returns list of imported <see cref="NBWRecommendationsViewModel"/> model.</returns>
        Task<List<NBWRecommendationsViewModel>> UploadCustomNBWDataAsync(UploadCustomNBWDataCommand request);

        /// <summary>
        /// Get history of files that are uploaded by type and country id.
        /// </summary>
        /// <param name="request"> <see cref="GetImportActionHistoryCommand"/> command request to get action import history.</param>
        /// <returns>Returns list of <see cref="GetActionImportHistoryViewModel"/> model.</returns>
        Task<List<GetActionImportHistoryViewModel>> GetActionImportHistoryAsync(GetImportActionHistoryCommand request);

        /// <summary>
        /// Get latest uploaded file for particular excel type.
        /// </summary>
        /// <param name="request"> <see cref="GetImportActionHistoryCommand"/> command request to get latest uploaded file.</param>
        /// <returns>Returns <see cref="FileContentResult"/> latest uploaded file from system settings.</returns>
        Task<DownloadExcelFileViewModel> DownloadLatestExcelFileAsync(GetImportActionHistoryCommand request);

        /// <summary>
        /// Get latest uploaded file for particular excel type.
        /// </summary>
        /// <param name="request"> <see cref="DownloadFileCommand"/> command request to get uploaded file.</param>
        /// <returns>Returns <see cref="FileContentResult"/> uploaded file from system settings.</returns>
        Task<DownloadExcelFileViewModel> DownloadExcelFileAsync(DownloadFileCommand request);

        /// <summary>
        /// Get data for system settings dashboard.
        /// </summary>
        /// <param name="countryId">Country id.</param>
        /// <returns>Returns data for system settings dashboard.</returns>
        Task<Dictionary<ExcelTypes, GetActionImportHistoryViewModel>> GetSystemSettingsDashboardDataAsync(int? countryId);

        /// <summary>
        /// Create custom IHR action.
        /// </summary>
        /// <param name="request"> <see cref="CreateIHRActionCommand"/> command request to create IHR action.</param>
        /// <returns>Returns the newly created <see cref="IHRRecommendationsViewModel"/> model.</returns>
        Task<IHRRecommendationsViewModel> CreateCustomIHRActionAsync(CreateIHRActionCommand request);

        /// <summary>
        /// Create custom IHR action.
        /// </summary>
        /// <param name="request"> <see cref="CreateNBWActionCommand"/> command request to create NBW action.</param>
        /// <returns>Returns the newly created <see cref="NBWRecommendationsViewModel"/> model.</returns>
        Task<NBWRecommendationsViewModel> CreateCustomNBWActionAsync(CreateNBWActionCommand request);

        /// <summary>
        /// Create custom IHR action.
        /// </summary>
        /// <param name="request"><see cref="GetCustomNBWActionsForPlanCommand"/> command request to get custom NBW Recommendations for plan.</param>
        /// <returns>Returns list of <see cref="NBWRecommendationsViewModel"/> model for plan.</returns>
        Task<List<NBWRecommendationsViewModel>> GetCustomNBWActionsForPlanAsync(GetCustomNBWActionsForPlanCommand request);
    }
}