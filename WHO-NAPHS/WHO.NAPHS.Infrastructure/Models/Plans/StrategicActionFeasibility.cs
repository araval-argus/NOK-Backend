// <copyright file="StrategicActionFeasibility.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Strategic action feasibility DB Model.
    /// </summary>
    public class StrategicActionFeasibility
    {
        /// <summary>
        /// Gets or sets feasibility id.
        /// </summary>
        [Key]
        public int FeasibilityId { get; protected set; }

        /// <summary>
        /// Gets or sets feasibility.
        /// </summary>
        public string? Feasibility { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="StrategicAction"/> for this strategic action feasibility.
        /// </summary>
        public virtual List<StrategicAction> StrategicActions { get; set; } = null!;
    }
}