// <copyright file="UpdatedScoreViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan
{
    /// <summary>
    /// Updated score view model.
    /// </summary>
    public class UpdatedScoreViewModel : TechnicalAreaIndicatorViewModel
    {
        /// <summary>
        /// Gets or sets old score.
        /// </summary>
        public int OldScore { get; set; }

        /// <summary>
        /// Gets or sets new score.
        /// </summary>
        public int NewScore { get; set; }

        /// <summary>
        /// Gets or sets old goal.
        /// </summary>
        public int OldGoal { get; set; }

        /// <summary>
        /// Gets or sets new goal.
        /// </summary>
        public int NewGoal { get; set; }

        /// <summary>
        /// Gets or sets technical area name.
        /// </summary>
        public string? TechnicalAreaName { get; set; }
    }
}