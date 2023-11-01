// <copyright file="CommonIndicatorsMapping.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress constructor error.
#pragma warning disable CS8618

namespace WHO.NAPHS.Infrastructure.Models.Assessments
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Common Indicator mapping DB model.
    /// </summary>
    public class CommonIndicatorsMapping
    {
        /// <summary>
        /// Gets or sets CommonIndicatorsMappingId.
        /// </summary>
        [Key]
        public int CommonIndicatorsMappingId { get; protected set; }

        /// <summary>
        /// Gets or Sets Common Indicator id.
        /// </summary>
        public int CommonIndicatorId { get; protected set; }

        /// <summary>
        /// Gets or Sets Indicator code.
        /// </summary>
        public string? IndicatorCode { get; protected set; }

        /// <summary>
        /// Gets or Sets Indicator id.
        /// </summary>
        public string IndicatorId { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="IndicatorType"/> enum.
        /// </summary>
        public IndicatorType Type { get; protected set; }
    }
}