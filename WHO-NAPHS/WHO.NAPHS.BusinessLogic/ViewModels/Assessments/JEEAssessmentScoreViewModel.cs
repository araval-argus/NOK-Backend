// <copyright file="JEEAssessmentScoreViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.Assessments
{
    using Newtonsoft.Json;

    /// <summary>
    /// View model to get JEE scores for a indicator of technical area.
    /// </summary>
    public class JEEAssessmentScoreViewModel
    {
        /// <summary>
        /// Gets or sets indicator name.
        /// </summary>
        [JsonProperty("indicator")]
        public string? Indicator { get; set; }

        /// <summary>
        /// Gets or sets score for indicator.
        /// </summary>
        public int? Score { get; set; }
    }
}