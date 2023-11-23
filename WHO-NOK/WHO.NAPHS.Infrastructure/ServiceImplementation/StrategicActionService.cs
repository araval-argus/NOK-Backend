// <copyright file="StrategicActionService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress possibly null reference warnings.
#pragma warning disable CS8604

namespace WHO.NOK.Infrastructure.ServiceImplementation
{
    using System.Data;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using Newtonsoft.Json;
    using WHO.NOK.BusinessLogic.Features;
    using WHO.NOK.BusinessLogic.Features.StrategicAction;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NOK.BusinessLogic.ViewModels.StrategicAction;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Common.Resources;
    using WHO.NOK.Core.Wrappers;
    using WHO.NOK.Infrastructure.Helper;
    using WHO.NOK.Infrastructure.Models.DatabaseContext;
    using WHO.NOK.Infrastructure.Models.Plans;

    /// <summary>
    /// Implement <see cref="IStrategicActionService"/> service.
    /// </summary>
    public class StrategicActionService : IStrategicActionService
    {
        private readonly ApplicationDbContext context;
        private readonly ICommonService commonService;
        private readonly IStringLocalizer<Resources> localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategicActionService"/> class.
        /// </summary>
        /// <param name="context"> Database context.</param>
        /// <param name="commonService"> Common service.</param>
        /// <param name="localizer">String localizer.</param>
        public StrategicActionService(
            ApplicationDbContext context,
            ICommonService commonService,
            IStringLocalizer<Resources> localizer)
        {
            this.context = context;
            this.commonService = commonService;
            this.localizer = localizer;
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> AddJEERecommendationAsync(AddJEERecommendationCommand request)
        {
            List<JEERecommendationOutputViewModel> output = new ();

            List<SqlParameter> parameters = new ()
            {
                new () { ParameterName = "@PlanId", Value = request.PlanId },
                new () { ParameterName = "@UserId", Value = request.User?.UserId },
                new () { ParameterName = "@Recommendations", Value = JsonConvert.SerializeObject(request.JeeRecommendations) },
            };

            await this.context.ExecuteProcedureAsync(
                 CommandType.StoredProcedure,
                 "sp_AddJEERecommendations",
                 (ds) =>
                 {
                    foreach (DataTable dt in ds.Tables)
                    {
                        dt.TableName = dt.Columns[0].ColumnName;
                    }

                    output = ds.Tables["OutputTable"].ConvertToList<JEERecommendationOutputViewModel>();
                },
                 parameters,
                 1800);

            return await this.commonService.GetPlanDetailsAsync(request.PlanId);
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> CreateCustomStrategicActionAsync(CreateCustomStrategicActionCommand request)
        {
            var action = await this.context.StrategicActions
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Objective == request.Objective
                                                           && x.PlanIndicatorId == request.PlanIndicatorId
                                                            && !x.IsDeleted);

            if (action != null)
            {
                throw new ApiException($"Strategic action already exists for same description.");
            }

            // Calculate the priority based on the feasibility and impact of the plan.
            ActionPriority priority = await this.commonService.GetActionPriorityAsync(request.Feasibility, request.Impact);

            await this.context.StrategicActions.AddAsync(
                new StrategicAction(
                    request.PlanIndicatorId,
                    request.Objective,
                    request.Action,
                    (int)request.Feasibility,
                    (int)request.Impact,
                    (int)priority,
                    Core.Common.PlanStage.NotStarted,
                    request.ResponsibleAuthority,
                    request.EstimatedCost,
                    request.Comments,
                    (int)SourceType.Custom,
                    request.Score,
                    request.Goal));

            await this.context.SaveChangesAsync();

            return await this.commonService.GetPlanDetailsAsync(request.CountryPlanId);
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> EditStrategicAction(EditStrategicActionCommand request)
        {
            var strategicAction = this.context.StrategicActions.Include("CountryPlanIndicator").AsNoTracking().FirstOrDefault(x => x.StrategicActionId == request.StrategicActionId && x.ReferenceId == null && !x.IsDeleted) ??
                throw new Exception(this.localizer["StrategicActionNotFound"]);

            // Calculate the priority based on the feasibility and impact of the plan.
            ActionPriority priority = await this.commonService.GetActionPriorityAsync(request.Feasibility, request.Impact);

            strategicAction.UpdateStrategicAction(
                request.Objective,
                request.Action,
                request.Feasibility.GetHashCode(),
                request.Impact.GetHashCode(),
                priority.GetHashCode(),
                (int)request.ImplementationStatus,
                request.ResponsibleAuthority,
                request.EstimatedCost,
                request.Comments);

            var countryPlan = await this.context.CountryPlans.FirstAsync(x => x.CountryPlanId == strategicAction.CountryPlanIndicator.CountryPlanId);

            // reference entry should be added only if plan is active else directly update.
            if (countryPlan!.PlanStatusId == (int)Core.Common.PlanStatus.Active)
            {
                strategicAction.EditDetailedActivity();
                strategicAction.CountryPlanIndicator = null!;
                await this.context.StrategicActions.AddAsync(strategicAction);
            }
            else
            {
                this.context.StrategicActions.Update(strategicAction);
            }

            await this.context.SaveChangesAsync();

            return await this.commonService.GetPlanDetailsAsync(countryPlan.CountryPlanId);
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> UpdateStrategicAction(UpdateStrategicActionCommand request)
        {
            var strategicAction = this.context.StrategicActions.FirstOrDefault(x => x.StrategicActionId == request.StrategicActionId && x.ReferenceId != null && !x.IsDeleted) ??
                throw new Exception(this.localizer["StrategicActionNotFound"]);

            var strategicActionToBeUpdated = this.context.StrategicActions.FirstOrDefault(x => x.StrategicActionId == strategicAction.ReferenceId && !x.IsDeleted) ??
                throw new Exception(this.localizer["StrategicActionNotFound"]);

            // dump data of reference table to strategic action.
            strategicActionToBeUpdated.UpdateStrategicAction(
                strategicAction.Objective,
                strategicAction.Action,
                strategicAction.Feasibility,
                strategicAction.Impact,
                strategicAction.Priority,
                strategicAction.ImplementationStatus,
                strategicAction.ResponsibleAuthority,
                strategicAction.EstimatedCost,
                strategicAction.Comments);

            // delete reference entry from table.
            strategicAction.DeactivateStrategicAction();

            await this.context.SaveChangesAsync();

            return await this.commonService.GetPlanDetailsAsync(request.CountryPlanId);
        }

        /// <inheritdoc/>
        public async Task<CompletePlanDetailsViewModel> DeleteStrategicActionAsync(DeleteStrategicActionCommand request)
        {
            var strategicAction = await this.context.StrategicActions.FirstOrDefaultAsync(x => x.StrategicActionId == request.StrategicActionId && !x.IsDeleted) ??
                throw new Exception(this.localizer["StrategicActionNotFound"]);

            strategicAction.DeactivateStrategicAction();

            var detailedActivities = await this.context.DetailedActivities.Where(x => x.StrategicActionId == request.StrategicActionId).ToListAsync();
            foreach (var detailedActivity in detailedActivities)
            {
                detailedActivity.DeactivateDetailedActivity();
            }

            await this.context.SaveChangesAsync();

            return await this.commonService.GetPlanDetailsAsync(request.CountryPlanId);
        }
    }
}