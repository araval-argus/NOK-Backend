// <copyright file="IHRRecommendations.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Excel
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Excel mapping DB Model.
    /// </summary>
    public class IHRRecommendations : ITrackable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IHRRecommendations"/> class.
        /// </summary>
        /// <param name="indicatorId"> Indicator id.</param>
        /// <param name="benchMark"> Bench mark.</param>
        /// <param name="objectives"> Objectives.</param>
        /// <param name="capacity"> Capacity.</param>
        /// <param name="previousScore"> Previous Score.</param>
        /// <param name="targetScore"> Target Score.</param>
        /// <param name="actions"> Actions.</param>
        /// <param name="countryId"> Country id.</param>
        /// <param name="countryPlanId"> Country plan id.</param>
        public IHRRecommendations(
            string indicatorId,
            string benchMark,
            string objectives,
            string capacity,
            int previousScore,
            int targetScore,
            string actions,
            int? countryId,
            int? countryPlanId)
        {
            this.IndicatorId = indicatorId;
            this.BenchMark = benchMark;
            this.Objectives = objectives;
            this.Capacity = capacity;
            this.PreviousScore = previousScore;
            this.TargetScore = targetScore;
            this.Actions = actions;
            this.CountryId = countryId;
            this.CountryPlanId = countryPlanId;
        }

        /// <summary>
        /// Gets or sets IHR recommendation id.
        /// </summary>
        [Key]
        public int IHRRecommendationId { get; protected set; }

        /// <summary>
        /// Gets or sets indicator id.
        /// </summary>
        public string IndicatorId { get; protected set; }

        /// <summary>
        /// Gets or sets benchMark.
        /// </summary>
        public string BenchMark { get; protected set; }

        /// <summary>
        /// Gets or sets objective.
        /// </summary>
        public string Objectives { get; protected set; }

        /// <summary>
        /// Gets or sets capacity.
        /// </summary>
        public string Capacity { get; protected set; }

        /// <summary>
        /// Gets or sets previous score.
        /// </summary>
        public int PreviousScore { get; protected set; }

        /// <summary>
        /// Gets or sets target score.
        /// </summary>
        public int TargetScore { get; protected set; }

        /// <summary>
        /// Gets or sets actions.
        /// </summary>
        public string Actions { get; protected set; }

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int? CountryId { get; protected set; }

        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int? CountryPlanId { get; protected set; }

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