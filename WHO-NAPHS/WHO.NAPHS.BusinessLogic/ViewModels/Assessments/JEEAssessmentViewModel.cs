// <copyright file="JEEAssessmentViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.Assessments
{
    using Newtonsoft.Json;

    /// <summary>
    /// View model to store the JEE assessments by country.
    /// </summary>
    public class JEEAssessmentViewModel
    {
        /// <summary>
        /// Gets or sets country name.
        /// </summary>
        [JsonProperty("country")]
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets country ISO Code.
        /// </summary>
        [JsonProperty("iso_code")]
        public string? ISOCode { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="JEERecommendationViewModel"/> recommendations for each technical areas.
        /// </summary>
        [JsonProperty("recommendations")]
        public List<JEERecommendationViewModel> Recommendations { get; set; } = null!;

        /// <summary>
        /// Gets or sets list of <see cref="JEEAssessmentScoreViewModel"/> for technical area indicators.
        /// </summary>
        [JsonProperty("scores")]
        public List<JEEAssessmentScoreViewModel> Scores { get; set; } = null!;
    }
}