// <copyright file="JEEChallengesAndStrengthsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Assessments
{
    using Newtonsoft.Json;

    /// <summary>
    /// JEE challenges and strengths view model.
    /// </summary>
    public class JEEChallengesAndStrengthsViewModel
    {
        /// <summary>
        /// Gets or sets indicator.
        /// </summary>
        [JsonProperty("indicator")]
        public string? Indicator { get; set; }

        /// <summary>
        /// Gets or sets strengths.
        /// </summary>
        [JsonProperty("strengths")]
        public string? Strengths { get; set; }

        /// <summary>
        /// Gets or sets challenges.
        /// </summary>
        [JsonProperty("challenges")]
        public string? Challenges { get; set; }
    }
}