// <copyright file="CountryViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Country;

/// <summary>
/// View model for country.
/// </summary>
public class CountryViewModel
{
    /// <summary>
    /// Gets or sets or Id of country.
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// Gets or sets country name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets ISO code of country.
    /// </summary>
    public string? ISOCode { get; set; }

    /// <summary>
    /// Gets or sets ISO 3 code of the country.
    /// </summary>
    public string? ISO3Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets latitude of the country.
    /// </summary>
    public decimal? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude of the country.
    /// </summary>
    public decimal? Longitude { get; set; }

    /// <summary>
    /// Gets or sets the region that the country belongs to.
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// Gets or sets currency id.
    /// </summary>
    public int? CurrencyId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="CurrencyViewModel"/> model for country.
    /// </summary>
    public CurrencyViewModel? Currency { get; set; }
}
