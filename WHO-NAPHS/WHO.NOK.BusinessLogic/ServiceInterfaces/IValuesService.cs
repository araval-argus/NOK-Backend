// <copyright file="IValuesService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ServiceInterfaces
{
    using WHO.NAPHS.BusinessLogic.ViewModels;
    using WHO.NAPHS.BusinessLogic.ViewModels.Country;
    using WHO.NAPHS.BusinessLogic.ViewModels.Language;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Interface definition for the values.
    /// </summary>
    public interface IValuesService : IBaseService
    {
        /// <summary>
        /// Get all the countries.
        /// </summary>
        /// <returns>Returns list of <see cref="CountryViewModel"/> model.</returns>
        Task<List<CountryViewModel>> GetAllCountriesAsync();

        /// <summary>
        /// Gets the list of the countries based upon the user role.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>Returns list of <see cref="CountryViewModel"/> model.</returns>
        Task<List<CountryViewModel>> GetCountriesFromRoleAsync(int userId);

        /// <summary>
        /// Get all the languages.
        /// </summary>
        /// <returns>Returns list of <see cref="LanguageViewModel"/> model.</returns>
        Task<List<LanguageViewModel>> GetLanguagesAsync();

        /// <summary>
        /// Get all the roles.
        /// </summary>
        /// <returns>Returns list of <see cref="RoleViewModel"/> model.</returns>
        Task<List<RoleViewModel>> GetRolesAsync();
    }
}
