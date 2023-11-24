// <copyright file="CachedValuesService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.CacheImplementation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;
    using WHO.NOK.BusinessLogic.CacheInterfaces;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.BusinessLogic.ViewModels;
    using WHO.NOK.BusinessLogic.ViewModels.Country;
    using WHO.NOK.BusinessLogic.ViewModels.Language;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Cache implementation for values service.
    /// </summary>
    public class CachedValuesService : ICacheService, IValuesService
    {
        private const string CountryList = "CountryList";
        private const string LanguageList = "LanguageList";
        private const string RolesList = "RolesList";

        private readonly IValuesService valuesService;
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedValuesService"/> class.
        /// </summary>
        /// <param name="valuesService"><see cref="IValuesService"/> service Implementations.</param>
        /// <param name="memoryCache"><see cref="IMemoryCache"/> interface.</param>
        public CachedValuesService(IValuesService valuesService, IMemoryCache memoryCache)
        {
            this.valuesService = valuesService;
            this.memoryCache = memoryCache;
        }

        /// <inheritdoc/>
        public void ClearCache()
        {
            this.memoryCache.Remove(CountryList);
            this.memoryCache.Remove(LanguageList);
            this.memoryCache.Remove(RolesList);
        }

        /// <inheritdoc/>
        public async Task<List<CountryViewModel>> GetAllCountriesAsync()
        {
            if (this.memoryCache.TryGetValue(CountryList, out List<CountryViewModel> result))
            {
                return result;
            }

            var options = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(10))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(300));

            result = await this.valuesService.GetAllCountriesAsync();

            this.memoryCache.Set(CountryList, result, options);

            return result;
        }

        /// <inheritdoc/>
        public Task<List<CountryViewModel>> GetCountriesFromRoleAsync(int userId)
        {
            return this.valuesService.GetCountriesFromRoleAsync(userId);
        }

        /// <inheritdoc/>
        public async Task<List<LanguageViewModel>> GetLanguagesAsync()
        {
            if (this.memoryCache.TryGetValue(LanguageList, out List<LanguageViewModel> result))
            {
                return result;
            }

            var options = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(10))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(300));

            result = await this.valuesService.GetLanguagesAsync();

            this.memoryCache.Set(LanguageList, result, options);

            return result;
        }

        /// <inheritdoc/>
        public async Task<List<RoleViewModel>> GetRolesAsync()
        {
            if (this.memoryCache.TryGetValue(RolesList, out List<RoleViewModel> result))
            {
                return result;
            }

            var options = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(10))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(300));

            result = await this.valuesService.GetRolesAsync();

            this.memoryCache.Set(RolesList, result, options);

            return result;
        }
    }
}