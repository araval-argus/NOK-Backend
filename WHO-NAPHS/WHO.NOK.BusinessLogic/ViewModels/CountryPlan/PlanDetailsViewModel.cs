// <copyright file="PlanDetailsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan
{
    /// <summary>
    /// View model for plan details.
    /// </summary>
    public class PlanDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the type of the plan.
        /// </summary>
        public int PlanType { get; set; }

        /// <summary>
        /// Gets or sets the details of the plan.
        /// </summary>
        public List<PlanDetails> Details { get; set; } = new ();
    }
}