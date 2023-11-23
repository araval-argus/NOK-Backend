// <copyright file="NBWRecommendationsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Excel
{
    using WHO.NOK.Core.Common;

    /// <summary>
    /// NBW Recommendations ViewModel.
    /// </summary>
    public class NBWRecommendationsViewModel
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int NBWRecommendationId { get; set; }

        /// <summary>
        /// Gets or sets Source.
        /// </summary>
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets set of indicator.
        /// </summary>
        public string SetOfIndicator { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets grouping in RoadMap.
        /// </summary>
        public string GroupingInRoadMap { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets technical area.
        /// </summary>
        public string TechnicalArea { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets indicator id.
        /// </summary>
        public string IndicatorId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets indicator code.
        /// </summary>
        public string IndicatorCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets indicator name.
        /// </summary>
        public string IndicatorName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets objective.
        /// </summary>
        public string Objective { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets strategic action.
        /// </summary>
        public string StrategicAction { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets detailed activity.
        /// </summary>
        public string DetailedActivity { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets <see cref="ActionFeasibility"/> enum.
        /// </summary>
        public ActionFeasibility? Feasibility { get; set; }

        /// <summary>
        /// Gets or sets <see cref="ActionImpact"/> enum.
        /// </summary>
        public ActionImpact? Impact { get; set; }

        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets end date.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets responsible authority.
        /// </summary>
        public string? ResponsibleAuthority { get; set; }

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets plan id.
        /// </summary>
        public int? PlanId { get; set; }

        /// <summary>
        /// Gets or sets planning tool id.
        /// </summary>
        public int? PlanningToolId { get; set; }

        /// <summary>
        /// Gets or sets tags.
        /// </summary>
        public string? Tags { get; set; }
    }
}