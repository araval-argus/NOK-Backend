// <copyright file="DownloadPlanHistory.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Plan indicator DB model.
    /// </summary>
    public class DownloadPlanHistory : ITrackable
    {
        /// <summary>
        /// Gets or sets download plan data id.
        /// </summary>
        [Key]
        public int DownloadPlanDataId { get; protected set; }

        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanId { get; protected set; }

        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public int? UserId { get; protected set; }

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