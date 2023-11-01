// <copyright file="ExcelImportIHRRecommendationsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

#nullable disable

namespace WHO.NAPHS.BusinessLogic.ViewModels.Excel
{
    /// <summary>
    /// Excel import view model.
    /// </summary>
    public class ExcelImportIHRRecommendationsViewModel
    {
        /// <summary>
        /// Gets or sets indicator id.
        /// </summary>
        public string IndicatorId { get; set; }

        /// <summary>
        /// Gets or sets benchMark.
        /// </summary>
        public string BenchMark { get; set; }

        /// <summary>
        /// Gets or sets objective.
        /// </summary>
        public string Objectives { get; set; }

        /// <summary>
        /// Gets or sets capacity.
        /// </summary>
        public string Capacity { get; set; }

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
        public string Actions { get; set; }
    }
}