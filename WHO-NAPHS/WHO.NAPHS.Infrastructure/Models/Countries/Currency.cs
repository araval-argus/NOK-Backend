// <copyright file="Currency.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Countries
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Currency Database Model.
    /// </summary>
    public class Currency : ITrackable
    {
        /// <summary>
        /// Gets or sets Id of Currency.
        /// </summary>
        [Key]
        public int CurrencyId { get; protected set; }

        /// <summary>
        /// Gets or sets Currency Code.
        /// </summary>
        public string Code { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets or sets Currency Sign.
        /// </summary>
        public string? Sign { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether the currency is default or not.
        /// </summary>
        public bool IsDefault { get; protected set; } = false;

        /// <summary>
        /// Gets or sets Conversion Factor.
        /// </summary>
        public decimal? ConversionFactor { get; protected set; }

        /// <summary>
        /// Gets or sets Description of currency.
        /// </summary>
        public string? Description { get; protected set; }

        /// <summary>
        /// Gets or sets created date.
        /// </summary>
        public DateTime CreatedAt { get;  set; }

        /// <summary>
        /// Gets or sets created by.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets last updated date.
        /// </summary>
        public DateTime LastUpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets last updated by.
        /// </summary>
        public int LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="Country"/> related to this currency.
        /// </summary>
        public virtual ICollection<Country>? Countries { get; set; }
    }
}