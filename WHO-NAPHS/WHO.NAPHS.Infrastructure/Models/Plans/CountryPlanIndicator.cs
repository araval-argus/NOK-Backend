// <copyright file="CountryPlanIndicator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Infrastructure.Models.Assessments;

    /// <summary>
    /// Plan indicator DB model.
    /// </summary>
    public class CountryPlanIndicator : ITrackable, IDeletable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryPlanIndicator"/> class.
        /// </summary>
        public CountryPlanIndicator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryPlanIndicator"/> class.
        /// </summary>
        /// <param name="indicatorId"> Indicator Id.</param>
        /// <param name="countryPlanId"> Country Plan id.</param>
        /// <param name="score"> Score.</param>
        /// <param name="goal"> Goal.</param>
        public CountryPlanIndicator(int? indicatorId, int countryPlanId, int score, int goal)
        {
            this.Score = score;
            this.Goal = goal;
            this.TechnicalAreaIndicatorId = indicatorId;
            this.CountryPlanId = countryPlanId;
        }

        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        [Key]
        public int PlanIndicatorId { get; protected set; }

        /// <summary>
        /// Gets or sets indicator id.
        /// </summary>
        public int? TechnicalAreaIndicatorId { get; protected set; }

        /// <summary>
        /// Gets or sets Plan id.
        /// </summary>
        public int CountryPlanId { get; protected set; }

        /// <summary>
        /// Gets or sets score.
        /// </summary>
        public int Score { get; protected set; }

        /// <summary>
        /// Gets or sets goal.
        /// </summary>
        public int Goal { get; protected set; }

        /// <inheritdoc/>
        public bool IsDeleted { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc/>
        public int CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime LastUpdatedAt { get; set; }

        /// <inheritdoc/>
        public int LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets <see cref="CountryPlan"/> to which country plan indicator belongs to.
        /// </summary>
        public virtual CountryPlan? CountryPlan { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="TechnicalAreaIndicator"/> object.
        /// </summary>
        public virtual TechnicalAreaIndicator? TechnicalAreaIndicator { get; protected set; }

        /// <summary>
        /// Gets or sets list of <see cref="StrategicAction"/> models.
        /// </summary>
        public virtual List<StrategicAction> StrategicActions { get; protected set; } = null!;

        /// <summary>
        /// Set score.
        /// </summary>
        /// <param name="score"> Score.</param>
        public void SetScore(int score)
        {
            this.Score = score;
        }

        /// <summary>
        /// Set goal.
        /// </summary>
        /// <param name="goal"> Score.</param>
        public void SetGoal(int goal)
        {
            this.Goal = goal;
        }
    }
}