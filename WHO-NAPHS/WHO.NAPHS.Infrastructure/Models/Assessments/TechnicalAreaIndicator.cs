// <copyright file="TechnicalAreaIndicator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Assessments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Technical Indicator DB Model.
    /// </summary>
    public class TechnicalAreaIndicator : ITrackable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicalAreaIndicator"/> class.
        /// </summary>
        public TechnicalAreaIndicator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicalAreaIndicator"/> class.
        /// </summary>
        /// <param name="technicalAreaId">Technical area id.</param>
        /// <param name="name">Technical area indicator name.</param>
        public TechnicalAreaIndicator(int technicalAreaId, string name)
        {
            this.TechnicalAreaId = technicalAreaId;
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets Indicator Id.
        /// </summary>
        [Key]
        public int TechnicalAreaIndicatorId { get; protected set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string? Name { get; protected set; }

        /// <summary>
        /// Gets or sets Technical area.
        /// </summary>
        public int? TechnicalAreaId { get; protected set; }

        /// <summary>
        /// Gets or sets Indicator code.
        /// </summary>
        public string? IndicatorCode { get; protected set; }

        /// <summary>
        /// Gets or sets indicator Id.
        /// </summary>
        public string? IndicatorCodeId { get; protected set; }

        /// <inheritdoc/>
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc/>
        public int CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime LastUpdatedAt { get; set; }

        /// <inheritdoc/>
        public int LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets <see cref="TechnicalArea"/> to which indicator belongs to.
        /// </summary>
        public virtual TechnicalArea? TechnicalArea { get; set; }
    }
}