// <copyright file="AuditStrategicActions.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Audit
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Audit strategic actions DB Model.
    /// </summary>
    public class AuditStrategicActions : ITrackable
    {
        /// <summary>
        /// Gets or sets strategic action id.
        /// </summary>
        [Key]
        public int AuditStrategicActionId { get; protected set; }

        /// <summary>
        /// Gets or sets audit log status.
        /// </summary>
        public AuditLogRowStatus AuditLogRowStatus { get; set; }

        /// <summary>
        /// Gets or sets strategic action id.
        /// </summary>
        public int StrategicActionId { get; protected set; }

        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        public int PlanIndicatorId { get; protected set; }

        /// <summary>
        /// Gets or sets descriptions.
        /// </summary>
        public string? Objective { get; protected set; }

        /// <summary>
        /// Gets or sets the action information.
        /// </summary>
        public string? Action { get; protected set; }

        /// <summary>
        /// Gets or sets feasibility.
        /// </summary>
        public int Feasibility { get; protected set; }

        /// <summary>
        /// Gets or sets impact.
        /// </summary>
        public int Impact { get; protected set; }

        /// <summary>
        /// Gets or sets priority of this action.
        /// </summary>
        public int Priority { get; protected set; }

        /// <summary>
        /// Gets or sets responsible authority.
        /// </summary>
        public string? ResponsibleAuthority { get; protected set; }

        /// <summary>
        /// Gets or sets estimated cost.
        /// </summary>
        public double? EstimatedCost { get; protected set; }

        /// <summary>
        /// Gets or sets comments.
        /// </summary>
        public string? Comments { get; protected set; }

        /// <summary>
        /// Gets or sets source.
        /// </summary>
        public int? Source { get; protected set; }

        /// <summary>
        /// Gets or sets old score for IHR Benchmark.
        /// </summary>
        public int? Score { get; protected set; }

        /// <summary>
        /// Gets or sets target score for IHR Benchmark.
        /// </summary>
        public int? Goal { get; protected set; }

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