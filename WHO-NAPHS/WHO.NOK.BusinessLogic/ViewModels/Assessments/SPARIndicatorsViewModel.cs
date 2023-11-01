// <copyright file="SPARIndicatorsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Assessments
{
    /// <summary>
    /// View model to spar indicators.
    /// </summary>
    public class SPARIndicatorsViewModel
    {
        /// <summary>
        /// Gets or sets indicator name.
        /// </summary>
        public string IndicatorName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets indicator number.
        /// </summary>
        public string IndicatorNo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets indicator sequence.
        /// </summary>
        public int? IndicatorSeq { get; set; }

        /// <summary>
        /// Gets or sets indicator score.
        /// </summary>
        public int? IndicatorScore { get; set; }

        /// <summary>
        /// Gets or sets attribute.
        /// </summary>
        public string Attribute { get; set; } = string.Empty;
    }
}