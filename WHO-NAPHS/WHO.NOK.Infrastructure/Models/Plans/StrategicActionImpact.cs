// <copyright file="StrategicActionImpact.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Strategic impact DB model.
    /// </summary>
    public class StrategicActionImpact
    {
        /// <summary>
        /// Gets or sets impact id.
        /// </summary>
        [Key]
        public int ImpactId { get; protected set; }

        /// <summary>
        /// Gets or sets impact.
        /// </summary>
        public string? Impact { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="StrategicAction"/> for this strategic action impact.
        /// </summary>
        public virtual List<StrategicAction> StrategicActions { get; set; } = null!;
    }
}