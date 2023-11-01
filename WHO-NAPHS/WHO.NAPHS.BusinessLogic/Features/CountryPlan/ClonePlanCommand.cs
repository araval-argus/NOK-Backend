// <copyright file="ClonePlanCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command for get the plan details.
    /// </summary>
    public class ClonePlanCommand : Request
    {
        /// <summary>
        /// Gets or sets plan id.
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include strategic actions.
        /// </summary>
        public bool IncludeStrategicActions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include priorities.
        /// </summary>
        public bool IncludePriority { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include estimated budget.
        /// </summary>
        public bool IncludeEstimatedBudget { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include responsible authority.
        /// </summary>
        public bool IncludeResponsibleAuthority { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether include comments.
        /// </summary>
        public bool IncludeComments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether update the latest score.
        /// </summary>
        public bool UpdateLatestScore { get; set; }
    }
}