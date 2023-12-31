// <copyright file="IHRRecommendationsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Excel
{
    /// <summary>
    /// IHR Recommendations ViewModel.
    /// </summary>
    public class IHRRecommendationsViewModel
    {
        /// <summary>
        /// Gets or sets IHR recommendation id.
        /// </summary>
        public int IHRRecommendationId { get; set; }

        /// <summary>
        /// Gets or sets indicator id.
        /// </summary>
        public string IndicatorId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets benchMark.
        /// </summary>
        public string BenchMark { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets objective.
        /// </summary>
        public string Objectives { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets capacity.
        /// </summary>
        public string Capacity { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets previous score.
        /// </summary>
        public int PreviousScore { get; set; }

        /// <summary>
        /// Gets or sets target score.
        /// </summary>
        public int TargetScore { get; set; }

        /// <summary>
        /// Gets or sets actions.
        /// </summary>
        public string Actions { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int? CountryPlanId { get; set; }
    }
}