// <copyright file="CollaboratingInstitution.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// DB Model for collaborating institutes that is being mapped with country and some common institutes.
    /// </summary>
    public class CollaboratingInstitution : ITrackable
    {
        /// <summary>
        /// Gets or sets institute id.
        /// </summary>
        [Key]
        public int InstituteId { get; protected set; }

        /// <summary>
        /// Gets or sets title of the institute.
        /// </summary>
        public string? Title { get; protected set; }

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int? CountryId { get; protected set; }

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