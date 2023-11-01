// <copyright file="AddNBWActionsToPlanCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to add NBW Actions to plan.
    /// </summary>
    public class AddNBWActionsToPlanCommand : Request
    {
        /// <summary>
        /// Gets or sets nbw actions ids to be added in plan.
        /// </summary>
        public List<int> NBWActions { get; set; } = new ();

        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanId { get; set; }
    }
}