// <copyright file="PlanStage.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Plan stage DB model.
    /// </summary>
    public class PlanStage
    {
        /// <summary>
        /// Gets or sets plan stage id.
        /// </summary>
        [Key]
        public int PlanStageId { get; protected set; }

        /// <summary>
        /// Gets or sets stage.
        /// </summary>
        public string? Stage { get; protected set; }

        /// <summary>
        /// Gets or sets list of <see cref="CountryPlan"/> for this plan stage.
        /// </summary>
        public virtual List<CountryPlan> CountryPlans { get; set; } = new List<CountryPlan>();
    }
}