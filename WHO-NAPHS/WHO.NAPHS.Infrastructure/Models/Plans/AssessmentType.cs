// <copyright file="AssessmentType.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Assessment type DB Model.
    /// </summary>
    public class AssessmentType
    {
        /// <summary>
        /// Gets or sets assessment type.
        /// </summary>
        [Key]
        public int AssessmentTypeId { get; protected set; }

        /// <summary>
        /// Gets or sets assessment type.
        /// </summary>
        public string? Type { get; protected set; }

        /// <summary>
        /// Gets or sets list of <see cref="CountryPlan"/> for this assessment type.
        /// </summary>
        public virtual List<CountryPlan> CountryPlans { get; set; } = new List<CountryPlan>();
    }
}