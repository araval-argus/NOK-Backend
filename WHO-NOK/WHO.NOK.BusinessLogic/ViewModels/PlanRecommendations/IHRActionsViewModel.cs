// <copyright file="IHRActionsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.PlanRecommendations
{
    /// <summary>
    /// Area technical indicators view model.
    /// </summary>
    public class IHRActionsViewModel
    {
        /// <summary>
        /// Gets or Sets IHR Recommendation id.
        /// </summary>
        public int IHRRecommendationId { get; set; }

        /// <summary>
        /// Gets or sets Action.
        /// </summary>
        public string? Action { get; set; }

        /// <summary>
        /// Gets or sets Target Score.
        /// </summary>
        public int TargetScore { get; set; }
    }
}