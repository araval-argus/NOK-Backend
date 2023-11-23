// <copyright file="PlanStatisticsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.CountryPlan
{
    /// <summary>
    /// Plan statistic view model.
    /// </summary>
    public class PlanStatisticsViewModel
    {
        /// <summary>
        /// Gets or sets cost.
        /// </summary>
        public double? Cost { get; set; }

        /// <summary>
        /// Gets or sets strategic action counts.
        /// </summary>
        public int StrategicActionCounts { get; set; }

        /// <summary>
        /// Gets or sets detailed activity counts.
        /// </summary>
        public int DetailedActivityCounts { get; set; }
    }
}