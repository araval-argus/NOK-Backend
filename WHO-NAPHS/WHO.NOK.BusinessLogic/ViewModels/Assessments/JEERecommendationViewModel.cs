// <copyright file="JEERecommendationViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.Assessments
{
    using Newtonsoft.Json;

    /// <summary>
    /// View model for JEE recommendations from the external API.
    /// </summary>
    public class JEERecommendationViewModel
    {
        /// <summary>
        /// Gets or sets technical areas.
        /// </summary>
        [JsonProperty("technicalArea")]
        public string? TechnicalArea { get; set; }

        /// <summary>
        /// Gets or sets Recommendations for each technical area.
        /// </summary>
        [JsonProperty("recommendations")]
        public List<string> Recommendations { get; set; } = null!;

        /// <summary>
        /// Gets or sets list of <see cref="JEEChallengesAndStrengthsViewModel"/> models.
        /// </summary>
        [JsonProperty("strengthsAndChallenges")]
        public List<JEEChallengesAndStrengthsViewModel> StrengthsAndChallenges { get; set; } = null!;
    }
}