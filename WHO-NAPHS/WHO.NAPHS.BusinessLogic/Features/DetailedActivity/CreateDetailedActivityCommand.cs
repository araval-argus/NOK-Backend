// <copyright file="CreateDetailedActivityCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.DetailedActivity
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Command for create detailed activity.
    /// </summary>
    public class CreateDetailedActivityCommand : Request
    {
        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanId { get; set; }

        /// <summary>
        /// Gets or sets strategic action id.
        /// </summary>
        public int StrategicActionId { get; set; }

        /// <summary>
        /// Gets or sets description of detailed activity.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start date of the detailed activity.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the detailed activity.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets <see cref="DetailedActivityFeasibility"/> enum.
        /// </summary>
        public DetailedActivityFeasibility Feasibility { get; set; }

        /// <summary>
        /// Gets or sets <see cref="DetailedActivityImpact"/> enum.
        /// </summary>
        public DetailedActivityImpact Impact { get; set; }

        /// <summary>
        /// Gets or sets <see cref="DetailedActivityPriority"/> enum.
        /// </summary>
        public DetailedActivityPriority Priority { get; set; }

        /// <summary>
        /// Gets or sets Activity type id.
        /// </summary>
        public HashSet<int>? ActivityTypes { get; set; } = new ();

        /// <summary>
        /// Gets or sets Responsible person.
        /// </summary>
        public string? Responsible { get; set; }

        /// <summary>
        /// Gets or sets comments.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Gets or sets Responsible Authority.
        /// </summary>
        public string? ResponsibleAuthority { get; set; }

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