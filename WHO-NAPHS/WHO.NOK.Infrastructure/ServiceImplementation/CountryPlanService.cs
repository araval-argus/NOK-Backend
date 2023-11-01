// <copyright file="CountryPlanService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8600, CS8602, CS8604, CS8603

namespace WHO.NAPHS.Infrastructure.ServiceImplementation
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using Newtonsoft.Json;
    using WHO.NAPHS.BusinessLogic.Features.CountryPlan;
    using WHO.NAPHS.BusinessLogic.Helper;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.ViewModels.Assessments;
    using WHO.NAPHS.BusinessLogic.ViewModels.Country;
    using WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NAPHS.BusinessLogic.ViewModels.StrategicAction;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;
    using WHO.NAPHS.Core.Constant;
    using WHO.NAPHS.Core.Wrappers;
    using WHO.NAPHS.Infrastructure.Helper;
    using WHO.NAPHS.Infrastructure.Models.Assessments;
    using WHO.NAPHS.Infrastructure.Models.DatabaseContext;
    using WHO.NAPHS.Infrastructure.Models.Plans;
    using Common = WHO.NAPHS.Core.Common;

    /// <summary>
    /// Implement the <see cref="ICountryPlanService"/> interface.
    /// </summary>
    public class CountryPlanService : ICountryPlanService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<Resources> localizer;
        private readonly ICommonService commonService;
        private readonly IAssessmentImportService assessmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryPlanService"/> class.
        /// </summary>
        /// <param name="context"> DB Context.</param>
        /// <param name="mapper"> Mapper.</param>
        /// <param name="localizer"> Localizer.</param>
        /// <param name="commonService"> Common service.</param>
        /// <param name="assessmentService"> Assessment service.</param>
        public CountryPlanService(
            ApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<Resources> localizer,
            ICommonService commonService,
            IAssessmentImportService assessmentService)
        {
            this.context = context;
            this.mapper = mapper;
            this.localizer = localizer;
            this.commonService = commonService;
            this.assessmentService = assessmentService;
        }

        /// <inheritdoc />
        public async Task<int> CreateNewStrategicPlanAsync(CreateStrategicPlanCommand model)
        {
            // Validate strategic plan
            await this.ValidateStrategicPlan(model.CountryId, model.StartDate);

            string countryISO = await this.commonService.GetCountryISOCodeFromIdAsync(model.CountryId);

            CountryPlan plan = new (model.CountryId, model.StartDate, model.EndDate, countryISO, string.Empty, (int)Common.PlanType.Strategic, (int)model.AssessmentType);
            plan.GeneratePlanCode();
            await this.context.CountryPlans.AddAsync(plan);
            await this.context.SaveChangesAsync();

            foreach (var indicator in model.Indicators)
            {
                await this.context.CountryPlanIndicators.AddAsync(new CountryPlanIndicator(
                                    indicator.TechnicalAreaIndicatorId,
                                    plan.CountryPlanId,
                                    indicator.Score,
                                    indicator.Goal));
            }

            await this.context.SaveChangesAsync();

            return plan.CountryPlanId;
        }

        /// <inheritdoc />
        public async Task<List<CountryPlanListViewModel>> GetAllPlansForCountryAsync(int countryId)
        {
            var plans = await this.context.CountryPlans
                .AsNoTracking()
                .Where(x => x.CountryId == countryId)
                .ToListAsync();

            return this.mapper.Map<List<CountryPlanListViewModel>>(plans);
        }

        /// <inheritdoc/>
        public async Task<InitialPlanDetailsViewModel> GetInitialPlanDetailsAsync(GetCountryInitialPlanDetailCommand request)
        {
            InitialPlanDetailsViewModel model = new ();

            List<SqlParameter> parameters = new ()
            {
                new SqlParameter() { ParameterName = "@CountryId", Value = request.CountryId },
                new SqlParameter() { ParameterName = "@PlanType", Value = request.PlanType },
            };

            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_GetInitialDataForPlanCreation",
                (ds) =>
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        dt.TableName = dt.Columns[0].ColumnName;
                    }

                    model.StrategicPlans = ds.Tables["StrategicPlansTable"].ConvertToList<CountryPlanViewModel>();
                    model.OperationalPlans = ds.Tables["OperationalPlanTable"].ConvertToList<CountryPlanViewModel>();
                    model.TechnicalAreas = ds.Tables["TechnicalAreasTable"].ConvertToList<TechnicalAreaViewModel>();
                    List<TechnicalAreaIndicatorViewModel> indicators = ds.Tables["TechnicalAreaIndicatorTable"].ConvertToList<TechnicalAreaIndicatorViewModel>();

                    foreach (TechnicalAreaViewModel area in model.TechnicalAreas)
                    {
                        var areaIndicators = indicators.Where(x => x.TechnicalAreaId == area.TechnicalAreaId);

                        if (areaIndicators != null && areaIndicators.Any())
                        {
                            area.TechnicalAreaIndicator = new List<TechnicalAreaIndicatorViewModel>();
                            area.TechnicalAreaIndicator.AddRange(areaIndicators);
                        }
                    }
                },
                parameters,
                1800);

            foreach (CountryPlanViewModel countryPlan in model.StrategicPlans)
            {
                countryPlan.TechnicalAreaIndicators = await this.context.CountryPlanIndicators.Where(
                    x => x.CountryPlanId == countryPlan.CountryPlanId && x.TechnicalAreaIndicatorId != null && !x.IsDeleted)
                .Select(x => x.TechnicalAreaIndicatorId !.Value).ToListAsync();
            }

            return model;
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> GetTechnicalAreaAndIndicatorsForPlanAsync(GetPlanDetailsCommand request)
        {
            var plan = this.context.CountryPlans.FirstOrDefaultAsync(x => x.CountryPlanId == request.PlanId && x.PlanTypeId == (int)Common.PlanType.Operational)
                ?? throw new ApiException(this.localizer["NotAnOperationalPlan"]);
            return await this.commonService.GetPlanDetailsAsync(request.PlanId, true, true, true, false, false);
        }

        /// <inheritdoc/>
        public async Task<List<IndicatorsWithScoreViewModel>> GetScoreForSelectedIndicatorsAsync(GetScoreForSelectedIndicatorsCommand request)
        {
            List<IndicatorsWithScoreViewModel> model = new ();

            if (request.AssessmentType == Common.AssessmentType.JEE)
            {
                var countryScores = await this.assessmentService.GetJEEScoreAsync(
                        await this.commonService.GetCountryISOCodeFromIdAsync(
                            request.CountryId));

                foreach (var indicator in request.Indicators)
                {
                    var indicatorDetail = countryScores.Scores?
                        .FirstOrDefault(x => x.Indicator == this.GetJEEIndicator(indicator));

                    IndicatorsWithScoreViewModel indicatorsWithScore = new ()
                    {
                        IndicatorCodeId = indicator.IndicatorCodeId,
                        TechnicalAreaIndicatorId = indicator.TechnicalAreaIndicatorId,
                        Score = 1,
                        Name = indicator.Name,
                        Goal = 4,
                        IndicatorCode = indicator.IndicatorCode,
                        TechnicalAreaId = indicator.TechnicalAreaId,
                    };

                    if (indicatorDetail != null)
                    {
                        indicatorsWithScore.Score = indicatorDetail.Score.GetValueOrDefault();
                    }

                    indicatorsWithScore.Goal = this.SetIndicatorGoal(
                        request.PlanType,
                        indicatorsWithScore.Score);

                    model.Add(indicatorsWithScore);
                }
            }
            else
            {
                var sparCountryScores = await this.assessmentService.GetSPARAssessmentAsync(
                    await this.commonService.GetCountryISO3CodeFromIdAsync(
                        request.CountryId));

                List<SPARIndicatorsViewModel> sparIndicators = new ();
                if (sparCountryScores != null)
                {
                    foreach (var capacity in sparCountryScores.Capacities)
                    {
                        if (capacity != null && capacity.Indicators.Any())
                        {
                            sparIndicators.AddRange(capacity.Indicators);
                        }
                    }
                }

                foreach (var indicator in request.Indicators)
                {
                    var indicatorDetail = sparIndicators?
                        .FirstOrDefault(x => x.IndicatorNo == this.GetSPARIndicator(indicator));

                    IndicatorsWithScoreViewModel indicatorsWithScore = new ()
                    {
                        IndicatorCodeId = indicator.IndicatorCodeId,
                        TechnicalAreaIndicatorId = indicator.TechnicalAreaIndicatorId,
                        Score = 1,
                        Name = indicator.Name,
                        Goal = 4,
                        IndicatorCode = indicator.IndicatorCode,
                        TechnicalAreaId = indicator.TechnicalAreaId,
                    };

                    if (indicatorDetail != null)
                    {
                        indicatorsWithScore.Score = this.GetSPARScore(indicatorDetail);
                    }

                    indicatorsWithScore.Goal = this.SetIndicatorGoal(
                        request.PlanType,
                        indicatorsWithScore.Score);

                    model.Add(indicatorsWithScore);
                }
            }

            return model;
        }

        /// <inheritdoc/>
        public async Task<List<UpdatedScoreViewModel>> CheckForUpdatedScoreAsync(CheckUpdatedScoreCommand request)
        {
            List<UpdatedScoreViewModel> updatedScoresList = new ();

            var planDetails = await this.context.CountryPlans
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.CountryPlanId == request.PlanId)
                                        ?? throw new ApiException($"{this.localizer["PlanNotValid"]}");

            var plan = await this.commonService.GetPlanDetailsAsync(request.PlanId);

            List<TechnicalAreaIndicatorViewModel> areaIndicators = new List<TechnicalAreaIndicatorViewModel>();

            // Add indicator. from the plan
            foreach (var technicalArea in plan.TechnicalAreas)
            {
                foreach (var indicator in technicalArea.TechnicalAreaIndicator)
                {
                    areaIndicators.Add(indicator);
                }
            }

            List<IndicatorsWithScoreViewModel> updatedScores = await this.GetScoreForSelectedIndicatorsAsync(
                new GetScoreForSelectedIndicatorsCommand()
                {
                    AssessmentType = (Common.AssessmentType)planDetails.AssessmentTypeId,
                    CountryId = planDetails.CountryId,
                    Indicators = areaIndicators,
                    PlanType = (Common.PlanType)planDetails.PlanTypeId,
                });

            foreach (var indicator in updatedScores)
            {
                var checkScoreChanged = areaIndicators
                                        .FirstOrDefault(x => x.TechnicalAreaIndicatorId == indicator.TechnicalAreaIndicatorId
                                                           && !x.Score.Equals(indicator.Score));

                var technicalArea = plan.TechnicalAreas
                    .FirstOrDefault(x => x.TechnicalAreaId == indicator.TechnicalAreaId);

                if (checkScoreChanged != null && technicalArea != null)
                {
                    UpdatedScoreViewModel model = new UpdatedScoreViewModel()
                    {
                        TechnicalAreaName = technicalArea.Name,
                        TechnicalAreaIndicatorId = indicator.TechnicalAreaIndicatorId,
                        IndicatorCode = indicator.IndicatorCode,
                        IndicatorCodeId = indicator.IndicatorCodeId,
                        Name = indicator.Name,
                        StrategicActions = indicator.StrategicActions,
                        TechnicalAreaId = indicator.TechnicalAreaId,
                        OldGoal = checkScoreChanged.Goal,
                        OldScore = checkScoreChanged.Score,
                        NewScore = indicator.Score == 0 ? 1 : indicator.Score,
                        NewGoal = this.SetIndicatorGoal((Common.PlanType)planDetails.PlanTypeId, indicator.Score == 0 ? 1 : indicator.Score),
                    };

                    updatedScoresList.Add(model);
                }
            }

            // Update the score to the database.
            if (updatedScoresList != null && updatedScoresList.Any())
            {
                var countryPlanIndicators = this.context.CountryPlanIndicators
                      .Where(x => updatedScoresList.Select(y => y.TechnicalAreaIndicatorId).Contains(x.TechnicalAreaIndicatorId.GetValueOrDefault())
                          && !x.IsDeleted
                          && x.CountryPlanId == request.PlanId);

                foreach (var countryPlanIndicator in countryPlanIndicators)
                {
                    var newScoreDetails = updatedScoresList
                        .FirstOrDefault(x => x.TechnicalAreaIndicatorId == countryPlanIndicator.TechnicalAreaIndicatorId);

                    if (newScoreDetails != null)
                    {
                        countryPlanIndicator.SetScore(newScoreDetails.NewScore);
                        countryPlanIndicator.SetGoal(newScoreDetails.NewGoal);
                    }
                }

                await this.context.SaveChangesAsync();
            }

            return updatedScoresList;
        }

        /// <inheritdoc/>
        public async Task<int> CreateOperationalPlanAsync(CreateOperationalPlanCommand model)
        {
            // TO DO: Validate the plan.
            string countryISO = await this.commonService.GetCountryISOCodeFromIdAsync(model.CountryId);
            int? strategicPlanId = model.StrategicPlan != null ? model.StrategicPlan.CountryPlanId : null;
            CountryPlan plan = new (model.CountryId, model.StartDate, model.EndDate, countryISO, string.Empty, (int)Common.PlanType.Operational, (int)model.AssessmentType, strategicPlanId);
            plan.GeneratePlanCode();
            await this.context.CountryPlans.AddAsync(plan);
            await this.context.SaveChangesAsync();

            foreach (var indicator in model.Indicators)
            {
                await this.context.CountryPlanIndicators.AddAsync(new CountryPlanIndicator(
                                    indicator.TechnicalAreaIndicatorId,
                                    plan.CountryPlanId,
                                    indicator.Score,
                                    indicator.Goal));
            }

            await this.context.SaveChangesAsync();

            // manage strategic plan and operational plan on the OP plan creation.
            await this.ManageCountryPlanOnOperationalPlanCreation(plan.CountryPlanId);
            return plan.CountryPlanId;
        }

        /// <inheritdoc/>
        public async Task<List<TechnicalAreaViewModel>> GetTechnicalAreasForUpdateIndicatorsAsync(GetTechnicalAreasForUpdatePlanIndicatorsCommand request)
        {
            CountryPlan countryPlan = await this.context.CountryPlans
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CountryPlanId == request.PlanId) ?? throw new ApiException($"No plan found with Id: {request.PlanId}");

            Common.AssessmentType assessmentType = (Common.AssessmentType)countryPlan.AssessmentTypeId;
            List<TechnicalAreaViewModel> listTechnicalArea = new ();
            List<TechnicalAreaViewModel> technicalAreas = new ();
            List<TechnicalAreaIndicatorViewModel> indicators = new ();
            List<TechnicalAreaIndicatorViewModel> existsIndicators = new ();

            List<SqlParameter> parameters = new ()
            {
                new SqlParameter() { ParameterName = "@PlanId", Value = request.PlanId },
            };

            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_GetAreaIndicatorsForPlanUpdate",
                (ds) =>
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        dt.TableName = dt.Columns[0].ColumnName;
                    }

                    technicalAreas = ds.Tables["TechnicalAreasTable"].ConvertToList<TechnicalAreaViewModel>();
                    indicators = ds.Tables["TechnicalAreaIndicatorTable"].ConvertToList<TechnicalAreaIndicatorViewModel>();
                    existsIndicators = ds.Tables["ExistsIndicatorTable"].ConvertToList<TechnicalAreaIndicatorViewModel>();
                },
                parameters,
                1800);

            List<JEEAssessmentScoreViewModel> jeeScores = new ();
            List<SPARIndicatorsViewModel> sparScores = new ();

            if (countryPlan.AssessmentTypeId == (int)Common.AssessmentType.JEE)
            {
                string countryISO = await this.commonService.GetCountryISOCodeFromIdAsync(countryPlan.CountryId);
                var assessment = await this.assessmentService.GetJEEScoreAsync(countryISO);
                jeeScores = assessment.Scores;
            }
            else
            {
                string countryISO3Code = await this.commonService.GetCountryISO3CodeFromIdAsync(countryPlan.CountryId);
                var assessment = await this.assessmentService.GetSPARAssessmentAsync(countryISO3Code);

                if (assessment != null)
                {
                    foreach (var capacity in assessment.Capacities)
                    {
                        if (capacity != null && capacity.Indicators.Any())
                        {
                            sparScores.AddRange(capacity.Indicators);
                        }
                    }
                }
            }

            foreach (var area in technicalAreas)
            {
                TechnicalAreaViewModel areaViewModel = new ();
                areaViewModel = area;
                areaViewModel.TechnicalAreaIndicator = new List<TechnicalAreaIndicatorViewModel>();
                var areIndicators = await this.BuildIndicatorsForTechnicalAreaAsync(area, indicators, new List<StrategicActionViewModel>());

                foreach (var indicator in areIndicators)
                {
                    indicator.IsExists = false;
                    var existingIndicator = existsIndicators.FirstOrDefault(x => x.TechnicalAreaIndicatorId == indicator.TechnicalAreaIndicatorId);

                    if (existingIndicator != null)
                    {
                        indicator.IsExists = true;
                        if (indicator.Goal <= 0)
                        {
                            indicator.Goal = this.SetIndicatorGoal((Common.PlanType)countryPlan.PlanTypeId, existingIndicator.Score);
                        }
                    }
                    else
                    {
                        if (countryPlan.AssessmentTypeId == (int)Common.AssessmentType.JEE)
                        {
                            indicator.Score = 1;

                            if (jeeScores != null && jeeScores.Any())
                            {
                                var jeeScore = jeeScores.FirstOrDefault(x => x.Indicator == this.GetJEEIndicator(indicator));

                                if (jeeScore != null)
                                {
                                    indicator.Score = jeeScore.Score.GetValueOrDefault() == 0 ? 1 : jeeScore.Score.GetValueOrDefault();
                                }
                            }

                            indicator.Goal = this.SetIndicatorGoal((Common.PlanType)countryPlan.PlanTypeId, indicator.Score);
                        }
                        else
                        {
                            indicator.Score = 1;

                            if (sparScores != null && sparScores.Any())
                            {
                                var sparScore = sparScores
                                    .FirstOrDefault(x => x.IndicatorNo == this.GetSPARIndicator(indicator));

                                if (sparScore != null)
                                {
                                    indicator.Score = this.GetSPARScore(sparScore);
                                }
                            }

                            indicator.Goal = this.SetIndicatorGoal((Common.PlanType)countryPlan.PlanTypeId, indicator.Score);
                        }
                    }
                }

                areaViewModel.TechnicalAreaIndicator.AddRange(areIndicators);

                listTechnicalArea.Add(areaViewModel);
            }

            return listTechnicalArea;
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> GetPlanDetailsAsync(GetPlanDetailsCommand request)
        {
            return await this.commonService.GetPlanDetailsAsync(request.PlanId);
        }

        /// <inheritdoc/>
        public async Task<DashboardViewModel> GetCountryDashboardDetailsAsync(GetCountryDashboardDetailsCommand request)
        {
            DashboardViewModel model = new ()
            {
                Countries = new List<CountryViewModel>(),
            };

            List<FilterParams<CountryPlanViewModel>> filterParamsList = new ();

            if (request.PaginationFilter != null)
            {
                foreach (KeyValuePair<string, HashSet<string>> item in request.PaginationFilter.ColumnsAndValuesForFilter)
                {
                    FilterParams<CountryPlanViewModel> filterParams = new ()
                    {
                        Column = item.Key,
                    };

                    foreach (string value in item.Value)
                    {
                        filterParams.Values.Add(value);
                    }

                    filterParamsList.Add(filterParams);
                }
            }

            List<SqlParameter> parameters = new ()
            {
                new SqlParameter() { ParameterName = "@UserId", Value = request.User.UserId },
            };

            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_GetCountryDashboardPlanDetails",
                async (ds) =>
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        dt.TableName = dt.Columns[0].ColumnName;
                    }

                    model.Countries = ds.Tables["CountriesTable"].ConvertToList<CountryViewModel>();
                    model.CountryPlans = await ds.Tables["PlansDetails"].ConvertToList<CountryPlanViewModel>()
                                        .SortAndPaginateAsync(request.PaginationFilter, filterParamsList);
                },
                parameters,
                1800);
            return model;
        }

        /// <inheritdoc/>
        public async Task<List<CountryPlanViewModel>> GetDashboardChartDetailsAsync(GetDataForDashboardChartCommand request)
        {
            var countryPlans = await this.context.CountryPlans.Where(x => x.CountryId == request.CountryId).OrderByDescending(x => x.PlanStartDate).ToListAsync();
            var listOfPreviousActivePlans = new List<CountryPlan>();
            int size = countryPlans.Count, count = 0;
            for (int i = 0; i < size; i++)
            {
                if (countryPlans[i].PlanStatusId == (int)Common.PlanStatus.Active)
                {
                    count++;
                }

                listOfPreviousActivePlans.Add(countryPlans[i]);
                if (count == 4)
                {
                    break;
                }
            }

            List<CountryPlanViewModel> strategicPlans = this.mapper.Map<List<CountryPlanViewModel>>(listOfPreviousActivePlans);
            foreach (var strategicPlan in strategicPlans)
            {
                if (strategicPlan.PlanStatusId == Common.PlanStatus.Active || strategicPlan.PlanStatusId == Common.PlanStatus.Draft)
                {
                    strategicPlan.OperationalPlans = this.mapper.Map<List<CountryPlanViewModel>>(await this.context.CountryPlans.Where(x => x.StrategicPlanId == strategicPlan.CountryPlanId).ToListAsync());
                }
            }

            return strategicPlans;
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> UpdatePlanIndicatorsAsync(UpdateIndicatorsForPlanCommand request)
        {
            List<IndicatorsWithScoreViewModel> indicatorWithScoresModel = new ();

            foreach (var indicator in request.Indicators)
            {
                IndicatorsWithScoreViewModel indicatorsWithScore = new ()
                {
                    IndicatorCodeId = indicator.IndicatorCodeId,
                    TechnicalAreaIndicatorId = indicator.TechnicalAreaIndicatorId,
                    Score = indicator.Score,
                    Name = indicator.Name,
                    Goal = this.SetIndicatorGoal(
                        request.PlanType,
                        indicator.Score),
                    IndicatorCode = indicator.IndicatorCode,
                    TechnicalAreaId = indicator.TechnicalAreaId,
                };

                indicatorWithScoresModel.Add(indicatorsWithScore);
            }

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new () { ParameterName = "@Indicators", Value = JsonConvert.SerializeObject(indicatorWithScoresModel) },
                new () { ParameterName = "@PlanId", Value = request.PlanId },
                new () { ParameterName = "@UserId", Value = request.User.UserId },
            };

            bool status = Convert.ToBoolean(await this.context.ExecuteScalarAsync(
                CommandType.StoredProcedure,
                "sp_UpdateCountryPlanIndicators",
                parameters,
                1800));

            if (!status)
            {
                throw new ApiException($"Something went wrong while updating the indicators for plan");
            }

            return await this.GetPlanDetailsAsync(new GetPlanDetailsCommand() { PlanId = request.PlanId });
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> UpdatePlanIndicatorGoalAsync(UpdatePlanIndicatorGoalCommand request)
        {
            var indicator = this.context.CountryPlanIndicators.FirstOrDefault(x => x.PlanIndicatorId == request.PlanIndicatorId) ??
                throw new Exception(this.localizer["InvalidIndicatorId"]);

            if (indicator.Score != request.Score)
            {
                throw new Exception(this.localizer["InvalidScore"]);
            }

            indicator.SetGoal(request.Goal);

            await this.context.SaveChangesAsync();

            return await this.commonService.GetPlanDetailsAsync(indicator.CountryPlanId);
        }

        /// <inheritdoc/>
        public async Task<List<JEERecommendationsTechnicalAreasViewModel>> GetJEERecommendationsAsync(GetJEERecommendationsCommand request)
        {
            List<JEERecommendationsTechnicalAreasViewModel> model = new ();

            var countryIso = await this.commonService.GetCountryISOCodeFromIdAsync(request.CountryId);
            var planDetails = await this.commonService.GetPlanDetailsAsync(request.PlanId, false, true, true, false);

            var jeeRecommendations = await this.assessmentService.GetJEERecommendationAsync(countryIso);

            foreach (var area in planDetails.TechnicalAreas.Where(x => !x.IsCustomTechnicalArea))
            {
                var areaRecommendations = jeeRecommendations.Recommendations?
                    .FirstOrDefault(x => x.TechnicalArea == this.GetTechnicalArea(area));

                JEERecommendationsTechnicalAreasViewModel jeeAreas = new JEERecommendationsTechnicalAreasViewModel()
                {
                    AreaCode = area.AreaCode,
                    AreaCodeId = area.AreaCodeId,
                    Name = area.Name,
                    TechnicalAreaId = area.TechnicalAreaId,
                    Recommendations = areaRecommendations != null ? areaRecommendations.Recommendations : new (),
                    StrengthsAndChallenges = new List<JEEStrengthsAndChallengesViewModel>(),
                };

                foreach (var indicator in area.TechnicalAreaIndicator)
                {
                    var strengthDetails = areaRecommendations?.StrengthsAndChallenges?
                        .FirstOrDefault(x => x.Indicator == this.GetJEEIndicator(indicator));

                    if (strengthDetails != null)
                    {
                        JEEStrengthsAndChallengesViewModel strengthsAndChallenges = new JEEStrengthsAndChallengesViewModel
                        {
                            Strengths = strengthDetails.Strengths,
                            Challenges = strengthDetails.Challenges,
                            IndicatorCode = indicator.IndicatorCode,
                            IndicatorCodeId = indicator.IndicatorCodeId,
                            Name = indicator.Name,
                            TechnicalAreaId = indicator.TechnicalAreaId,
                            TechnicalAreaIndicatorId = indicator.TechnicalAreaIndicatorId,
                        };

                        jeeAreas.StrengthsAndChallenges.Add(strengthsAndChallenges);
                    }
                }

                model.Add(jeeAreas);
            }

            return model;
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> CancelPlanAsync(CancelPlanCommand request)
        {
            var countryPlan = await this.context.CountryPlans
                .FirstOrDefaultAsync(x => x.CountryPlanId == request.PlanId) ?? throw new ApiException($"No plan exists with id: {request.PlanId}");

            // Cancel the active/draft plan.
            countryPlan.CancelPlan();
            await this.context.SaveChangesAsync();

            return await this.commonService.GetPlanDetailsAsync(request.PlanId);
        }

        /// <inheritdoc/>
        public async Task<List<TechnicalAreaViewModel>> CreateTechnicalAreasAsync(CustomTechnicalAreasCommand request)
        {
            List<TechnicalArea> technicalAreas = new ();
            foreach (var technicalAreaObject in request.TechnicalAreas)
            {
                var technicalArea = new TechnicalArea(technicalAreaObject.Name, request.CountryId, (int)SourceType.Custom, true, true);

                await this.context.TechnicalAreas.AddAsync(technicalArea);
                await this.context.SaveChangesAsync();

                foreach (var indicator in technicalAreaObject.Indicators)
                {
                    var technicalAreaIndicator = new TechnicalAreaIndicator(technicalArea.TechnicalAreaId, indicator.Name);
                    await this.context.TechnicalAreaIndicators.AddAsync(technicalAreaIndicator);
                }

                await this.context.SaveChangesAsync();
                technicalAreas.Add(technicalArea);
            }

            var technicalAreaViewModels = this.mapper.Map<List<TechnicalAreaViewModel>>(technicalAreas);

            foreach (var technicalArea in technicalAreaViewModels)
            {
                foreach (var indicator in technicalArea.TechnicalAreaIndicator)
                {
                    indicator.Score = 1;
                    indicator.Goal = 2;
                }
            }

            return technicalAreaViewModels;
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> AddIHRActionsToPlanAsync(AddIHRActionsToPlanCommand request)
        {
            List<SqlParameter> parameters = new ()
            {
                new () { ParameterName = "@IHRActions", Value = JsonConvert.SerializeObject(request.IHRActions) },
                new () { ParameterName = "@UserId", Value = request.User.UserId },
            };

            await this.context.ExecuteNonQueryAsync(
                CommandType.StoredProcedure,
                "sp_AddIHRActionsToPlan",
                parameters,
                1800);

            return await this.commonService.GetPlanDetailsAsync(request.CountryPlanId);
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> AddNBWActionsToPlanAsync(AddNBWActionsToPlanCommand request)
        {
            List<SqlParameter> parameters = new ()
            {
                new () { ParameterName = "@NBWActions", Value = JsonConvert.SerializeObject(request.NBWActions) },
                new () { ParameterName = "@CountryPlanId", Value = request.CountryPlanId },
                new () { ParameterName = "@UserId", Value = request.User.UserId },
            };

            await this.context.ExecuteNonQueryAsync(
                CommandType.StoredProcedure,
                "sp_AddNBWActionsToPlan",
                parameters,
                1800);

            return await this.commonService.GetPlanDetailsAsync(request.CountryPlanId);
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> ActivatePlanAsync(ActivatePlanCommand request)
        {
            CountryPlan plan = await this.context.CountryPlans
                .FirstOrDefaultAsync(x => x.CountryPlanId == request.CountryPlanId) ?? throw new ApiException($"No plan found for id: {request.CountryPlanId}");

            // Activate the plan.
            plan.ActivatePlan(request.IsPlanOfficialApproved, request.IsPlanVisibleToOther);

            // Add next plan review date.
            await this.context.CountryPlanReviews.AddAsync(
                new CountryPlanReview(
                    request.CountryPlanId,
                    request.NextReviewDate));

            await this.context.SaveChangesAsync();

            return await this.commonService.GetPlanDetailsAsync(request.CountryPlanId);
        }

        /// <inheritdoc/>
        public async Task<PlanDetailsViewModel> GetDetailsForDownloadPlanAsync(DownloadPlanCommand request)
        {
            PlanDetailsViewModel planDetailsViewModel = new ();

            List<SqlParameter> sqlParameters = new ()
            {
                new SqlParameter { ParameterName = "@CountryPlanId", Value = request.CountryPlanId },
                new SqlParameter { ParameterName = "@UserId", Value = request.User.UserId },
            };

            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_DetailsForDownloadPlan",
                (ds) =>
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        dt.TableName = dt.Columns[0].ColumnName;
                    }

                    planDetailsViewModel.PlanType = (int)ds.Tables["PlanType"].Rows[0].ItemArray[1] !;
                    planDetailsViewModel.Details = ds.Tables["PlanDetails"].ConvertToList<PlanDetails>();
                },
                sqlParameters);

            return planDetailsViewModel;
        }

        /// <inheritdoc/>
        public async Task<CountryPlanViewModel> ClonePlanAsync(ClonePlanCommand request)
        {
            await this.ValidatePlanRangeAsync(request);

            List<SqlParameter> parameters = new ()
            {
                new SqlParameter() { ParameterName = "@PlanId", Value = request.PlanId },
                new SqlParameter() { ParameterName = "@StartDate", Value = request.StartDate },
                new SqlParameter() { ParameterName = "@EndDate", Value = request.EndDate },
                new SqlParameter() { ParameterName = "@IncludeStrategicActions", Value = request.IncludeStrategicActions },
                new SqlParameter() { ParameterName = "@IncludePriorities", Value = request.IncludePriority },
                new SqlParameter() { ParameterName = "@IncludeEstimatedBudget", Value = request.IncludeEstimatedBudget },
                new SqlParameter() { ParameterName = "@IncludeResponsibleAuthority", Value = request.IncludeResponsibleAuthority },
                new SqlParameter() { ParameterName = "@IncludeComments", Value = request.IncludeComments },
                new SqlParameter() { ParameterName = "@UserId", Value = request.User.UserId },
            };

            // Create a new plan from the existing one.
            int clonedPlanId = Convert.ToInt32(await this.context.ExecuteScalarAsync(
                CommandType.StoredProcedure,
                "sp_ClonePlan",
                parameters,
                1800));

            if (clonedPlanId == 0)
            {
                throw new ApiException(this.localizer["SomethingWentWrong"]);
            }

            // Update the latest score.
            await this.CheckForUpdatedScoreAsync(new CheckUpdatedScoreCommand()
            {
                PlanId = clonedPlanId,
                User = request.User,
            });

            var clonedPlanDetails = await this.context.CountryPlans
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CountryPlanId == clonedPlanId) ?? throw new ApiException(this.localizer["InvalidPlanId"]);

            return this.mapper.Map<CountryPlanViewModel>(clonedPlanDetails);
        }

        /// <inheritdoc/>
        public async Task<int> GetActionsCount(GetPlanDetailsCommand request)
        {
            List<SqlParameter> parameters = new ()
            {
                new SqlParameter() { ParameterName = "@PlanId", Value = request.PlanId },
            };

            int count = Convert.ToInt32(await this.context.ExecuteScalarAsync(
                CommandType.StoredProcedure,
                "sp_CalculatePlanCount",
                parameters,
                1800));

            return count;
        }

        /// <inheritdoc/>
        public async Task<GetAssistanceDashboardDataViewModel> GetAssistanceDashboardDataAsync(GetAssistanceDataCommand request)
        {
            List<SqlParameter> parameters = new ()
            {
                new () { ParameterName = "@NeedFinancialAssistance", Value = request.NeedFinancialAssistance },
                new () { ParameterName = "@NeedFilterData", Value = request.NeedFilterData },
                new () { ParameterName = "@UserId", Value = request.User.UserId },
            };

            List<FilterParams<AssistanceDataViewModel>> filterParamsList = new ();

            if (request.PaginationFilter != null)
            {
                foreach (KeyValuePair<string, HashSet<string>> item in request.PaginationFilter.ColumnsAndValuesForFilter)
                {
                    FilterParams<AssistanceDataViewModel> filterParams = new ()
                    {
                        Column = item.Key,
                    };

                    foreach (string value in item.Value)
                    {
                        filterParams.Values.Add(value);
                    }

                    filterParamsList.Add(filterParams);
                }
            }

            GetAssistanceDashboardDataViewModel model = new ();

            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_GetAssistanceDashboardData",
                async (ds) =>
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        dt.TableName = dt.Columns[0].ColumnName;
                    }

                    if (request.NeedFilterData)
                    {
                        model.TechnicalAreas = ds.Tables["TechnicalAreaTable"].ConvertToList<TechnicalAreaViewModel>();
                        model.TechnicalAreaIndicators = ds.Tables["TechnicalAreaIndicatorTable"].ConvertToList<TechnicalAreaIndicatorViewModel>();
                    }

                    model.AssistanceData = await ds.Tables["AssistanceDashboardTable"].ConvertToList<AssistanceDataViewModel>().SortAndPaginateAsync(request.PaginationFilter, filterParamsList);
                },
                parameters,
                1800);

            foreach (var techArea in model.TechnicalAreas)
            {
                string[] arr = techArea.TechnicalAreaIds.Split(',');
                techArea.TechnicalAreaIndicator = model.TechnicalAreaIndicators.Where(x => arr.Contains(x.TechnicalAreaId.ToString())).ToList();
            }

            model.TechnicalAreaIndicators = new ();

            return model;
        }

        /// <inheritdoc/>
        public async Task<DashboardViewModel> GetSharedPlansAsync(GetSharedPlansCommand request)
        {
            DashboardViewModel result = new ();
            var plans = await this.context.CountryPlans.AsNoTracking()
                .Where(x => x.VisibleToAnotherCountries && !x.IsDeleted)
                .ToListAsync();

            List<FilterParams<CountryPlanViewModel>> filterParams = new ();

            foreach (var item in request.PaginationFilter.ColumnsAndValuesForFilter)
            {
                FilterParams<CountryPlanViewModel> filterParam = new ()
                {
                    Column = item.Key,
                    Values = item.Value,
                };

                filterParams.Add(filterParam);
            }

            result.CountryPlans = await this.mapper.Map<List<CountryPlanViewModel>>(plans)
                                        .SortAndPaginateAsync(request.PaginationFilter, filterParams);

            result.Countries = this.mapper.Map<List<CountryViewModel>>(
                                await this.context.Countries.AsNoTracking()
                                .OrderBy(x => x.Name).ToListAsync());

            foreach (var plan in result.CountryPlans.Data)
            {
                plan.Name = result.Countries
                                .FirstOrDefault(country => country.CountryId == plan.CountryId)
                                .Name;
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> GetSharedPlanAsync(int planId)
        {
            var plan = await this.commonService.GetPlanDetailsAsync(planId);

            return plan.CountryPlan.VisibleToAnotherCountries ? plan : throw new BadHttpRequestException("Not Found");
        }

        /// <inheritdoc/>
        public async Task<List<CountryPlanReviewViewModel>> GetReviews(GetReviewsCommand request)
        {
            List<CountryPlanReviewViewModel> reviews = new ();

            List<SqlParameter> sqlParameters = new ()
            {
                new SqlParameter() { ParameterName = "@userId", Value = request.User.UserId, },
                new SqlParameter() { ParameterName = "@months", Value = 1, },
            };

            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_GetReviews",
                (ds) =>
                {
                    reviews = ds.Tables[0].ConvertToList<CountryPlanReviewViewModel>();
                },
                sqlParameters);

            return reviews;
        }

        /// <inheritdoc/>
        public async Task<List<UploadPlanResponseViewModel>> UploadPlanAsync(UploadPlanCommand request)
        {
            List<UploadPlanResponseViewModel> model = new List<UploadPlanResponseViewModel>();

            List<SqlParameter> parameters = new ()
            {
                new SqlParameter() { ParameterName = "@PlanId", Value = request.CountryPlanId },
                new SqlParameter() { ParameterName = "@UserId", Value = request.User.UserId },
                new SqlParameter() { ParameterName = "@PlanDetails", Value = JsonConvert.SerializeObject(request.PlansDetails) },
            };

            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_UploadPlan",
                (ds) =>
                {
                    model = ds.Tables[0].ConvertToList<UploadPlanResponseViewModel>();
                },
                parameters,
                1800);

            return model;
        }

        /// <summary>
        /// Create strategic plan if not exists and if SP is created then add the actions to the new created plan.
        /// </summary>
        /// <param name="operationalPlanId"> Operational plan id.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        private async Task ManageCountryPlanOnOperationalPlanCreation(int operationalPlanId)
        {
            List<SqlParameter> parameters = new ()
            {
                new () { ParameterName = "@PlanId", Value = operationalPlanId },
            };

            await this.context.ExecuteNonQueryAsync(
                CommandType.StoredProcedure,
                "sp_ManageCountryPlanOnOperationalPlanCreation",
                parameters,
                1800);
        }

        /// <summary>
        /// Build complete plan model.
        /// </summary>
        /// <param name="countryPlan"> <see cref="CountryPlanViewModel"/> model.</param>
        /// <param name="technicalAreas"> List of various <see cref="TechnicalAreaViewModel"/> model.</param>
        /// <param name="indicators"> List of <see cref="TechnicalAreaIndicatorViewModel"/> for technical areas.</param>
        /// <param name="strategicActions"> List of <see cref="StrategicActionViewModel"/> actions.</param>
        /// <returns> Returns the <see cref="CompletePlanDetailsViewModel"/> model.</returns>
        private async Task<CompletePlanDetailsViewModel> BuildCompletePlanModelAsync(
            CountryPlanViewModel countryPlan,
            List<TechnicalAreaViewModel> technicalAreas,
            List<TechnicalAreaIndicatorViewModel> indicators,
            List<StrategicActionViewModel> strategicActions)
        {
            CompletePlanDetailsViewModel model = new CompletePlanDetailsViewModel
            {
                CountryPlan = countryPlan,
                TechnicalAreas = new List<TechnicalAreaViewModel>(),
            };

            List<TechnicalAreaViewModel> listTechnicalArea = new List<TechnicalAreaViewModel>();

            foreach (var area in technicalAreas)
            {
                TechnicalAreaViewModel areaViewModel = new ();
                areaViewModel = area;
                areaViewModel.TechnicalAreaIndicator = new List<TechnicalAreaIndicatorViewModel>();
                areaViewModel.TechnicalAreaIndicator = await this.BuildIndicatorsForTechnicalAreaAsync(area, indicators, strategicActions);
                listTechnicalArea.Add(areaViewModel);
            }

            model.TechnicalAreas = listTechnicalArea;

            return model;
        }

        /// <summary>
        /// Build indicators for technical areas.
        /// </summary>
        /// <param name="area"> <see cref="TechnicalAreaViewModel"/> model.</param>
        /// <param name="indicators"> List of <see cref="TechnicalAreaIndicatorViewModel"/> for technical area.</param>
        /// <param name="strategicActions"> List of <see cref="StrategicActionViewModel"/> actions.</param>
        /// <returns> Returns list of <see cref="TechnicalAreaIndicatorViewModel"/> model.</returns>
        private async Task<List<TechnicalAreaIndicatorViewModel>> BuildIndicatorsForTechnicalAreaAsync(
            TechnicalAreaViewModel area,
            List<TechnicalAreaIndicatorViewModel> indicators,
            List<StrategicActionViewModel> strategicActions)
        {
            var areaIndicators = indicators.Where(x => x.TechnicalAreaId == area.TechnicalAreaId);

            List<TechnicalAreaIndicatorViewModel> technicalAreaIndicators = new List<TechnicalAreaIndicatorViewModel>();

            if (areaIndicators != null && areaIndicators.Any())
            {
                foreach (var indicator in areaIndicators)
                {
                    TechnicalAreaIndicatorViewModel indicatorViewModel = new ();
                    indicatorViewModel = indicator;
                    indicatorViewModel.StrategicActions = new ();
                    indicatorViewModel.StrategicActions = await this.BuildStrategicActionModelAsync(indicator, strategicActions);
                    technicalAreaIndicators.Add(indicatorViewModel);
                }
            }

            return technicalAreaIndicators;
        }

        /// <summary>
        /// Build strategic action model for indicator.
        /// </summary>
        /// <param name="indicator"> <see cref="TechnicalAreaIndicatorViewModel"/> model.</param>
        /// <param name="strategicActions"> List of <see cref="StrategicActionViewModel"/> actions.</param>
        /// <returns> Returns list of <see cref="StrategicActionViewModel"/> for the indicator.</returns>
        private async Task<List<StrategicActionViewModel>> BuildStrategicActionModelAsync(
            TechnicalAreaIndicatorViewModel indicator,
            List<StrategicActionViewModel> strategicActions)
        {
            return await Task.FromResult(strategicActions
                            .Where(x => x.TechnicalAreaIndicatorId == indicator.TechnicalAreaIndicatorId).ToList());
        }

        /// <summary>
        /// Validate whether plan can be cloned or not.
        /// </summary>
        /// <param name="request"><see cref="ClonePlanCommand"/> command to validate clone plan request.</param>
        /// <returns>Returns async task.</returns>
        private async Task ValidatePlanRangeAsync(ClonePlanCommand request)
        {
            // step 1 : Get Plan
            var countryPlan = await this.context.CountryPlans.FirstOrDefaultAsync(x => x.CountryPlanId == request.PlanId && x.PlanStatusId != (int)Common.PlanStatus.Draft)
                ?? throw new ApiException(this.localizer["InvalidPlanId"]);

            // step 2 : validate plans according to their types.
            if (countryPlan.PlanTypeId == (int)Common.PlanType.Strategic)
            {
                if (request.StartDate.Date.AddYears(5) != request.EndDate.Date)
                {
                    throw new ApiException(this.localizer["NotValidDateDiffStrategicPlan"]);
                }

                await this.ValidateStrategicPlan(countryPlan.CountryId, request.StartDate);
            }
            else
            {
                if (!(request.EndDate.Date >= request.StartDate.AddYears(1).Date && request.EndDate.Date <= request.StartDate.AddYears(2).Date))
                {
                    throw new ApiException(this.localizer["NotValidDateDiffOperationalPlan"]);
                }

                await this.ValidateOperationalPlan(countryPlan.StrategicPlanId, request.StartDate, request.EndDate);
            }
        }

        /// <summary>
        /// Validate strategic plan.
        /// </summary>
        /// <param name="countryId">Country id.</param>
        /// <param name="startDate">Start date.</param>
        /// <returns> Returns the async task.</returns>
        private async Task ValidateStrategicPlan(int countryId, DateTime startDate)
        {
            // Check if any active or draft plan has end date before our new plan's start date?
            // Also we will check if we have only two SP exists in the country.
            var plans = await this.context.CountryPlans
                .AsNoTracking()
                .Where(x => x.CountryId == countryId
                    && x.PlanTypeId == (int)Common.PlanType.Strategic
                    && (x.PlanStatusId == (int)Common.PlanStatus.Draft || x.PlanStatusId == (int)Common.PlanStatus.Active))
                .ToListAsync();

            // Check for the required conditions.
            if (plans != null && plans.Any())
            {
                // If count exceeded the limit
                if (plans.Count == CommonSettings.ApplicationSettings.MaxStrategicPlanAllowed)
                {
                    throw new ApiException($"{CommonSettings.ApplicationSettings.MaxStrategicPlanAllowed} {this.localizer["LimitStrategicPlanExisted"]}");
                }

                // If plan end date is in between the new plan date.
                else if (plans.Where(x => x.PlanEndDate.Date > startDate.Date).Any())
                {
                    DateTime lastEndDate = plans
                        .OrderByDescending(x => x.PlanEndDate.Date)
                        .FirstOrDefault().PlanEndDate.Date;

                    throw new ApiException($"{this.localizer["ValidStrategicPlanDate"]} {lastEndDate.ToString("MM/dd/yyyy")}");
                }
            }
        }

        /// <summary>
        /// Validate whether operational plan can be created or not.
        /// </summary>
        /// <param name="strategicPlanId">Strategic plan id.</param>
        /// <param name="startDate">Given Start date.</param>
        /// <param name="endDate">Given end date.</param>
        /// <returns>Returns the async task.</returns>
        private async Task ValidateOperationalPlan(int? strategicPlanId, DateTime startDate, DateTime endDate)
        {
            // it's parent SP should be active or draft only.
            var strategicPlan = await this.context.CountryPlans
                    .FirstOrDefaultAsync(x => x.CountryPlanId == strategicPlanId
                        && (x.PlanStatusId == (int)Common.PlanStatus.Active || x.PlanStatusId == (int)Common.PlanStatus.Draft))
                ?? throw new ApiException(this.localizer["InvalidPlanId"]);

            // step - 1 check if OP is in between SP range.
            if (strategicPlan.PlanStartDate.Date > startDate.Date)
            {
                throw new ApiException(this.localizer["OPStartDateGreaterThanSPStartDate"]);
            }

            if (strategicPlan.PlanEndDate.Date < endDate.Date)
            {
                throw new ApiException(this.localizer["OPEndDateLessThanSPEndDate"]);
            }

            // Step 2 : Get all OP that belong to current strategic plan.
            var plans = await this.context.CountryPlans
                .AsNoTracking()
                .Where(x => x.StrategicPlanId == strategicPlanId
                    && x.PlanTypeId == (int)Common.PlanType.Operational
                    && (x.PlanStatusId == (int)Common.PlanStatus.Draft || x.PlanStatusId == (int)Common.PlanStatus.Active))
                .OrderBy(x => x.PlanStartDate)
                .ToListAsync();

            // step 3 : check if any OP overlaps with new plans start and end date.
            // (StartA <= EndB) and (EndA >= StartB) then overlap
            if (plans != null && plans.Where(x => x.PlanStartDate.Date <= endDate.Date && x.PlanEndDate.Date >= startDate.Date).Any())
            {
                throw new ApiException(this.localizer["OPAlreadyExists"]);
            }
        }

        /// <summary>
        /// Get JEE indicator details.
        /// </summary>
        /// <param name="technicalAreaIndicator"> <see cref="TechnicalAreaIndicatorViewModel"/> model.</param>
        /// <returns> Returns the indicator.</returns>
        private string GetJEEIndicator(TechnicalAreaIndicatorViewModel technicalAreaIndicator)
        {
            string indicator = string.Empty;

            if (!string.IsNullOrEmpty(technicalAreaIndicator.IndicatorCode))
            {
                indicator = technicalAreaIndicator.IndicatorCode;
            }

            if (!string.IsNullOrEmpty(technicalAreaIndicator.IndicatorCodeId))
            {
                indicator += $".{technicalAreaIndicator.IndicatorCodeId}";
            }

            return indicator;
        }

        /// <summary>
        /// Get SPAR indicator details.
        /// </summary>
        /// <param name="technicalAreaIndicator"> <see cref="TechnicalAreaIndicatorViewModel"/> technical area indicator. </param>
        /// <returns> Returns the SPAR indicator value. </returns>
        private string GetSPARIndicator(TechnicalAreaIndicatorViewModel technicalAreaIndicator)
        {
            return $"{technicalAreaIndicator.IndicatorCode}{technicalAreaIndicator.IndicatorCodeId}";
        }

        /// <summary>
        /// Get the SPAR score.
        /// SPAR is score is calculated out of 100 so we need to convert that score out of 5.
        /// </summary>
        /// <param name="sparIndicator"> <see cref="SPARIndicatorsViewModel"/> Spar indicator details. </param>
        /// <returns> Returns the spar score.</returns>
        private int GetSPARScore(SPARIndicatorsViewModel sparIndicator)
        {
            double sparScore = 1;

            if (sparIndicator != null && sparIndicator.IndicatorScore.GetValueOrDefault() > 0)
            {
                // Get the spar score.
                sparScore = sparIndicator.IndicatorScore.GetValueOrDefault() / 20;

                // If score comes with fractions.
                if (!(sparIndicator.IndicatorScore % 20 == 0))
                {
                    // Ceiling the amount.
                    sparScore = Math.Ceiling(sparScore);
                }
            }

            return Convert.ToInt32(sparScore);
        }

        /// <summary>
        /// Set initial goal for indicator while creating a plan.
        /// </summary>
        /// <param name="planType"> <see cref="Common.PlanType"/> enum.</param>
        /// <param name="score"> Indicator score.</param>
        /// <returns> Returns the goal.</returns>
        private int SetIndicatorGoal(Common.PlanType planType, int score)
        {
            int goal = score;
            if (new List<int>() { 1, 2, 3 }.Contains(score))
            {
                if (planType == Common.PlanType.Strategic)
                {
                    goal = 4;
                }
                else
                {
                    goal = score + 1;
                }
            }
            else
            {
                goal = 5;
            }

            return goal;
        }

        /// <summary>
        /// Get technical area code.
        /// </summary>
        /// <param name="area"> <see cref="TechnicalAreaViewModel"/> model.</param>
        /// <returns> Returns the technical area code.</returns>
        private string GetTechnicalArea(TechnicalAreaViewModel area)
        {
            string technicalArea = string.Empty;

            if (!string.IsNullOrEmpty(area.AreaCode))
            {
                technicalArea += area.AreaCode;
            }

            if (!string.IsNullOrEmpty(area.AreaCodeId))
            {
                technicalArea += $".{area.AreaCodeId}";
            }

            return technicalArea;
        }
    }
}