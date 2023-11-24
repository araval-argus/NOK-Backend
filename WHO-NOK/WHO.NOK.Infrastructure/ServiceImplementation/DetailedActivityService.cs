// <copyright file="DetailedActivityService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.ServiceImplementation
{
    using System.Data;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using WHO.NOK.BusinessLogic.Features.DetailedActivity;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NOK.BusinessLogic.ViewModels.DetailedActivity;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Common.Resources;
    using WHO.NOK.Infrastructure.Helper;
    using WHO.NOK.Infrastructure.Models.DatabaseContext;
    using WHO.NOK.Infrastructure.Models.Plans;

    /// <summary>
    /// Implement the <see cref="IDetailedActivityService"/>.
    /// </summary>
    public class DetailedActivityService : IDetailedActivityService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<Resources> localizer;
        private readonly ICommonService commonService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailedActivityService"/> class.
        /// </summary>
        /// <param name="context">ApplicationDbContext.</param>
        /// <param name="mapper">Mapper.</param>
        /// <param name="localizer">StringLocalizer.</param>
        /// <param name="commonService"><see cref="ICommonService"/> service.</param>
        public DetailedActivityService(
            ApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<Resources> localizer,
            ICommonService commonService)
        {
            this.context = context;
            this.mapper = mapper;
            this.localizer = localizer;
            this.commonService = commonService;
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> CreateDetailedActivityAsync(CreateDetailedActivityCommand request)
        {
            request.ActivityTypes ??= new ();

            var activityTypesString = string.Empty;
            foreach (var x in request.ActivityTypes!)
            {
                activityTypesString += "," + x;
            }

            // store detailed activity.
            var detailedActivity = new DetailedActivity(
                request.StrategicActionId,
                request.Description,
                activityTypesString.Length > 0 ? activityTypesString[1..] : null,
                request.StartDate,
                request.EndDate,
                Core.Common.PlanStage.NotStarted,
                request.Feasibility.GetHashCode(),
                request.Impact.GetHashCode(),
                request.Priority.GetHashCode(),
                (int)SourceType.Custom,
                request.Responsible,
                request.ResponsibleAuthority,
                request.Comments,
                request.EstimatedCost,
                request.CostAssumptions,
                request.NeedTechnicalAssistance,
                request.NeedFinancialAssistance);

            await this.context.DetailedActivities.AddAsync(detailedActivity);
            await this.context.SaveChangesAsync();

            List<SqlParameter> parameters = new ()
            {
                new () { ParameterName = "@StrategicActionId", Value = request.StrategicActionId },
                new () { ParameterName = "@UserId", Value = request.User!.UserId },
            };

            await this.context.ExecuteNonQueryAsync(
                CommandType.StoredProcedure,
                "sp_UpdateStrategicActionsEstimatedCost",
                parameters,
                1800);

            return await this.commonService.GetPlanDetailsAsync(request.CountryPlanId);
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> UpdateDetailedActivityAsync(UpdateDetailedActivityCommand request)
        {
            var detailedActivity = await this.context.DetailedActivities.FirstOrDefaultAsync(e => e.DetailedActivityId == request.DetailedActivityId && e.ReferenceId != null && !e.IsDeleted) ??
                throw new Exception(this.localizer["DetailedActivityNotFound"]);

            var detailedActivityToBeUpdated = await this.context.DetailedActivities.FirstOrDefaultAsync(e => e.DetailedActivityId == detailedActivity.ReferenceId && !e.IsDeleted) ??
                throw new Exception(this.localizer["DetailedActivityNotFound"]);

            var flag = detailedActivity.EstimatedCost != detailedActivityToBeUpdated.EstimatedCost;

            detailedActivityToBeUpdated.UpdateDetailedActivity(
                detailedActivity.Description,
                detailedActivity.ActivityTypeIds,
                detailedActivity.StartDate,
                detailedActivity.EndDate,
                detailedActivity.Feasibility.GetHashCode(),
                detailedActivity.Impact.GetHashCode(),
                detailedActivity.Priority.GetHashCode(),
                detailedActivity.Responsible,
                detailedActivity.ResponsibleAuthority,
                detailedActivity.Comments,
                detailedActivity.EstimatedCost,
                detailedActivity.CostAssumptions,
                detailedActivity.NeedTechnicalAssistance,
                detailedActivity.NeedFinancialAssistance);

            detailedActivity.DeactivateDetailedActivity();

            await this.context.SaveChangesAsync();

            if (flag)
            {
                List<SqlParameter> parameters = new ()
                {
                    new () { ParameterName = "@StrategicActionId", Value = detailedActivity.StrategicActionId },
                    new () { ParameterName = "@UserId", Value = request.User!.UserId },
                };

                await this.context.ExecuteNonQueryAsync(
                    CommandType.StoredProcedure,
                    "sp_UpdateStrategicActionsEstimatedCost",
                    parameters,
                    1800);
            }

            return await this.commonService.GetPlanDetailsAsync(request.CountryPlanId);
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> EditDetailedActivity(EditDetailedActivityCommand request)
        {
            var detailedActivity = await this.context.DetailedActivities.AsNoTracking().FirstOrDefaultAsync(x => x.DetailedActivityId == request.DetailedActivityId && x.ReferenceId == null && !x.IsDeleted) ??
                throw new Exception(this.localizer["DetailedActivityNotFound"]);

            var activityTypesString = string.Empty;
            foreach (var x in request.ActivityTypes)
            {
                activityTypesString += "," + x;
            }

            var flag = detailedActivity.EstimatedCost != request.EstimatedCost;

            detailedActivity.UpdateDetailedActivity(
                request.Description,
                activityTypesString.Length > 0 ? activityTypesString[1..] : null,
                request.StartDate,
                request.EndDate,
                request.Feasibility.GetHashCode(),
                request.Impact.GetHashCode(),
                request.Priority.GetHashCode(),
                request.Responsible,
                request.ResponsibleAuthority,
                request.Comments,
                request.EstimatedCost,
                request.CostAssumptions,
                request.NeedTechnicalAssistance,
                request.NeedFinancialAssistance);

            var strategicAction = await this.context.StrategicActions.Include("CountryPlanIndicator").FirstAsync(x => x.StrategicActionId == detailedActivity.StrategicActionId);
            var countryPlan = await this.context.CountryPlans.FirstAsync(x => x.CountryPlanId == strategicAction.CountryPlanIndicator.CountryPlanId);

            // reference entry should be added only if plan is active else directly update.
            if (countryPlan!.PlanStatusId == (int)Core.Common.PlanStatus.Active)
            {
                flag = false;
                detailedActivity.EditDetailedActivity();
                await this.context.DetailedActivities.AddAsync(detailedActivity);
            }
            else
            {
                this.context.DetailedActivities.Update(detailedActivity);
            }

            await this.context.SaveChangesAsync();

            if (flag)
            {
                List<SqlParameter> parameters = new ()
                {
                    new () { ParameterName = "@StrategicActionId", Value = detailedActivity.StrategicActionId },
                    new () { ParameterName = "@UserId", Value = request.User!.UserId },
                };

                await this.context.ExecuteNonQueryAsync(
                    CommandType.StoredProcedure,
                    "sp_UpdateStrategicActionsEstimatedCost",
                    parameters,
                    1800);
            }

            return await this.commonService.GetPlanDetailsAsync(countryPlan.CountryPlanId);
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> DeleteDetailedActivityAsync(DeleteDetailedActivityCommand request)
        {
            var detailedActivity = await this.context.DetailedActivities.FirstOrDefaultAsync(x => x.DetailedActivityId == request.DetailedActivityId && !x.IsDeleted) ??
                throw new Exception(this.localizer["DetailedActivityNotFound"]);

            detailedActivity.DeactivateDetailedActivity();
            await this.context.SaveChangesAsync();

            return await this.commonService.GetPlanDetailsAsync(request.CountryPlanId);
        }

        /// <inheritdoc/>
        public async Task<List<DetailedActivityTypeViewModel>> GetDetailedActivityTypesAsync(GetDetailedActivityCommand request)
        {
            var detailedActivities = await this.context.DetailedActivityTypes.Where(x => x.CountryId == null || x.CountryId == request.CountryId).OrderBy(x => x.Activity).ToListAsync();
            return this.mapper.Map<List<DetailedActivityTypeViewModel>>(detailedActivities);
        }

        /// <inheritdoc/>
        public async Task<DetailedActivityTypeViewModel> CreateDetailedActivityTypeAsync(CreateDetailedActivityTypeCommand request)
        {
            var detailedActivity = await this.context.DetailedActivityTypes.FirstOrDefaultAsync(x => (x.CountryId == null || x.CountryId == request.CountryId) && x.Activity!.ToLower().Equals(request.Activity.Trim().ToLower()));

            if (detailedActivity != null)
            {
                throw new Exception(this.localizer["ActivityAlreadyExists"]);
            }

            var detailedActivityType = new DetailedActivityType(request.CountryId, request.Activity.Trim());
            await this.context.DetailedActivityTypes.AddAsync(detailedActivityType);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<DetailedActivityTypeViewModel>(detailedActivityType);
        }
    }
}