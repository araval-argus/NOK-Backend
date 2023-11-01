// <copyright file="ITrackable.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Core.Common
{
    /// <summary>
    ///  Interface to track save details.
    /// </summary>
    public interface ITrackable
    {
        /// <summary>
        /// Gets or sets createdAt TimeStamp of the Entity.
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets profileId of the User who created the Entity.
        /// </summary>
        int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets lastUpdated TimeStamp of the Entity.
        /// </summary>
        DateTime LastUpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets profileId of the user who last updated the Entity.
        /// </summary>
        int LastUpdatedBy { get; set; }
    }
}