// <copyright file="UpdatePlanIndicatorGoalCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to update plan indicator goal.
    /// </summary>
    public class UpdatePlanIndicatorGoalCommand : Request
    {
        /// <summary>
        /// Gets or sets indicator id.
        /// </summary>
        public int PlanIndicatorId { get; set; }

        /// <summary>
        /// Gets or sets the country id of which the plan is associated to.
        /// </summary>
        public int CountryPlanId { get; set; }

        /// <summary>
        /// Gets or sets score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the goal.
        /// </summary>
        public int Goal { get; set; }
    }
}