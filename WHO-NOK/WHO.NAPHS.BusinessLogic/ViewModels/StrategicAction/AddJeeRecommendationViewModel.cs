// <copyright file="AddJeeRecommendationViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.StrategicAction
{
    /// <summary>
    /// Add JEE Recommendation view model.
    /// </summary>
    public class AddJeeRecommendationViewModel
    {
        /// <summary>
        /// Gets or sets indicator Id.
        /// </summary>
        public int IndicatorId { get; set; }

        /// <summary>
        /// Gets or sets JEE recommendation.
        /// </summary>
        public string JEERecommendation { get; set; } = null!;
    }
}