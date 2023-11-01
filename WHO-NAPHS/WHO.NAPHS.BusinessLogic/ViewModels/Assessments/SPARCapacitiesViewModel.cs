// <copyright file="SPARCapacitiesViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.Assessments
{
    /// <summary>
    /// View model to spar capacities.
    /// </summary>
    public class SPARCapacitiesViewModel
    {
        /// <summary>
        /// Gets or sets capacity name.
        /// </summary>
        public string CapacityName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets capacity number.
        /// </summary>
        public string CapacityNo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets capacity sequence.
        /// </summary>
        public int? CapacitySeq { get; set; }

        /// <summary>
        /// Gets or sets global average.
        /// </summary>
        public int? GlobalAvg { get; set; }

        /// <summary>
        /// Gets or sets regional average.
        /// </summary>
        public int? RegionalAvg { get; set; }

        /// <summary>
        /// Gets or sets country average.
        /// </summary>
        public int? CountryAvg { get; set; }

        /// <summary>
        /// Gets or sets list of indicators.
        /// </summary>
        public List<SPARIndicatorsViewModel> Indicators { get; set; } = new List<SPARIndicatorsViewModel>();
    }
}