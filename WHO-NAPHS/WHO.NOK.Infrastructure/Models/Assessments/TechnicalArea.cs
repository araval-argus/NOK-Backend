// <copyright file="TechnicalArea.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Assessments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Technical area DB Model.
    /// </summary>
    public class TechnicalArea : ITrackable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicalArea"/> class.
        /// </summary>
        public TechnicalArea()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicalArea"/> class.
        /// </summary>
        /// <param name="name">Technical area name.</param>
        /// <param name="countryId">Country id.</param>
        /// <param name="sourceId">Source id.</param>
        /// <param name="isActive">is active.</param>
        /// <param name="isCustomTechnicalArea">Is custom technical area.</param>
        public TechnicalArea(string name, int countryId, int sourceId, bool isActive, bool isCustomTechnicalArea)
        {
            this.Name = name;
            this.CountryId = countryId;
            this.SourceId = sourceId;
            this.IsActive = isActive;
            this.IsCustomTechnicalArea = isCustomTechnicalArea;
        }

        /// <summary>
        /// Gets or sets Technical area id.
        /// </summary>
        [Key]
        public int TechnicalAreaId { get; protected set; }

        /// <summary>
        /// Gets or sets Technical area name.
        /// </summary>
        public string? Name { get; protected set; }

        /// <summary>
        /// Gets or sets Area code.
        /// </summary>
        public string AreaCode { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets or sets area code id.
        /// </summary>
        public string? AreaCodeId { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active or not.
        /// </summary>
        public bool IsActive { get; protected set; }

        /// <summary>
        /// Gets or sets Technical source id.
        /// </summary>
        public int SourceId { get; protected set; }

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int? CountryId { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether is custom technical area.
        /// </summary>
        public bool IsCustomTechnicalArea { get; protected set; }

        /// <inheritdoc/>
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc/>
        public int CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime LastUpdatedAt { get; set; }

        /// <inheritdoc/>
        public int LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Source"/>.
        /// </summary>
        public virtual Source? Source { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="TechnicalAreaIndicator"/> for technical area.
        /// </summary>
        public virtual ICollection<TechnicalAreaIndicator>? TechnicalAreaIndicator { get; set; }
    }
}