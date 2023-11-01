// <copyright file="DetailedActivityType.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Detailed activity type DB model.
    /// </summary>
    public class DetailedActivityType : ITrackable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailedActivityType"/> class.
        /// </summary>
        public DetailedActivityType()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailedActivityType"/> class.
        /// </summary>
        /// <param name="countryId">Country id.</param>
        /// <param name="activity">Activity.</param>
        public DetailedActivityType(int countryId, string activity)
        {
            this.CountryId = countryId;
            this.Activity = activity;
        }

        /// <summary>
        /// Gets or sets activity type id.
        /// </summary>
        [Key]
        public int ActivityTypeId { get; protected set; }

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int? CountryId { get; protected set; }

        /// <summary>
        /// Gets or sets activity name.
        /// </summary>
        public string? Activity { get; protected set; }

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