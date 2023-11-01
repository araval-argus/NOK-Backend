// <copyright file="SelectIHRActions.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.PlanRecommendations
{
    /// <summary>
    /// Class to select ihr actions.
    /// </summary>
    public class SelectIHRActions
    {
        /// <summary>
        /// Gets or Sets Score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or Sets Goal.
        /// </summary>
        public int Goal { get; set; }

        /// <summary>
        /// Gets or sets technical area.
        /// </summary>
        public string TechnicalArea { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets plan indicator id.
        /// </summary>
        public int PlanIndicatorId { get; set; }

        /// <summary>
        /// Gets or Sets IHR Recommendation id.
        /// </summary>
        public int IHRRecommendationId { get; set; }

        /// <summary>
        /// Gets or Sets Indicator code (JEE/SPAR).
        /// </summary>
        public string IndicatorCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets Indicator code id (JEE/SPAR).
        /// </summary>
        public string IndicatorCodeId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets Indicator id.
        /// </summary>
        public string IndicatorId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets BenchMark.
        /// </summary>
        public string BenchMark { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets Actions.
        /// </summary>
        public string Actions { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets Target Score.
        /// </summary>
        public int TargetScore { get; set; }
    }
}