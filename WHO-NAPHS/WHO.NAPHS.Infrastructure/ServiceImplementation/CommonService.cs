// <copyright file="CommonService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8604, CS8600

namespace WHO.NAPHS.Infrastructure.ServiceImplementation
{
    using System.Data;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NAPHS.BusinessLogic.ViewModels.StrategicAction;
    using WHO.NAPHS.BusinessLogic.ViewModels.UserClaims;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Core.Common.Resources;
    using WHO.NAPHS.Core.Wrappers;
    using WHO.NAPHS.Infrastructure.Helper;
    using WHO.NAPHS.Infrastructure.Models.DatabaseContext;

    /// <summary>
    /// Implement <see cref="ICommonService"/> interface.
    /// </summary>
    public class CommonService : ICommonService
    {
        private readonly ApplicationDbContext context;
        private readonly IStringLocalizer<Resources> localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonService"/> class.
        /// </summary>
        /// <param name="context"> Database context.</param>
        /// <param name="localizer"> Localizer.</param>
        public CommonService(ApplicationDbContext context, IStringLocalizer<Resources> localizer)
        {
            this.context = context;
            this.localizer = localizer;
        }

        /// <inheritdoc/>
        public async Task<int> GetCountryIdFromISOCodeAsync(string countryISOCode)
        {
            var country = await this.context.Countries
                               .AsNoTracking()
                               .FirstOrDefaultAsync(c => c.ISOCode == countryISOCode);

            if (country == null)
            {
                throw new ApiException($"No country available for country code: {countryISOCode}");
            }

            return country.CountryId;
        }

        /// <inheritdoc/>
        public async Task<string> GetCountryISOCodeFromIdAsync(int countryId)
        {
            var country = await this.context.Countries
                               .AsNoTracking()
                               .FirstOrDefaultAsync(c => c.CountryId == countryId);

            if (country == null)
            {
                throw new ApiException($"No country available for id: {countryId}");
            }

            return country.ISOCode;
        }

        /// <inheritdoc/>
        public async Task<ActionPriority> GetActionPriorityAsync(ActionFeasibility feasibility, ActionImpact impact)
        {
            ActionPriority priority = ActionPriority.VeryLow;

            switch (feasibility)
            {
                case ActionFeasibility.Easy:
                    {
                        switch (impact)
                        {
                            case ActionImpact.Low:
                                {
                                    priority = ActionPriority.Medium;
                                    break;
                                }

                            case ActionImpact.High:
                                {
                                    priority = ActionPriority.VeryHigh;
                                    break;
                                }

                            case ActionImpact.Medium:
                                {
                                    priority = ActionPriority.High;
                                    break;
                                }

                            default:
                                {
                                    priority = ActionPriority.VeryLow;
                                    break;
                                }
                        }

                        break;
                    }

                case ActionFeasibility.Medium:
                    {
                        switch (impact)
                        {
                            case ActionImpact.Low:
                                {
                                    priority = ActionPriority.Low;
                                    break;
                                }

                            case ActionImpact.High:
                                {
                                    priority = ActionPriority.High;
                                    break;
                                }

                            case ActionImpact.Medium:
                                {
                                    priority = ActionPriority.Medium;
                                    break;
                                }

                            default:
                                {
                                    priority = ActionPriority.VeryLow;
                                    break;
                                }
                        }

                        break;
                    }

                case ActionFeasibility.Difficult:
                    {
                        switch (impact)
                        {
                            case ActionImpact.Low:
                                {
                                    priority = ActionPriority.VeryLow;
                                    break;
                                }

                            case ActionImpact.High:
                                {
                                    priority = ActionPriority.Medium;
                                    break;
                                }

                            case ActionImpact.Medium:
                                {
                                    priority = ActionPriority.Low;
                                    break;
                                }

                            default:
                                {
                                    priority = ActionPriority.VeryLow;
                                    break;
                                }
                        }

                        break;
                    }

                default:
                    {
                        priority = ActionPriority.VeryLow;
                        break;
                    }
            }

            return await Task.FromResult(priority);
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> GetPlanDetailsAsync(
            int planId,
            bool needCountryPlanInfo = true,
            bool includeAreas = true,
            bool includeIndicators = true,
            bool includeStrategicActions = true,
            bool includeDetailedActivity = true)
        {
            List<SqlParameter> parameters = new ()
            {
                new SqlParameter() { ParameterName = "@PlanId", Value = planId },
                new SqlParameter() { ParameterName = "@NeedCountryDetails", Value = needCountryPlanInfo },
            };

            CountryPlanViewModel countryPlan = new ();
            List<TechnicalAreaViewModel> technicalAreas = new ();
            List<TechnicalAreaIndicatorViewModel> indicators = new ();
            List<StrategicActionViewModel> strategicActions = new ();
            List<DetailActivityViewModel> detailActivities = new ();
            PlanStatisticsViewModel planStats = new ();

            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_GetPlanDetails",
                (ds) =>
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        dt.TableName = dt.Columns[0].ColumnName;
                    }

                    if (needCountryPlanInfo)
                    {
                        countryPlan = ds.Tables["CountryPlanTable"].ConvertToList<CountryPlanViewModel>().FirstOrDefault();
                    }

                    if (includeAreas)
                    {
                        technicalAreas = ds.Tables["TechnicalAreaTable"].ConvertToList<TechnicalAreaViewModel>();
                    }

                    if (includeIndicators)
                    {
                        indicators = ds.Tables["TechnicalAreaIndicatorTable"].ConvertToList<TechnicalAreaIndicatorViewModel>();
                    }

                    if (includeStrategicActions)
                    {
                        strategicActions = ds.Tables["StrategicActionsTable"].ConvertToList<StrategicActionViewModel>();
                    }

                    if (includeDetailedActivity)
                    {
                        detailActivities = ds.Tables["DetailedActivitiesTable"].ConvertToList<DetailActivityViewModel>();
                    }

                    planStats = ds.Tables["PlanStatsTable"].ConvertToList<PlanStatisticsViewModel>().FirstOrDefault();
                },
                parameters,
                1800);

            CompletePlanDetailsViewModel model = await this.BuildCompletePlanModelAsync(
                countryPlan,
                technicalAreas,
                indicators,
                strategicActions,
                detailActivities,
                planStats,
                includeIndicators,
                includeStrategicActions,
                includeDetailedActivity);

            return model;
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> BuildCompletePlanModelAsync(
            CountryPlanViewModel countryPlan,
            List<TechnicalAreaViewModel> technicalAreas,
            List<TechnicalAreaIndicatorViewModel> indicators,
            List<StrategicActionViewModel> strategicActions,
            List<DetailActivityViewModel> detailActivities,
            PlanStatisticsViewModel planStats,
            bool includeIndicators = true,
            bool includeStrategicActions = true,
            bool includeDetailedActivity = true)
        {
            CompletePlanDetailsViewModel model = new CompletePlanDetailsViewModel
            {
                CountryPlan = countryPlan,
                PlanStatistics = planStats,
                TechnicalAreas = new List<TechnicalAreaViewModel>(),
            };

            List<TechnicalAreaViewModel> listTechnicalArea = new ();

            foreach (var area in technicalAreas)
            {
                TechnicalAreaViewModel areaViewModel = new ();
                areaViewModel = area;
                areaViewModel.TechnicalAreaIndicator = new List<TechnicalAreaIndicatorViewModel>();

                if (includeIndicators)
                {
                    areaViewModel.TechnicalAreaIndicator = await this.BuildIndicatorsForTechnicalAreaAsync(area, indicators, strategicActions, detailActivities, includeStrategicActions, includeDetailedActivity);
                    listTechnicalArea.Add(areaViewModel);
                }
            }

            model.TechnicalAreas = listTechnicalArea;

            return model;
        }

        /// <inheritdoc/>
        public async Task<List<TechnicalAreaIndicatorViewModel>> BuildIndicatorsForTechnicalAreaAsync(
            TechnicalAreaViewModel area,
            List<TechnicalAreaIndicatorViewModel> indicators,
            List<StrategicActionViewModel> strategicActions,
            List<DetailActivityViewModel> detailActivities,
            bool includeStrategicActions = true,
            bool includeDetailedActivity = true)
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

                    if (includeStrategicActions)
                    {
                        indicatorViewModel.StrategicActions = await this.BuildStrategicActionModelAsync(indicator, strategicActions, detailActivities, includeDetailedActivity);
                    }

                    technicalAreaIndicators.Add(indicatorViewModel);
                }
            }

            return technicalAreaIndicators;
        }

        /// <inheritdoc/>
        public async Task<List<StrategicActionViewModel>> BuildStrategicActionModelAsync(
            TechnicalAreaIndicatorViewModel indicator,
            List<StrategicActionViewModel> strategicActions,
            List<DetailActivityViewModel> detailActivities,
            bool includeDetailedActivity = true)
        {
            var indicatorActions = strategicActions
                            .Where(x => x.TechnicalAreaIndicatorId == indicator.TechnicalAreaIndicatorId);

            List<StrategicActionViewModel> actions = new ();

            foreach (var indicatorAction in indicatorActions)
            {
                StrategicActionViewModel model = new ();
                model = indicatorAction;
                model.DetailActivities = new List<DetailActivityViewModel>();

                if (includeDetailedActivity)
                {
                    model.DetailActivities = await this.BuildDetailedActivitiesAsync(indicatorAction, detailActivities);
                }

                actions.Add(model);
            }

            return actions;
        }

        /// <inheritdoc/>
        public async Task<List<DetailActivityViewModel>> BuildDetailedActivitiesAsync(
            StrategicActionViewModel strategicAction,
            List<DetailActivityViewModel> detailActivities)
        {
            return await Task.FromResult(detailActivities
                    .Where(x => x.StrategicActionId == strategicAction.StrategicActionId).ToList());
        }

        /// <inheritdoc/>
        public async Task<int> GetCountryIdFromISO3CodeAsync(string iso3Code)
        {
            var country = await this.context.Countries
                               .AsNoTracking()
                               .FirstOrDefaultAsync(c => c.ISO3Code == iso3Code);

            if (country == null)
            {
                throw new ApiException($"No country available for country code: {iso3Code}");
            }

            return country.CountryId;
        }

        /// <inheritdoc/>
        public async Task<string> GetCountryISO3CodeFromIdAsync(int countryId)
        {
            var country = await this.context.Countries
                               .AsNoTracking()
                               .FirstOrDefaultAsync(c => c.CountryId == countryId);

            if (country == null)
            {
                throw new ApiException($"No country available for id: {countryId}");
            }

            return country.ISO3Code;
        }

        /// <inheritdoc/>
        public async Task<string> GenerateRandomStringAsync(int length, bool numericOnly = true)
        {
            Random random = new ();

            string chars = "0123456789";

            if (!numericOnly)
            {
                chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            }

            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }

            return await Task.FromResult(stringBuilder.ToString());
        }

        /// <inheritdoc/>
        public bool CheckLoggedInUserAuthorizationAsync(UserClaimsViewModel? model)
        {
            // If user claims is null then we will throw 401.
            if (model == null)
            {
                throw new UnauthorizedAccessException(this.localizer["UserNotFound"]);
            }

            var userDetails = this.context.Users
                .AsNoTracking()
                .FirstOrDefault(x => x.Email == model.PrimaryEmail);

            // If user has claims details but user is not exists in the application then we will throw 401.
            if (userDetails == null)
            {
                throw new UnauthorizedAccessException(this.localizer["UserNotFound"]);
            }
            else
            {
                // if user is not active or deleted in that case we will throw exception.
                if (!userDetails.IsActive || userDetails.IsDeleted)
                {
                    throw new UnauthorizedAccessException(this.localizer["UserNotFound"]);
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> IsUserAuthorizedAsync(
            int currentUserId,
            string activity,
            int? countryId = null,
            int? planId = null,
            int? strategicActionId = null,
            int? detailedActivityId = null,
            int? userId = null,
            string? region = null)
        {
            bool result = false;
            List<SqlParameter> sqlParameters = new ()
            {
                new SqlParameter() { ParameterName = "@currentUserId", Value = currentUserId },
                new SqlParameter() { ParameterName = "@activity", Value = activity },
                new SqlParameter() { ParameterName = "@countryId", Value = countryId },
                new SqlParameter() { ParameterName = "@planId", Value = planId },
                new SqlParameter() { ParameterName = "@strategicActionId", Value = strategicActionId },
                new SqlParameter() { ParameterName = "@detailedActivityId", Value = detailedActivityId },
                new SqlParameter() { ParameterName = "@userId", Value = userId },
                new SqlParameter() { ParameterName = "@region", Value = region },
            };
            await this.context.ExecuteProcedureAsync(
                CommandType.StoredProcedure,
                "sp_IsCurrentUserHasPermission",
                (ds) =>
                {
                    result = (bool)ds.Tables[0]?.Rows[0]?.ItemArray[0] !;
                },
                sqlParameters);

            return result;
        }
    }
}