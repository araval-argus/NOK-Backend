// <copyright file="IHRIndicatorsListViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.PlanRecommendations
{
    /// <summary>
    /// Area technical indicators view model.
    /// </summary>
    public class IHRIndicatorsListViewModel
    {
        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        public int PlanIndicatorId { get; set; }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public string? BenchMark { get; set; }

        /// <summary>
        /// Gets or sets Indicator code id.
        /// </summary>
        public string? IHRIndicatorId { get; set; }

        /// <summary>
        /// Gets or Sets Indicator code (JEE/SPAR).
        /// </summary>
        public string? IndicatorCode { get; set; }

        /// <summary>
        /// Gets or Sets Indicator code id (JEE/SPAR).
        /// </summary>
        public string? IndicatorCodeId { get; set; }

        /// <summary>
        /// Gets or Sets Score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or Sets Goal.
        /// </summary>
        public int Goal { get; set; }

        /// <summary>
        /// Gets or sets relevant list of <see cref="IHRActionsViewModel"/>.
        /// </summary>
        public List<IHRActionsViewModel> IHRActions { get; set; } = new ();
    }
}