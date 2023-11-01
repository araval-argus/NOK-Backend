// <copyright file="CreateNBWActionCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.Excel
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to import excel data from the front end by parsing the excel file.
    /// </summary>
    public class CreateNBWActionCommand : Request
    {
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
        /// Gets or sets tags.
        /// </summary>
        public string? Tags { get; set; }

        /// <summary>
        /// Gets or sets detailed activity.
        /// </summary>
        public string DetailedActivity { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets feasibility.
        /// </summary>
        public int Feasibility { get; set; }

        /// <summary>
        /// Gets or sets impact.
        /// </summary>
        public int Impact { get; set; }

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
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanId { get; set; }
    }
}