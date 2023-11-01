// <copyright file="IHRActionsListCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan
{
    /// <summary>
    /// Command to add IHR Actions list to plan.
    /// </summary>
    public class IHRActionsListCommand
    {
        /// <summary>
        /// Gets or sets ihr action id to be added in plan.
        /// </summary>
        public int IHRRecommendationId { get; set; } = new ();

        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        public int PlanIndicatorId { get; set; }
    }
}