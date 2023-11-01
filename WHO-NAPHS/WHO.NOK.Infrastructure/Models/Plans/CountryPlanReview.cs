// <copyright file="CountryPlanReview.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Country Plan review DB model.
    /// </summary>
    public class CountryPlanReview : ITrackable, IDeletable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryPlanReview"/> class.
        /// </summary>
        /// <param name="countryPlanId"> Country plan id.</param>
        /// <param name="reviewDate"> Review date.</param>
        public CountryPlanReview(int countryPlanId, DateTime reviewDate)
        {
            this.CountryPlanId = countryPlanId;
            this.ReviewDate = reviewDate;
            this.ReviewStatus = ReviewStatus.Due;
            this.IsDeleted = false;
        }

        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        [Key]
        public int CountryPlanReviewId { get; set; }

        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanId { get; set; }

        /// <summary>
        /// Gets or sets review date.
        /// </summary>
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// Gets or sets the status of a review.
        /// </summary>
        public ReviewStatus ReviewStatus { get; set; }

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
        /// Gets or sets <see cref="CountryPlan"/> object.
        /// </summary>
        public virtual CountryPlan CountryPlan { get; set; } = null!;
    }
}