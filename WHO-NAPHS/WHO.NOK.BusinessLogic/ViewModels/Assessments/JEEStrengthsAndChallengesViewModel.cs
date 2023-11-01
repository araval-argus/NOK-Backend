// <copyright file="JEEStrengthsAndChallengesViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Assessments
{
    /// <summary>
    /// JEE strengths and challenges view model.
    /// </summary>
    public class JEEStrengthsAndChallengesViewModel
    {
        /// <summary>
        /// Gets or sets Indicator Id.
        /// </summary>
        public int TechnicalAreaIndicatorId { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets Technical area.
        /// </summary>
        public int? TechnicalAreaId { get; set; }

        /// <summary>
        /// Gets or sets Indicator code.
        /// </summary>
        public string? IndicatorCode { get; set; }

        /// <summary>
        /// Gets or sets indicator Id.
        /// </summary>
        public string? IndicatorCodeId { get; set; }

        /// <summary>
        /// Gets or sets strengths.
        /// </summary>
        public string? Strengths { get; set; }

        /// <summary>
        /// Gets or sets challenges.
        /// </summary>
        public string? Challenges { get; set; }
    }
}