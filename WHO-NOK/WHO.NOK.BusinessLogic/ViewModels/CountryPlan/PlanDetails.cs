// <copyright file="PlanDetails.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.CountryPlan
{
    /// <summary>
    /// Represents the details of a plan.
    /// </summary>
    public class PlanDetails
    {
        /// <summary>
        /// Gets or sets value of Technical Area.
        /// </summary>
        public string TechnicalArea { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets value of indicator.
        /// </summary>
        public string Indicator { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets encrypted value of indicator id.
        /// </summary>
        public string PlanIndicatorId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets value of indicator score.
        /// </summary>
        public int IndicatorScore { get; set; }

        /// <summary>
        /// Gets or sets value of indicator goal.
        /// </summary>
        public int IndicatorGoal { get; set; }

        /// <summary>
        /// Gets or sets encrypted value of strategic action id.
        /// </summary>
        public string StrategicAction { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets value of strategic action name.
        /// </summary>
        public string? Action { get; set; }

        /// <summary>
        /// Gets or sets value of objective of a strategic action.
        /// </summary>
        public string? Objective { get; set; }

        /// <summary>
        /// Gets or sets value of responsible authority for strategic action .
        /// </summary>
        public string? StrategicActionResponsibleAuthority { get; set; }

        /// <summary>
        /// Gets or sets value of strategic action estimated cost.
        /// </summary>
        public double StrategicActionEstimatedCost { get; set; }

        /// <summary>
        /// Gets or sets value of comments.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Gets or sets value of strategic action source.
        /// </summary>
        public int StrategicActionSource { get; set; }

        /// <summary>
        /// Gets or sets value of strategic action source.
        /// </summary>
        public int StrategicActionImpact { get; set; }

        /// <summary>
        /// Gets or sets value of strategic action source.
        /// </summary>
        public int StrategicActionFeasibility { get; set; }

        /// <summary>
        /// Gets or sets value of strategic action source.
        /// </summary>
        public int StrategicActionPriority { get; set; }

        /// <summary>
        /// Gets or sets value of strategic action score.
        /// </summary>
        public int StrategicActionScore { get; set; }

        /// <summary>
        /// Gets or sets value of strategic action goal.
        /// </summary>
        public int StrategicActionGoal { get; set; }

        /// <summary>
        /// Gets or sets encrypted value of detailed activity id.
        /// </summary>
        public string DetailedActivity { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets value of description of detailed activity.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets value of start date of detailed activity.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets value of end date of detailed activity.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets value of implementation status.
        /// </summary>
        public string ImplementationStatus { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets value of detailed activity source.
        /// </summary>
        public int DetailedActivitySource { get; set; }

        /// <summary>
        /// Gets or sets value of strategic action source.
        /// </summary>
        public int DetailedActivityImpact { get; set; }

        /// <summary>
        /// Gets or sets value of strategic action source.
        /// </summary>
        public int DetailedActivityFeasibility { get; set; }

        /// <summary>
        /// Gets or sets value of strategic action source.
        /// </summary>
        public int DetailedActivityPriority { get; set; }

        /// <summary>
        /// Gets or sets value of risk name.
        /// </summary>
        public string? RiskName { get; set; }

        /// <summary>
        /// Gets or sets value of deadline.
        /// </summary>
        public string? Deadline { get; set; }

        /// <summary>
        /// Gets or sets the types of the activity.
        /// </summary>
        public string? Activity { get; set; }

        /// <summary>
        /// Gets or sets value of responsible.
        /// </summary>
        public string? Responsible { get; set; }

        /// <summary>
        /// Gets or sets value of responsible authority of detailed activity.
        /// </summary>
        public string? DetailedActivityResponsibleAuthority { get; set; }

        /// <summary>
        /// Gets or sets value of cost assumptions.
        /// </summary>
        public string? CostAssumptions { get; set; }

        /// <summary>
        /// Gets or sets value of estimated cost of detailed activity.
        /// </summary>
        public double DetailedActivityEstimatedCost { get; set; }

        /// <summary>
        /// Gets or sets value of fund availability.
        /// </summary>
        public string FundAvailability { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets value of Estimated Budget Source.
        /// </summary>
        public string? EstimatedBudgetSource { get; set; }

        /// <summary>
        /// Gets or sets value of existing budget.
        /// </summary>
        public double ExistingBudget { get; set; }

        /// <summary>
        /// Gets or sets value of financial gap.
        /// </summary>
        public double FinancialGap { get; set; }

        /// <summary>
        /// Gets or sets value of technical assistance.
        /// </summary>
        public string TechnicalAssistance { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets value of financial assistance.
        /// </summary>
        public string FinancialAssistance { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets value of donor contribution.
        /// </summary>
        public double DonorContribution { get; set; }

        /// <summary>
        /// Gets or sets value of donor.
        /// </summary>
        public string? Donor { get; set; }

        /// <summary>
        /// Gets or sets value of actual cost.
        /// </summary>
        public double ActualCost { get; set; }

        /// <summary>
        /// Gets or sets value of detailed activity comments.
        /// </summary>
        public string? DetailedActivityComments { get; set; }
    }
}