// <copyright file="StrategicActionPriority.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Strategic priority DB model.
    /// </summary>
    public class StrategicActionPriority
    {
        /// <summary>
        /// Gets or sets priority Id.
        /// </summary>
        [Key]
        public int PriorityId { get; set; }

        /// <summary>
        /// Gets or sets priority.
        /// </summary>
        public string? Priority { get; set; }

        /// <summary>
        /// Gets or sets <see cref="StrategicAction"/> for this strategic action priority.
        /// </summary>
        public virtual List<StrategicAction> StrategicActions { get; set; } = null!;
    }
}