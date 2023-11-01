// <copyright file="DetailActivityViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.StrategicAction
{
    /// <summary>
    /// Detailed activity view model.
    /// </summary>
    public class DetailActivityViewModel
    {
        /// <summary>
        /// Gets or sets DetailedActivityId.
        /// </summary>
        public int DetailedActivityId { get; set; }

        /// <summary>
        /// Gets or sets StrategicActionId.
        /// </summary>
        public int StrategicActionId { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets StartDate.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets EndDate.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets ImplementationStatus.
        /// </summary>
        public int ImplementationStatus { get; set; }

        /// <summary>
        /// Gets or sets Feasibility.
        /// </summary>
        public int Feasibility { get; set; }

        /// <summary>
        /// Gets or sets Impact.
        /// </summary>
        public int Impact { get; set; }

        /// <summary>
        /// Gets or sets Priority.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets Source.
        /// </summary>
        public int? Source { get; set; }

        /// <summary>
        /// Gets or sets RiskName.
        /// </summary>
        public string? RiskName { get; set; }

        /// <summary>
        /// Gets or sets RiskLevel.
        /// </summary>
        public int? RiskLevel { get; set; }

        /// <summary>
        /// Gets or sets Deadline from STAR.
        /// </summary>
        public string? Deadline { get; set; }

        /// <summary>
        /// Gets or sets Responsible.
        /// </summary>
        public string? Responsible { get; set; }

        /// <summary>
        /// Gets or sets ResponsibleAuthority.
        /// </summary>
        public string? ResponsibleAuthority { get; set; }

        /// <summary>
        /// Gets or sets InstituteId.
        /// </summary>
        public int? InstituteId { get; set; }

        /// <summary>
        /// Gets or sets CostAssumptions.
        /// </summary>
        public string? CostAssumptions { get; set; }

        /// <summary>
        /// Gets or sets EstimatedCost.
        /// </summary>
        public double? EstimatedCost { get; set; }

        /// <summary>
        /// Gets or sets FundAvailability.
        /// </summary>
        public bool? FundAvailability { get; set; }

        /// <summary>
        /// Gets or sets EstimatedBudgetSource.
        /// </summary>
        public string? EstimatedBudgetSource { get; set; }

        /// <summary>
        /// Gets or sets ExistingBudget.
        /// </summary>
        public double? ExistingBudget { get; set; }

        /// <summary>
        /// Gets or sets FinancialGap.
        /// </summary>
        public double? FinancialGap { get; set; }

        /// <summary>
        /// Gets or sets NeedTechnicalAssistance.
        /// </summary>
        public bool? NeedTechnicalAssistance { get; set; }

        /// <summary>
        /// Gets or sets NeedFinancialAssistance.
        /// </summary>
        public bool? NeedFinancialAssistance { get; set; }

        /// <summary>
        /// Gets or sets DonorContribution.
        /// </summary>
        public double? DonorContribution { get; set; }

        /// <summary>
        /// Gets or sets Donor.
        /// </summary>
        public string? Donor { get; set; }

        /// <summary>
        /// Gets or sets ActualCost.
        /// </summary>
        public double? ActualCost { get; set; }

        /// <summary>
        /// Gets or sets Comments.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Gets or sets Activity.
        /// </summary>
        public string? Activity { get; set; }

        /// <summary>
        /// Gets or sets ActivityTypeId.
        /// </summary>
        public string ActivityTypeIds { get; set; } = string.Empty;
    }
}