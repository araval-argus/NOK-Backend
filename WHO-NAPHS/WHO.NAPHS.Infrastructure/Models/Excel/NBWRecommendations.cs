// <copyright file="NBWRecommendations.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Excel
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Excel mapping DB Model.
    /// </summary>
    public class NBWRecommendations : ITrackable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NBWRecommendations"/> class.
        /// </summary>
        /// <param name="source"> Name of the file that has been uploaded.</param>
        /// <param name="setOfIndicator"> Set of indicators.</param>
        /// <param name="groupingInRoadMap"> Grouping in RoadMap.</param>
        /// <param name="technicalArea"> Technical area.</param>
        /// <param name="indicatorId"> Indicator id.</param>
        /// <param name="indicatorCode"> Indicator code.</param>
        /// <param name="indicatorName"> Indicator name.</param>
        /// <param name="objective"> Objective.</param>
        /// <param name="strategicAction"> Strategic Action.</param>
        /// <param name="detailedActivity"> Detailed Activity.</param>
        /// <param name="feasibility"> Feasibility.</param>
        /// <param name="impact"> Impact.</param>
        /// <param name="startDate"> Start date.</param>
        /// <param name="endDate"> End Date.</param>
        /// <param name="responsibleAuthority"> Responsible authority.</param>
        /// <param name="countryId"> Country id.</param>
        /// <param name="countryPlanId"> Plan id.</param>
        /// <param name="planningToolId"> Planning tool id.</param>
        /// <param name="tags"> Tags.</param>
        public NBWRecommendations(
            string source,
            string setOfIndicator,
            string groupingInRoadMap,
            string technicalArea,
            string indicatorId,
            string indicatorCode,
            string indicatorName,
            string objective,
            string strategicAction,
            string detailedActivity,
            int feasibility,
            int impact,
            DateTime? startDate,
            DateTime? endDate,
            string? responsibleAuthority,
            int? countryId,
            int? countryPlanId,
            int? planningToolId,
            string? tags)
        {
            this.Source = source;
            this.SetOfIndicator = setOfIndicator;
            this.GroupingInRoadMap = groupingInRoadMap;
            this.TechnicalArea = technicalArea;
            this.IndicatorId = indicatorId;
            this.IndicatorCode = indicatorCode;
            this.IndicatorName = indicatorName;
            this.Objective = objective;
            this.StrategicAction = strategicAction;
            this.DetailedActivity = detailedActivity;
            this.Feasibility = feasibility;
            this.Impact = impact;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.ResponsibleAuthority = responsibleAuthority;
            this.CountryId = countryId;
            this.CountryPlanId = countryPlanId;
            this.PlanningToolId = planningToolId;
            this.Tags = tags;
        }

        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        [Key]
        public int NBWRecommendationId { get; protected set; }

        /// <summary>
        /// Gets or sets Source.
        /// </summary>
        public string Source { get; protected set; }

        /// <summary>
        /// Gets or sets set of indicator.
        /// </summary>
        public string SetOfIndicator { get; protected set; }

        /// <summary>
        /// Gets or sets grouping in RoadMap.
        /// </summary>
        public string GroupingInRoadMap { get; protected set; }

        /// <summary>
        /// Gets or sets technical area.
        /// </summary>
        public string TechnicalArea { get; protected set; }

        /// <summary>
        /// Gets or sets indicator id.
        /// </summary>
        public string IndicatorId { get; protected set; }

        /// <summary>
        /// Gets or sets indicator code.
        /// </summary>
        public string IndicatorCode { get; protected set; }

        /// <summary>
        /// Gets or sets indicator name.
        /// </summary>
        public string IndicatorName { get; protected set; }

        /// <summary>
        /// Gets or sets objective.
        /// </summary>
        public string Objective { get; protected set; }

        /// <summary>
        /// Gets or sets strategic action.
        /// </summary>
        public string StrategicAction { get; protected set; }

        /// <summary>
        /// Gets or sets detailed activity.
        /// </summary>
        public string DetailedActivity { get; protected set; }

        /// <summary>
        /// Gets or sets feasibility.
        /// </summary>
        public int Feasibility { get; protected set; }

        /// <summary>
        /// Gets or sets impact.
        /// </summary>
        public int Impact { get; protected set; }

        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        public DateTime? StartDate { get; protected set; }

        /// <summary>
        /// Gets or sets end date.
        /// </summary>
        public DateTime? EndDate { get; protected set; }

        /// <summary>
        /// Gets or sets responsible authority.
        /// </summary>
        public string? ResponsibleAuthority { get; protected set; }

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int? CountryId { get; protected set; }

        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int? CountryPlanId { get; protected set; }

        /// <summary>
        /// Gets or sets planning tool id.
        /// </summary>
        public int? PlanningToolId { get; protected set; }

        /// <summary>
        /// Gets or sets tags.
        /// </summary>
        public string? Tags { get; protected set; }

        /// <inheritdoc/>
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc/>
        public int CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime LastUpdatedAt { get; set; }

        /// <inheritdoc/>
        public int LastUpdatedBy { get; set; }
    }
}