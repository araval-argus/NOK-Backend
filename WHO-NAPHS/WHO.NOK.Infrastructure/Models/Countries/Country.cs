// <copyright file="Country.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Countries
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NOK.Infrastructure.Models.Users;

    /// <summary>
    /// Country Database Model.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Gets or sets Id of country.
        /// </summary>
        [Key]
        public int CountryId { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets country name.
        /// </summary>
        public string Name { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets ISO code of country.
        /// </summary>
        public string ISOCode { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets ISO 3 code of the country.
        /// </summary>
        public string ISO3Code { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets or sets latitude of the country.
        /// </summary>
        public decimal? Latitude { get; protected set; }

        /// <summary>
        /// Gets or sets the longitude of the country.
        /// </summary>
        public decimal? Longitude { get; protected set; }

        /// <summary>
        /// Gets or sets the region that the country belongs to.
        /// </summary>
        public string? Region { get; protected set; }

        /// <summary>
        /// Gets or sets the Currency id.
        /// </summary>
        public int? CurrencyId { get; protected set; }

        /// <summary>
        /// Gets or sets list of <see cref="User"/> belonging to the country.
        /// </summary>
        public virtual ICollection<User>? Users { get; protected set; }

        /// <summary>
        /// Gets or sets the <see cref="Currency"/> associated with the country.
        /// </summary>
        public virtual Currency? Currency { get; protected set; }
    }
}
