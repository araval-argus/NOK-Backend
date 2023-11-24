// <copyright file="PlanType.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Plan type DB Model.
    /// </summary>
    public class PlanType
    {
        /// <summary>
        /// Gets or sets plan type id.
        /// </summary>
        [Key]
        public int PlanTypeId { get; protected set; }

        /// <summary>
        /// Gets or sets type.
        /// </summary>
        public string? Type { get; protected set; }

        /// <summary>
        /// Gets or sets list of <see cref="CountryPlan"/> for this plan type.
        /// </summary>
        public virtual List<CountryPlan> CountryPlans { get; set; } = new List<CountryPlan>();
    }
}