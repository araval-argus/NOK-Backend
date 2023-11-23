// <copyright file="PlanRecommendationService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.ServiceImplementation
{
    using System.Data;
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;
    using WHO.NOK.BusinessLogic.Features.PlanRecommendations;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.BusinessLogic.ViewModels.PlanRecommendations;
    using WHO.NOK.Infrastructure.Helper;
    using WHO.NOK.Infrastructure.Models.DatabaseContext;

    /// <summary>
    /// Implement <see cref="IPlanRecommendationService"/> interface.
    /// </summary>
    public class PlanRecommendationService : IPlanRecommendationService
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanRecommendationService"/> class.
        /// </summary>
        /// /// <param name="context"> Database context.</param>
        public PlanRecommendationService(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task<List<ImportIHRActionsViewModel>> ImportIHRActionsAsync(ImportIHRActionsCommand request)
        {
            List<SqlParameter> sqlParameters = new ()
            {
                new SqlParameter() { ParameterName = "@CountryPlanId", Value = request.CountryPlanId },
                new SqlParameter() { ParameterName = "@IndicatorCodeId", Value = request.IndicatorCodeId, IsNullable = true },
                new SqlParameter() { ParameterName = "@IndicatorCode", Value = request.IndicatorCode, IsNullable = true },
            };

            var ihrActionsList = new List<SelectIHRActions>();

            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_GetImportedIHRActionsFromPlanId",
                (reader) =>
                {
                    ihrActionsList = reader.Tables[0].ConvertToList<SelectIHRActions>();
                },
                sqlParameters);

            Dictionary<string, ImportIHRActionsViewModel> ihrActionsMap = new ();
            Dictionary<string, int> indicatorsMap = new ();

            foreach (var item in ihrActionsList)
            {
                if (!ihrActionsMap.ContainsKey(item.TechnicalArea))
                {
                    var ihrActionObject = new ImportIHRActionsViewModel()
                    {
                        TechnicalArea = item.TechnicalArea,
                        Indicators = new (),
                    };
                    ihrActionsMap.Add(item.TechnicalArea, ihrActionObject);
                }

                var indicators = ihrActionsMap[item.TechnicalArea].Indicators;
                if (!indicatorsMap.ContainsKey(item.IndicatorId))
                {
                    indicatorsMap.Add(item.IndicatorId, indicators.Count);
                    var addIndicator = new IHRIndicatorsListViewModel()
                    {
                        PlanIndicatorId = item.PlanIndicatorId,
                        BenchMark = item.BenchMark,
                        IndicatorCode = item.IndicatorCode,
                        IndicatorCodeId = item.IndicatorCodeId,
                        IHRIndicatorId = item.IndicatorId,
                        Score = item.Score,
                        Goal = item.Goal,
                        IHRActions = new (),
                    };
                    indicators.Add(addIndicator);
                }

                indicators[indicatorsMap[item.IndicatorId]].IHRActions.Add(
                    new IHRActionsViewModel()
                    {
                        IHRRecommendationId = item.IHRRecommendationId,
                        Action = item.Actions,
                        TargetScore = item.TargetScore,
                    });
            }

            List<ImportIHRActionsViewModel> importIHRActionsList = new ();
            foreach (var item in ihrActionsMap)
            {
                importIHRActionsList.Add(item.Value);
            }

            return importIHRActionsList;
        }

        /// <inheritdoc/>
        public async Task<List<ImportNBWActionsViewModel>> ImportNBWActionsAsync(ImportNBWActionsCommand request)
        {
            List<SqlParameter> sqlParameters = new ()
            {
                new SqlParameter() { ParameterName = "@CountryPlanId", Value = request.CountryPlanId },
                new SqlParameter() { ParameterName = "@IndicatorCodeId", Value = request.IndicatorCodeId, IsNullable = true },
                new SqlParameter() { ParameterName = "@IndicatorCode", Value = request.IndicatorCode, IsNullable = true },
            };

            var nbwActionsList = new List<SelectNBWActions>();

            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_GetImportedNBWActionsFromPlanId",
                (reader) =>
                {
                    nbwActionsList = reader.Tables[0].ConvertToList<SelectNBWActions>();
                },
                sqlParameters);

            Dictionary<string, ImportNBWActionsViewModel> nbwActionsMap = new ();
            Dictionary<string, int> indicatorsMap = new ();

            foreach (var item in nbwActionsList)
            {
                if (!nbwActionsMap.ContainsKey(item.TechnicalArea))
                {
                    var nbwActionObject = new ImportNBWActionsViewModel()
                    {
                        TechnicalArea = item.TechnicalArea,
                        Indicators = new (),
                    };
                    nbwActionsMap.Add(item.TechnicalArea, nbwActionObject);
                }

                var indicators = nbwActionsMap[item.TechnicalArea].Indicators;
                if (!indicatorsMap.ContainsKey(item.IndicatorName))
                {
                    indicatorsMap.Add(item.IndicatorName, indicators.Count);
                    var addIndicator = new NBWIndicatorsListViewModel()
                    {
                        PlanIndicatorId = item.PlanIndicatorId,
                        Score = item.Score,
                        Goal = item.Goal,
                        IndicatorName = item.IndicatorName,
                        IndicatorCode = item.IndicatorCode,
                        IndicatorId = item.IndicatorId,
                        Objective = item.Objective,
                        NBWActions = new List<NBWActionsViewModel>(),
                    };
                    indicators.Add(addIndicator);
                }

                indicators[indicatorsMap[item.IndicatorName]].NBWActions.Add(
                    new NBWActionsViewModel()
                    {
                        NBWRecommendationId = item.NBWRecommendationId,
                        Feasibility = item.Feasibility,
                        Impact = item.Impact,
                        StrategicAction = item.StrategicAction,
                        ResponsibleAuthority = item.ResponsibleAuthority,
                        DetailedActivity = item.DetailedActivity,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                    });
            }

            List<ImportNBWActionsViewModel> importNBWActionsList = new ();
            foreach (var item in nbwActionsMap)
            {
                importNBWActionsList.Add(item.Value);
            }

            return importNBWActionsList;
        }
    }
}