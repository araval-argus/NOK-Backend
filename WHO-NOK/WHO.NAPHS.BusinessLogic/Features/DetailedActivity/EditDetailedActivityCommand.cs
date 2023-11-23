// <copyright file="EditDetailedActivityCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.DetailedActivity
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Command for update detailed activity.
    /// </summary>
    public class EditDetailedActivityCommand : Request
    {
        /// <summary>
        /// Gets or sets detailed activity id.
        /// </summary>
        public int DetailedActivityId { get; set; }

        /// <summary>
        /// Gets or sets description of detailed activity.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets description of activity type ids.
        /// </summary>
        public HashSet<int> ActivityTypes { get; set; } = new ();

        /// <summary>
        /// Gets or sets the start date of the detailed activity.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the detailed activity.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets feasibility.
        /// </summary>
        public DetailedActivityFeasibility Feasibility { get; set; }

        /// <summary>
        /// Gets or sets Impact.
        /// </summary>
        public DetailedActivityImpact Impact { get; set; }

        /// <summary>
        /// Gets or sets Priority.
        /// </summary>
        public DetailedActivityPriority Priority { get; set; }

        /// <summary>
        /// Gets or sets Responsible person.
        /// </summary>
        public string? Responsible { get; set; }

        /// <summary>
        /// Gets or sets Responsible Authority.
        /// </summary>
        public string? ResponsibleAuthority { get; set; }

        /// <summary>
        /// Gets or sets comments.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Gets or sets Estimated Cost.
        /// </summary>
        public double EstimatedCost { get; set; }

        /// <summary>
        /// Gets or sets Cost Assumptions.
        /// </summary>
        public string? CostAssumptions { get; set; }

        /// <summary>
        /// Gets or sets NeedTechnicalAssistance.
        /// </summary>
        public bool? NeedTechnicalAssistance { get; set; }

        /// <summary>
        /// Gets or sets NeedFinancialAssistance.
        /// </summary>
        public bool? NeedFinancialAssistance { get; set; }
    }
}