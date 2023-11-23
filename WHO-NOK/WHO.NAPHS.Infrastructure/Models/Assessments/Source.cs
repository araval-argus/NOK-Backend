// <copyright file="Source.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Assessments
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Source DB Model.
    /// </summary>
    public class Source
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Source"/> class.
        /// </summary>
        public Source()
        {
        }

        /// <summary>
        /// Gets or sets source id.
        /// </summary>
        [Key]
        public int SourceId { get; protected set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string? Name { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="SourceType"/> enum.
        /// </summary>
        public SourceType SourceType { get; protected set; }

        /// <summary>
        /// Gets or sets list of <see cref="TechnicalArea"/> with current source.
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<TechnicalArea>? TechnicalAreas { get; set; }
    }
}