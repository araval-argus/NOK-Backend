// <copyright file="CurrencyViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Country;

/// <summary>
/// View model for Currency.
/// </summary>
public class CurrencyViewModel
{
    /// <summary>
    /// Gets or sets Id of Currency.
    /// </summary>
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
}