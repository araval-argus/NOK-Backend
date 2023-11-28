// <copyright file="AuditDetailedActivities.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Audit
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Audit detailed activities DB model.
    /// </summary>
    public class AuditDetailedActivities : ITrackable
    {
        /// <summary>
        /// Gets or sets DetailedActivityId.
        /// </summary>
        [Key]
        public int AuditDetailedActivityId { get; protected set; }

        /// <summary>
        /// Gets or sets audit log status.
        /// </summary>
        public AuditLogRowStatus AuditLogRowStatus { get; set; }

        /// <summary>
        /// Gets or sets DetailedActivityId.
        /// </summary>
        public int DetailedActivityId { get; protected set; }

        /// <summary>
        /// Gets or sets StrategicActionId.
        /// </summary>
        public int StrategicActionId { get; protected set; }

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
    }
}