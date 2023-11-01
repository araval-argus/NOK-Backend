// <copyright file="AuditCountryPlanIndicators.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Audit
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Audit detailed activities DB model.
    /// </summary>
    public class AuditCountryPlanIndicators : ITrackable
    {
        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        [Key]
        public int AuditPlanIndicatorId { get; protected set; }

        /// <summary>
        /// Gets or sets audit log status.
        /// </summary>
        public AuditLogRowStatus AuditLogRowStatus { get; set; }

        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        public int PlanIndicatorId { get; protected set; }

        /// <summary>
        /// Gets or sets indicator id.
        /// </summary>
        public int? TechnicalAreaIndicatorId { get; protected set; }

        /// <summary>
        /// Gets or sets Plan id.
        /// </summary>
        public int CountryPlanId { get; protected set; }

        /// <summary>
        /// Gets or sets score.
        /// </summary>
        public int Score { get; protected set; }

        /// <summary>
        /// Gets or sets goal.
        /// </summary>
        public int Goal { get; protected set; }

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