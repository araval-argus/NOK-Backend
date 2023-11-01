// <copyright file="CompletePlanDetailsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan
{
    /// <summary>
    /// Plan details view model to get the complete plan details for planning page.
    /// </summary>
    public class CompletePlanDetailsViewModel
    {
        /// <summary>
        /// Gets or sets country plans.
        /// </summary>
        public CountryPlanViewModel? CountryPlan { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="TechnicalAreaViewModel"/> models.
        /// </summary>
        public List<TechnicalAreaViewModel> TechnicalAreas { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="PlanStatisticsViewModel"/> object.
        /// </summary>
        public PlanStatisticsViewModel PlanStatistics { get; set; } = new PlanStatisticsViewModel();
    }
}