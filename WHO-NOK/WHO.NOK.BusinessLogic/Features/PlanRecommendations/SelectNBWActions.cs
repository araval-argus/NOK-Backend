// <copyright file="SelectNBWActions.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.PlanRecommendations
{
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Class to select nbw actions.
    /// </summary>
    public class SelectNBWActions
    {
        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        public int PlanIndicatorId { get; set; }

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
        /// Gets or Sets NBW Recommendation id.
        /// </summary>
        public int NBWRecommendationId { get; set; }

        /// <summary>
        /// Gets or Sets Indicator name.
        /// </summary>
        public string IndicatorName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets Indicator id.
        /// </summary>
        public string IndicatorId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets Indicator code.
        /// </summary>
        public string IndicatorCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets <see cref="ActionFeasibility"/> enum.
        /// </summary>
        public ActionFeasibility Feasibility { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ActionImpact"/> enum.
        /// </summary>
        public ActionImpact Impact { get; set; }

        /// <summary>
        /// Gets or sets objective.
        /// </summary>
        public string Objective { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets Strategic action.
        /// </summary>
        public string StrategicAction { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets detailed activity.
        /// </summary>
        public string DetailedActivity { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets Start date.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or Sets end date.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or Sets responsible authority.
        /// </summary>
        public string ResponsibleAuthority { get; set; } = string.Empty;
    }
}