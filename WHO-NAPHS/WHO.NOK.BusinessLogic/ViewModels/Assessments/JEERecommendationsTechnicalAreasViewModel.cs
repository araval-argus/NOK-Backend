// <copyright file="JEERecommendationsTechnicalAreasViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.Assessments
{
    /// <summary>
    /// Jee technical areas view model for recommendations.
    /// </summary>
    public class JEERecommendationsTechnicalAreasViewModel
    {
        /// <summary>
        /// Gets or sets technical areas.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets technical areas.
        /// </summary>
        public int TechnicalAreaId { get; set; }

        /// <summary>
        /// Gets or sets area code.
        /// </summary>
        public string? AreaCode { get; set; }

        /// <summary>
        /// Gets or sets area code id.
        /// </summary>
        public string? AreaCodeId { get; set; }

        /// <summary>
        /// Gets or sets Recommendations.
        /// </summary>
        public List<string> Recommendations { get; set; } = new ();

        /// <summary>
        /// Gets or sets list of <see cref="JEEStrengthsAndChallengesViewModel"/> models.
        /// </summary>
        public List<JEEStrengthsAndChallengesViewModel> StrengthsAndChallenges { get; set; } = new ();
    }
}