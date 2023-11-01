// <copyright file="JEERecommendationOutputViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.StrategicAction
{
    /// <summary>
    /// JEE recommendation output view model.
    /// </summary>
    public class JEERecommendationOutputViewModel
    {
        /// <summary>
        /// Gets or sets Area code.
        /// </summary>
        public string? AreaCode { get; set; }

        /// <summary>
        /// Gets or sets area code id.
        /// </summary>
        public string? AreaCodeId { get; set; }

        /// <summary>
        /// Gets or sets Technical area name.
        /// </summary>
        public string? AreaName { get; set; }

        /// <summary>
        /// Gets or sets message in case of duplicate desc tried to save for one indicator.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets Indicator code.
        /// </summary>
        public string? IndicatorCode { get; set; }

        /// <summary>
        /// Gets or sets indicator Id.
        /// </summary>
        public string? IndicatorCodeId { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string? IndicatorName { get; set; }
    }
}