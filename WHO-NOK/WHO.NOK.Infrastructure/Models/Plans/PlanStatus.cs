// <copyright file="PlanStatus.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Plan status DB model.
    /// </summary>
    public class PlanStatus
    {
        /// <summary>
        /// Gets or sets plan status id.
        /// </summary>
        [Key]
        public int PlanStatusId { get; protected set; }

        /// <summary>
        /// Gets or sets status.
        /// </summary>
        public string? Status { get; protected set; }

        /// <summary>
        /// Gets or sets list of <see cref="CountryPlan"/> for this plan status.
        /// </summary>
        public virtual List<CountryPlan> CountryPlans { get; set; } = new List<CountryPlan>();
    }
}