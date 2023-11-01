// <copyright file="CountryDropdownViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Country
{
    /// <summary>
    /// View Model for Country dropdown list.
    /// </summary>
    public class CountryDropdownViewModel
    {
        /// <summary>
        /// Gets or sets Country Id.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets Country name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}