// <copyright file="ExcelExportImportService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8600, CS8602, CS8604

namespace WHO.NAPHS.Infrastructure.ServiceImplementation
{
    using System.Data;
    using System.Threading.Tasks;
    using AutoMapper;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using Newtonsoft.Json;
    using WHO.NAPHS.BusinessLogic.Features.Excel;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.ViewModels.Excel;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;
    using WHO.NAPHS.Core.Constant;
    using WHO.NAPHS.Core.Wrappers;
    using WHO.NAPHS.Infrastructure.Helper;
    using WHO.NAPHS.Infrastructure.Models.DatabaseContext;
    using WHO.NAPHS.Infrastructure.Models.Excel;
    using WHO.NAPHS.Infrastructure.Models.Plans;

    /// <summary>
    /// Implements the <see cref="IExcelExportImportService"/> service interface.
    /// </summary>
    public class ExcelExportImportService : IExcelExportImportService
    {
        private readonly ApplicationDbContext context;
        private readonly IStringLocalizer<Resources> localizer;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelExportImportService"/> class.
        /// </summary>
        /// /// <param name="context"> Database context.</param>
        /// <param name="localizer"> Localizer.</param>
        /// <param name="mapper"> Mapper.</param>
        public ExcelExportImportService(ApplicationDbContext context, IStringLocalizer<Resources> localizer, IMapper mapper)
        {
            this.context = context;
            this.localizer = localizer;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task ImportExcelAsync(ImportExcelFileCommand request)
        {
            switch (request.ExcelType)
            {
                case ExcelTypes.JEEAssessments:
                case ExcelTypes.ESPARAssessments:
                    {
                        await this.ImportAssessmentsTemplateAsync(request);

                        break;
                    }
            }
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetSheetDetailsAsync(GetExcelSheetDetailsCommand request)
        {
            if (request.File == null)
            {
                throw new ApiException(this.localizer["InvalidFile"]);
            }

            List<string> sheetNames = new ();
            using Stream stream = request.File.OpenReadStream();

            using (SpreadsheetDocument document = SpreadsheetDocument.Open(stream, false))
            {
                WorkbookPart workbookPart = document.WorkbookPart;
                IEnumerable<Sheet> sheets = workbookPart.Workbook.Descendants<Sheet>();

                foreach (Sheet sheet in sheets)
                {
                    sheetNames.Add(sheet.Name);
                }
            }

            return await Task.FromResult(sheetNames);
        }

        /// <inheritdoc/>
        public async Task<bool> IsValidExcelFileAsync(IFormFile file)
        {
            using Stream stream = file.OpenReadStream();
            bool status = false;
            try
            {
                using SpreadsheetDocument document = SpreadsheetDocument.Open(stream, false);

                // If the document opens without exceptions, it's a valid Excel file.
                status = true;
            }
            catch (Exception)
            {
                // An exception occurred, indicating the file is not a valid Excel file.
                throw new ApiException($"{this.localizer["InvalidFile"]}");
            }

            return await Task.FromResult(status);
        }

        /// <inheritdoc/>
        public async Task<bool> ImportExcelDataAsync(ImportExcelDataCommand request)
        {
            List<AreaIndicatorViewModel> areaIndicators = new ();
            List<TechnicalAreaViewModel> technicalAreas = new ();
            int parentIndexCounter = 1;

            foreach (ExcelImportTechnicalAreaViewModel item in request.TechnicalAreas)
            {
                technicalAreas.Add(new ()
                {
                    AreaCode = item.AreaCode,
                    AreaCodeId = item.AreaCodeId,
                    Index = parentIndexCounter,
                    ParentIndex = parentIndexCounter,
                    Value = item.TechnicalArea,
                    OriginalValue = item.TechnicalArea,
                });

                foreach (var areaIndicator in item.TechnicalAreaIndicators)
                {
                    areaIndicators.Add(new ()
                    {
                        IndicatorCode = areaIndicator.IndicatorCode,
                        IndicatorId = areaIndicator.IndicatorId,
                        ParentIndex = parentIndexCounter,
                        Value = areaIndicator.IndicatorName,
                    });
                }

                parentIndexCounter++;
            }

            return (await this.SaveTechnicalAreasAndIndicatorsAsync(
              request.User.UserId,
              areaIndicators,
              technicalAreas,
              request.ExcelType)) > 0;
        }

        /// <inheritdoc/>
        public async Task<int> UploadExcelFileAsync(UploadExcelFileCommand request)
        {
            switch (request.ExcelType)
            {
                case ExcelTypes.Custom:
                    request.CountryId = null;

                    var plan = await this.context.CountryPlans.FirstOrDefaultAsync(x => x.CountryPlanId == request.CountryPlanId && x.PlanTypeId == (int)Core.Common.PlanType.Operational) ??
                        throw new ApiException(this.localizer["NotAnOperationalPlan"]);

                    break;

                case ExcelTypes.JEEAssessments or ExcelTypes.ESPARAssessments:
                    request.CountryPlanId = null;
                    request.CountryId = null;
                    break;

                default:
                    request.CountryPlanId = null;
                    break;
            }

            var file = request.File;
            string folderPath = CommonSettings.ApplicationSettings!.ExcelImportFilePath;

            string fileName = DateTime.Now.Ticks.ToString();
            var extension = Path.GetExtension(file.FileName);

            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(Path.Combine(folderPath));
            }

            var filePath = Path.Combine(folderPath, fileName + extension);
            using (var fileStreams = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }

            var planningTool = new PlanningTools(file.FileName, fileName + extension, request.ExcelType, request.CountryId, request.CountryPlanId);
            await this.context.PlanningTools.AddAsync(planningTool);

            if (await this.context.SaveChangesAsync() > 0)
            {
                return planningTool.PlanningToolId;
            }

            return -1;
        }

        /// <inheritdoc/>
        public async Task<bool> ImportIHRDataAsync(ImportIHRDataCommand request)
        {
            try
            {
                foreach (var data in request.IHRRecommendations)
                {
                    await this.context.IHRRecommendations.AddAsync(
                        new IHRRecommendations(
                        data.IndicatorId.Trim(), data.BenchMark.Trim(), data.Objectives.Trim(), data.Capacity.Trim(), data.PreviousScore, data.TargetScore, data.Actions.Trim(), null, null));
                }

                return await this.context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                await this.DeletePlanningTool(request.PlanningToolId);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ImportNBWDataAsync(ImportNBWDataCommand request)
        {
            try
            {
                foreach (var data in request.NBWRecommendations)
                {
                    data.Impact ??= ActionImpact.Medium;
                    data.Feasibility ??= ActionFeasibility.Medium;

                    await this.context.NBWRecommendations.AddAsync(
                        new NBWRecommendations(data.Source.Trim(), data.SetOfIndicator.Trim(), data.GroupingInRoadMap.Trim(), data.TechnicalArea.Trim(), data.IndicatorId.Trim(), data.IndicatorCode.Trim(), data.IndicatorName.Trim(), data.Objective.Trim(), data.StrategicAction.Trim(), data.DetailedActivity.Trim(), (int)data.Feasibility, (int)data.Impact, data.StartDate, data.EndDate, data.ResponsibleAuthority, request.CountryId, null, request.PlanningToolId, data.Tags));
                }

                return await this.context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                await this.DeletePlanningTool(request.PlanningToolId);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<List<NBWRecommendationsViewModel>> UploadCustomNBWDataAsync(UploadCustomNBWDataCommand request)
        {
            try
            {
                // check if plan is operational
                var countryPlan = this.context.CountryPlans.FirstOrDefault(x => x.CountryPlanId == request.CountryPlanId)
                    ?? throw new ApiException(this.localizer["InvalidPlanId"]);

                var nbwRecommendations = new List<NBWRecommendations>();
                foreach (var data in request.NBWRecommendations)
                {
                    data.Impact ??= ActionImpact.Medium;
                    data.Feasibility ??= ActionFeasibility.Medium;

                    var nbwRecommendationObj = new NBWRecommendations(data.Source.Trim(), data.SetOfIndicator.Trim(), data.GroupingInRoadMap.Trim(), data.TechnicalArea.Trim(), data.IndicatorId.Trim(), data.IndicatorCode.Trim(), data.IndicatorName.Trim(), data.Objective.Trim(), data.StrategicAction.Trim(), data.DetailedActivity.Trim(), (int)data.Feasibility, (int)data.Impact, data.StartDate, data.EndDate, data.ResponsibleAuthority, countryPlan.CountryId, request.CountryPlanId, request.PlanningToolId, data.Tags);
                    nbwRecommendations.Add(nbwRecommendationObj);
                }

                await this.context.NBWRecommendations.AddRangeAsync(nbwRecommendations);
                await this.context.SaveChangesAsync();

                return this.mapper.Map<List<NBWRecommendationsViewModel>>(nbwRecommendations);
            }
            catch (Exception)
            {
                await this.DeletePlanningTool(request.PlanningToolId);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<List<GetActionImportHistoryViewModel>> GetActionImportHistoryAsync(GetImportActionHistoryCommand request)
        {
            List<GetActionImportHistoryViewModel> actionImportHistory = new ();

            if (request.ExcelType == ExcelTypes.NBWAssessments)
            {
                actionImportHistory = await this.context.PlanningTools.Where(x => x.ExcelTypeId == request.ExcelType && x.CountryId == request.CountryId)
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(e => new GetActionImportHistoryViewModel()
                    {
                        SheetId = e.PlanningToolId,
                        SheetName = e.FileName,
                        UploadTime = e.CreatedAt,
                    }).ToListAsync();
            }
            else
            {
                actionImportHistory = await this.context.PlanningTools.Where(x => x.ExcelTypeId == request.ExcelType)
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(e => new GetActionImportHistoryViewModel()
                    {
                        SheetId = e.PlanningToolId,
                        SheetName = e.FileName,
                        UploadTime = e.CreatedAt,
                    }).ToListAsync();
            }

            return actionImportHistory;
        }

        /// <inheritdoc/>
        public async Task<DownloadExcelFileViewModel> DownloadLatestExcelFileAsync(GetImportActionHistoryCommand request)
        {
            PlanningTools planningTool;
            if (request.ExcelType == ExcelTypes.NBWAssessments)
            {
                planningTool = await this.context.PlanningTools.Where(x => x.ExcelTypeId == request.ExcelType && x.CountryId == request.CountryId)
                    .OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
            }
            else
            {
                planningTool = await this.context.PlanningTools.Where(x => x.ExcelTypeId == request.ExcelType)
                    .OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
            }

            if (planningTool == null)
            {
                throw new ApiException("No files have been uploaded for this assessment.");
            }

            return await this.DownloadFileById(planningTool);
        }

        /// <inheritdoc/>
        public async Task<DownloadExcelFileViewModel> DownloadExcelFileAsync(DownloadFileCommand request)
        {
            var planningTool = await this.context.PlanningTools.FirstOrDefaultAsync(x => x.PlanningToolId == request.PlanningToolId)
                    ?? throw new ApiException(this.localizer["InvalidPlanningToolId"]);

            return await this.DownloadFileById(planningTool);
        }

        /// <inheritdoc/>
        public async Task<Dictionary<ExcelTypes, GetActionImportHistoryViewModel>> GetSystemSettingsDashboardDataAsync(int? countryId)
        {
            Dictionary<ExcelTypes, GetActionImportHistoryViewModel> dict = new ();

            var ihrData = await this.GetActionImportHistoryAsync(new ()
            {
                ExcelType = ExcelTypes.IHRBenchmarks,
            });

            var jeeData = await this.GetActionImportHistoryAsync(new ()
            {
                ExcelType = ExcelTypes.JEEAssessments,
            });

            var esparData = await this.GetActionImportHistoryAsync(new ()
            {
                ExcelType = ExcelTypes.ESPARAssessments,
            });

            var nbwData = new List<GetActionImportHistoryViewModel>();
            if (countryId != null)
            {
                nbwData = await this.GetActionImportHistoryAsync(new ()
                {
                    ExcelType = ExcelTypes.NBWAssessments,
                    CountryId = countryId,
                });
            }

            dict.Add(
                ExcelTypes.IHRBenchmarks,
                ihrData.Count > 0 ? new () { SheetId = ihrData[0].SheetId, SheetName = ihrData[0].SheetName, UploadTime = ihrData[0].UploadTime, CurrentVersion = ihrData.Count, } : null);

            dict.Add(
                ExcelTypes.JEEAssessments,
                jeeData.Count > 0 ? new () { SheetId = jeeData[0].SheetId, SheetName = jeeData[0].SheetName, UploadTime = jeeData[0].UploadTime, CurrentVersion = jeeData.Count, } : null);

            dict.Add(
                ExcelTypes.ESPARAssessments,
                esparData.Count > 0 ? new () { SheetId = esparData[0].SheetId, SheetName = esparData[0].SheetName, UploadTime = esparData[0].UploadTime, CurrentVersion = esparData.Count, } : null);

            dict.Add(
                ExcelTypes.NBWAssessments,
                nbwData.Count > 0 ? new () { SheetId = nbwData[0].SheetId, SheetName = nbwData[0].SheetName, UploadTime = nbwData[0].UploadTime, CurrentVersion = nbwData.Count, } : null);

            return dict;
        }

        /// <inheritdoc/>
        public async Task<IHRRecommendationsViewModel> CreateCustomIHRActionAsync(CreateIHRActionCommand request)
        {
            var ihrRecommendationObj = new IHRRecommendations(request.IndicatorId, request.BenchMark, request.Objectives, request.Capacity, request.PreviousScore, request.TargetScore, request.Actions, request.CountryId, request.CountryPlanId);
            await this.context.IHRRecommendations.AddAsync(ihrRecommendationObj);
            await this.context.SaveChangesAsync();
            return this.mapper.Map<IHRRecommendationsViewModel>(ihrRecommendationObj);
        }

        /// <inheritdoc/>
        public async Task<NBWRecommendationsViewModel> CreateCustomNBWActionAsync(CreateNBWActionCommand request)
        {
            // check if plan is operational
            var countryPlan = this.context.CountryPlans.FirstOrDefault(x => x.CountryPlanId == request.CountryPlanId)
                ?? throw new ApiException(this.localizer["InvalidPlanId"]);

            var nbwRecommendationObj = new NBWRecommendations(request.Source, request.SetOfIndicator, request.GroupingInRoadMap, request.TechnicalArea, request.IndicatorId, request.IndicatorCode, request.IndicatorName, request.Objective, request.StrategicAction, request.DetailedActivity, request.Feasibility, request.Impact, request.StartDate, request.EndDate, request.ResponsibleAuthority, countryPlan.CountryId, request.CountryPlanId, null, request.Tags);
            await this.context.NBWRecommendations.AddAsync(nbwRecommendationObj);
            await this.context.SaveChangesAsync();
            return this.mapper.Map<NBWRecommendationsViewModel>(nbwRecommendationObj);
        }

        /// <inheritdoc/>
        public async Task<List<NBWRecommendationsViewModel>> GetCustomNBWActionsForPlanAsync(GetCustomNBWActionsForPlanCommand request)
        {
            return this.mapper.Map<List<NBWRecommendationsViewModel>>(await this.context.NBWRecommendations.Where(x => x.CountryPlanId == request.CountryPlanId).ToListAsync());
        }

        /// <summary>
        /// Delete Planning tool along with stored file in case of DATA Save failure.
        /// </summary>
        /// <param name="planningToolId">Id of planning tool that needs to be deleted.</param>
        /// <returns>Returns the async operation.</returns>
        private async Task DeletePlanningTool(int planningToolId)
        {
            var planningTool = await this.context.PlanningTools.FirstOrDefaultAsync(x => x.PlanningToolId == planningToolId);
            if (planningTool != null)
            {
                string folderPath = CommonSettings.ApplicationSettings!.ExcelImportFilePath;
                var filePath = Path.Combine(folderPath, planningTool.FilePath);
                FileInfo file = new FileInfo(filePath);
                if (file.Exists)
                {
                    file.Delete();
                }

                this.context.PlanningTools.Remove(planningTool);
                await this.context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Download excel file by it's id.
        /// </summary>
        /// <param name="planningTool"><see cref="PlanningTool"/> model that represents file that is to be downloaded.</param>
        /// <returns>Returns <see cref="DownloadExcelFileViewModel"/> model.</returns>
        private async Task<DownloadExcelFileViewModel> DownloadFileById(PlanningTools planningTool)
        {
            string filePath = $"{CommonSettings.ApplicationSettings.ExcelImportFilePath}/{planningTool.FilePath}";

            DownloadExcelFileViewModel model = new ();

            if (File.Exists(filePath))
            {
                var fileBytes = await File.ReadAllBytesAsync(filePath);

                string contentType = new FileExtensionContentTypeProvider().Mappings
                        .FirstOrDefault(mapping => mapping.Key.Equals(Path.GetExtension(filePath), StringComparison.OrdinalIgnoreCase))
                        .Value;

                model.FileBytes = fileBytes;
                model.ContentType = contentType;
                model.FileName = planningTool.FileName;
            }
            else
            {
                throw new ApiException($"No file found with file name: {planningTool.FileName}");
            }

            return model;
        }

        /// <summary>
        /// Import JEE/SPAR Assessments.
        /// </summary>
        /// <param name="model"> <see cref="ImportExcelFileCommand"/> command to import JEE assessments.</param>
        /// <returns> Returns the async operation.</returns>
        private async Task ImportAssessmentsTemplateAsync(ImportExcelFileCommand model)
        {
            if (model.File == null)
            {
                throw new ApiException(this.localizer["InvalidFile"]);
            }

            using Stream stream = model.File.OpenReadStream();

            var excelMappingRecords = await this.context.ExcelMappings
                .AsNoTracking()
                .Where(x => x.ExcelType.Equals(model.ExcelType))
                .ToListAsync();

            string sheetName = (!string.IsNullOrEmpty(model.SheetName) ? model.SheetName : excelMappingRecords.FirstOrDefault()?.SheetName) ?? throw new ApiException(this.localizer["NoExcelSheetAvailable"]);

            bool isFirstRowColumnName = model.IsFirstRowColumnName ? model.IsFirstRowColumnName : true;

            using SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(stream, false);
            WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
            Sheet sheet = workbookPart.Workbook.Descendants<Sheet>()
                .FirstOrDefault(s => s.Name == sheetName);

            List<TechnicalAreaViewModel> technicalAreas = new ();
            List<AreaIndicatorViewModel> areaIndicators = new ();
            if (sheet != null)
            {
                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                int? parentIndex = 1;
                int rowCounter = 1;
                string columnName = (excelMappingRecords != null
                                        && excelMappingRecords.Any()
                                        && excelMappingRecords.FirstOrDefault(x => x.MappingType == MappingTypes.TechnicalArea) != null)
                                    ? excelMappingRecords.FirstOrDefault(x => x.MappingType == MappingTypes.TechnicalArea).ExcelColumn
                                    : CommonSettings.ApplicationSettings.TechnicalAreaExcelColumn;

                foreach (Row row in sheetData.Elements<Row>())
                {
                    if (isFirstRowColumnName && rowCounter == 1)
                    {
                        rowCounter++;
                        continue;
                    }

                    Cell cell = row.Elements<Cell>()
                                   .FirstOrDefault(c => this.GetColumnName(c.CellReference) == columnName);

                    string? cellValue = null;
                    bool isNull = false;

                    if (cell != null)
                    {
                        cellValue = this.GetCellValue(cell, workbookPart);
                        parentIndex = rowCounter;
                    }
                    else
                    {
                        // Cell is empty.
                        isNull = true;
                    }

                    string? originalValue = cellValue;
                    string areaCode = CommonMethods.GetTechnicalAreaCode(cellValue);
                    string? areaCodeId = CommonMethods.GetTechnicalAreaCodeId(cellValue);
                    string? areaName = CommonMethods.GetTechnicalAreaName(cellValue, areaCode, areaCodeId);
                    technicalAreas.Add(
                        new TechnicalAreaViewModel(
                            rowCounter,
                            originalValue,
                            areaName,
                            areaCode,
                            areaCodeId,
                            isNull ? parentIndex : null));

                    rowCounter++;
                }

                string indicatorColumnName = (excelMappingRecords != null
                                                    && excelMappingRecords.Any()
                                                    && excelMappingRecords.FirstOrDefault(x => x.MappingType == MappingTypes.Indicators) != null)
                                                ? excelMappingRecords
                                                    .FirstOrDefault(x => x.MappingType == MappingTypes.Indicators).ExcelColumn
                                                : CommonSettings.ApplicationSettings.IndicatorExcelColumn;

                foreach (var area in technicalAreas.Where(c => c.ParentIndex == null))
                {
                    var areaDetails = technicalAreas
                        .Where(x => x.Index == area.Index || x.ParentIndex == area.Index).ToList();

                    if (areaDetails != null && areaDetails.Any())
                    {
                        int pIndex = areaDetails.FirstOrDefault().Index;
                        string areaValue = areaDetails.FirstOrDefault().OriginalValue;
                        foreach (var item in areaDetails)
                        {
                            Cell cell = worksheetPart.Worksheet.Descendants<Cell>()
                                .FirstOrDefault(c => c.CellReference == $"{indicatorColumnName}{item.Index}");

                            if (cell != null)
                            {
                                var cellValue = this.GetCellValue(cell, workbookPart);

                                string? indicatorId = CommonMethods.FindIndicatorId(areaValue, cellValue);
                                string? indicatorCode = CommonMethods.GetIndicatorCode(cellValue, indicatorId);
                                string? indicatorName = CommonMethods.GetIndicatorName(cellValue, indicatorId, indicatorCode);

                                areaIndicators.Add(new AreaIndicatorViewModel()
                                {
                                    Value = indicatorName,
                                    IndicatorId = indicatorId,
                                    ParentIndex = pIndex,
                                    IndicatorCode = indicatorCode,
                                });
                            }
                        }
                    }
                }

                if (technicalAreas != null && technicalAreas.Any() && areaIndicators != null && areaIndicators.Any())
                {
                    await this.SaveTechnicalAreasAndIndicatorsAsync(
                        model.User.UserId,
                        areaIndicators,
                        technicalAreas.Where(x => x.ParentIndex == null).ToList(),
                        model.ExcelType);
                }
            }
            else
            {
                throw new ApiException($"{this.localizer["NoExcelSheetAvailable"]} {sheetName}");
            }
        }

        /// <summary>
        /// Save technical areas.
        /// </summary>
        /// <param name="userId"> User id.</param>
        /// <param name="indicators"> List of <see cref="TechnicalAreaIndicatorViewModel"/> for technical areas.</param>
        /// <param name="technicalAreas"> List of <see cref="TechnicalAreaViewModel"/> models.</param>
        /// <param name="excelType"> <see cref="ExcelType"/> enum.</param>
        /// <returns> Returns the affected rows.</returns>
        private async Task<int> SaveTechnicalAreasAndIndicatorsAsync(int userId, List<AreaIndicatorViewModel> indicators, List<TechnicalAreaViewModel> technicalAreas, ExcelTypes excelType)
        {
            List<SqlParameter> parameters = new ()
            {
                new SqlParameter() { ParameterName = "@TechnicalAreas", Value = JsonConvert.SerializeObject(technicalAreas) },
                new SqlParameter() { ParameterName = "@AreaIndicators", Value = JsonConvert.SerializeObject(indicators) },
                new SqlParameter() { ParameterName = "@SourceId", Value = (int)excelType },
                new SqlParameter() { ParameterName = "@UserId", Value = userId },
            };

            int affectedRows = await this.context.ExecuteNonQueryAsync(
                CommandType.StoredProcedure,
                "sp_SaveTechnicalAreaAndIndicator",
                parameters,
                180);

            return affectedRows;
        }

        /// <summary>
        /// Get column name.
        /// </summary>
        /// <param name="cellReference"> Cell ref.</param>
        /// <returns> Returns the column name.</returns>
        private string GetColumnName(string cellReference)
        {
            // Remove digits from the cell reference to get the column name
            return new string(cellReference.Where(c => !char.IsDigit(c)).ToArray());
        }

        /// <summary>
        /// Get the cell value.
        /// </summary>
        /// <param name="cell"> Cell.</param>
        /// <param name="workbookPart"> <see cref="WorkbookPart"/> Workbook part.</param>
        /// <returns> Returns the cell value.</returns>
        private string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            string value = cell.InnerText;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                int sharedStringIndex = int.Parse(value);
                SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;
                return sharedStringPart.SharedStringTable.Elements<SharedStringItem>().ElementAt(sharedStringIndex).InnerText;
            }

            return value;
        }
    }
}
