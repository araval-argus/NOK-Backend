// <copyright file="CommonTechnicalArea.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Assessments
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Common technical area DB model.
    /// </summary>
    public class CommonTechnicalArea
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        [Key]
        public int CommonTechnicalAreaId { get; protected set; }

        /// <summary>
        /// Gets or sets display name.
        /// </summary>
        public string DisplayName { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets or sets Indicator Id.
        /// </summary>
        public int IndicatorId { get; protected set; }

        /// <summary>
        /// Gets or sets order by.
        /// </summary>
        public int OrderBy { get; protected set; }
    }
}