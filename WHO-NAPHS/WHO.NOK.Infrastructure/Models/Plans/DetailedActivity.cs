// <copyright file="DetailedActivity.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Detailed activity DB model.
    /// </summary>
    public class DetailedActivity : ITrackable, IDeletable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailedActivity"/> class.
        /// </summary>
        public DetailedActivity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailedActivity"/> class.
        /// </summary>
        /// <param name="strategicActionId">Strategic action id.</param>
        /// <param name="description">Description.</param>
        /// <param name="activityTypeIds">Activity type ids.</param>
        /// <param name="startDate">Start date.</param>
        /// <param name="endDate">End date.</param>
        /// <param name="feasibility">Feasibility.</param>
        /// <param name="implementationStatus">Implementation Status.</param>
        /// <param name="impact">Impact.</param>
        /// <param name="priority">Priority.</param>
        /// <param name="source">Source.</param>
        /// <param name="responsible">Responsible Person.</param>
        /// <param name="responsibleAuthority">Responsible Authority.</param>
        /// <param name="comments">Comments.</param>
        /// <param name="estimatedCost">Estimated cost.</param>
        /// <param name="costAssumptions">Cost Assumptions.</param>
        /// <param name="needTechnicalAssistance">Technical Assistance needed or not.</param>
        /// <param name="needFinancialAssistance">Financial Assistance needed or not.</param>
        public DetailedActivity(
            int strategicActionId,
            string? description,
            string? activityTypeIds,
            DateTime startDate,
            DateTime endDate,
            Core.Common.PlanStage implementationStatus,
            int feasibility,
            int impact,
            int priority,
            int? source,
            string? responsible,
            string? responsibleAuthority,
            string? comments,
            double? estimatedCost,
            string? costAssumptions,
            bool? needTechnicalAssistance,
            bool? needFinancialAssistance)
        {
            this.StrategicActionId = strategicActionId;
            this.Description = description;
            this.ActivityTypeIds = activityTypeIds;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.ImplementationStatus = (int)implementationStatus;
            this.Feasibility = feasibility;
            this.Impact = impact;
            this.Priority = priority;
            this.Source = source;
            this.Responsible = responsible;
            this.ResponsibleAuthority = responsibleAuthority;
            this.Comments = comments;
            this.EstimatedCost = estimatedCost;
            this.CostAssumptions = costAssumptions;
            this.NeedTechnicalAssistance = needTechnicalAssistance;
            this.NeedFinancialAssistance = needFinancialAssistance;
        }

        /// <summary>
        /// Gets or sets DetailedActivityId.
        /// </summary>
        [Key]
        public int DetailedActivityId { get; protected set; }

        /// <summary>
        /// Gets or sets ReferenceId.
        /// </summary>
        public int? ReferenceId { get; protected set; }

        /// <summary>
        /// Gets or sets StrategicActionId.
        /// </summary>
        public int StrategicActionId { get; protected set; }

        /// <summary>
        /// Gets or sets ActivityTypeIds.
        /// </summary>
        public string? ActivityTypeIds { get; protected set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets StartDate.
        /// </summary>
        public DateTime? StartDate { get; protected set; }

        /// <summary>
        /// Gets or sets EndDate.
        /// </summary>
        public DateTime? EndDate { get; protected set; }

        /// <summary>
        /// Gets or sets ImplementationStatus.
        /// </summary>
        public int ImplementationStatus { get; protected set; }

        /// <summary>
        /// Gets or sets Feasibility.
        /// </summary>
        public int Feasibility { get; protected set; }

        /// <summary>
        /// Gets or sets Impact.
        /// </summary>
        public int Impact { get; protected set; }

        /// <summary>
        /// Gets or sets Priority.
        /// </summary>
        public int Priority { get; protected set; }

        /// <summary>
        /// Gets or sets Source.
        /// </summary>
        public int? Source { get; protected set; }

        /// <summary>
        /// Gets or sets RiskName.
        /// </summary>
        public string? RiskName { get; protected set; }

        /// <summary>
        /// Gets or sets RiskLevel.
        /// </summary>
        public int? RiskLevel { get; protected set; }

        /// <summary>
        /// Gets or sets Deadline from STAR.
        /// </summary>
        public string? Deadline { get; protected set; }

        /// <summary>
        /// Gets or sets Responsible.
        /// </summary>
        public string? Responsible { get; protected set; }

        /// <summary>
        /// Gets or sets ResponsibleAuthority.
        /// </summary>
        public string? ResponsibleAuthority { get; protected set; }

        /// <summary>
        /// Gets or sets InstituteId.
        /// </summary>
        public int? InstituteId { get; protected set; }

        /// <summary>
        /// Gets or sets CostAssumptions.
        /// </summary>
        public string? CostAssumptions { get; protected set; }

        /// <summary>
        /// Gets or sets EstimatedCost.
        /// </summary>
        public double? EstimatedCost { get; protected set; }

        /// <summary>
        /// Gets or sets FundAvailability.
        /// </summary>
        public bool? FundAvailability { get; protected set; }

        /// <summary>
        /// Gets or sets EstimatedBudgetSource.
        /// </summary>
        public string? EstimatedBudgetSource { get; protected set; }

        /// <summary>
        /// Gets or sets ExistingBudget.
        /// </summary>
        public double? ExistingBudget { get; protected set; }

        /// <summary>
        /// Gets or sets FinancialGap.
        /// </summary>
        public double? FinancialGap { get; protected set; }

        /// <summary>
        /// Gets or sets NeedTechnicalAssistance.
        /// </summary>
        public bool? NeedTechnicalAssistance { get; protected set; }

        /// <summary>
        /// Gets or sets NeedFinancialAssistance.
        /// </summary>
        public bool? NeedFinancialAssistance { get; protected set; }

        /// <summary>
        /// Gets or sets DonorContribution.
        /// </summary>
        public double? DonorContribution { get; protected set; }

        /// <summary>
        /// Gets or sets Donor.
        /// </summary>
        public string? Donor { get; protected set; }

        /// <summary>
        /// Gets or sets ActualCost.
        /// </summary>
        public double? ActualCost { get; protected set; }

        /// <summary>
        /// Gets or sets Comments.
        /// </summary>
        public string? Comments { get; protected set; }

        /// <inheritdoc/>
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc/>
        public int CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime LastUpdatedAt { get; set; }

        /// <inheritdoc/>
        public int LastUpdatedBy { get; set; }

        /// <inheritdoc/>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets <see cref="StrategicActionFeasibility"/> object.
        /// </summary>
        public virtual StrategicActionFeasibility StrategicActionFeasibility { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="StrategicActionImpact"/> object.
        /// </summary>
        public virtual StrategicActionImpact StrategicActionImpact { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="CollaboratingInstitution"/> object.
        /// </summary>
        public virtual CollaboratingInstitution? CollaboratingInstitution { get; set; }

        /// <summary>
        /// Gets or sets <see cref="StrategicActionPriority"/> object.
        /// </summary>
        public virtual StrategicActionPriority StrategicActionPriority { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="StrategicAction"/> object.
        /// </summary>
        public virtual StrategicAction StrategicAction { get; set; } = null!;

        /// <summary>
        /// Update existing detailed activity.
        /// </summary>
        /// <param name="description">Description.</param>
        /// <param name="activityTypeIds">Activity Type ids.</param>
        /// <param name="startDate">Start date.</param>
        /// <param name="endDate">End date.</param>
        /// <param name="feasibility">Feasibility.</param>
        /// <param name="impact">Impact.</param>
        /// <param name="priority">Priority.</param>
        /// <param name="responsible">Responsible Person.</param>
        /// <param name="responsibleAuthority">responsible Authority.</param>
        /// <param name="comments">comments.</param>
        /// <param name="estimatedCost">Estimated cost.</param>
        /// <param name="costAssumptions">Cost Assumptions.</param>
        /// <param name="needTechnicalAssistance">Technical Assistance needed or not.</param>
        /// <param name="needFinancialAssistance">Financial Assistance needed or not.</param>
        public void UpdateDetailedActivity(
            string? description,
            string? activityTypeIds,
            DateTime? startDate,
            DateTime? endDate,
            int feasibility,
            int impact,
            int priority,
            string? responsible,
            string? responsibleAuthority,
            string? comments,
            double? estimatedCost,
            string? costAssumptions,
            bool? needTechnicalAssistance,
            bool? needFinancialAssistance)
        {
            this.Description = description;
            this.ActivityTypeIds = activityTypeIds;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Feasibility = feasibility;
            this.Impact = impact;
            this.Priority = priority;
            this.Responsible = responsible;
            this.ResponsibleAuthority = responsibleAuthority;
            this.Comments = comments;
            this.EstimatedCost = estimatedCost;
            this.CostAssumptions = costAssumptions;
            this.NeedTechnicalAssistance = needTechnicalAssistance;
            this.NeedFinancialAssistance = needFinancialAssistance;
        }

        /// <summary>
        /// Deactivates the detailed activity.
        /// </summary>
        public void DeactivateDetailedActivity()
        {
            this.IsDeleted = true;
        }

        /// <summary>
        /// Edit detailed activity.
        /// </summary>
        public void EditDetailedActivity()
        {
            this.ReferenceId = this.DetailedActivityId;
            this.DetailedActivityId = 0;
        }
    }
}