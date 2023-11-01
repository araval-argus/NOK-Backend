// <copyright file="NBWIndicatorsListViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.PlanRecommendations
{
    /// <summary>
    /// Area technical indicators view model.
    /// </summary>
    public class NBWIndicatorsListViewModel
    {
        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        public int PlanIndicatorId { get; set; }

        /// <summary>
        /// Gets or sets Indicator name.
        /// </summary>
        public string? IndicatorName { get; set; }

        /// <summary>
        /// Gets or sets Indicator code.
        /// </summary>
        public string? IndicatorCode { get; set; }

        /// <summary>
        /// Gets or sets Indicator id.
        /// </summary>
        public string? IndicatorId { get; set; }

        /// <summary>
        /// Gets or Sets Score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or Sets Goal.
        /// </summary>
        public int Goal { get; set; }

        /// <summary>
        /// Gets or sets objective.
        /// </summary>
        public string? Objective { get; set; }

        /// <summary>
        /// Gets or sets relevant list of <see cref="NBWActionsViewModel"/>.
        /// </summary>
        public List<NBWActionsViewModel> NBWActions { get; set; } = new ();
    }
}