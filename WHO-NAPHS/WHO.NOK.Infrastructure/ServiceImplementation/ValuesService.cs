// <copyright file="ValuesService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.ServiceImplementation
{
    using AutoMapper;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using WHO.NAPHS.BusinessLogic.ServiceInterfaces;
    using WHO.NAPHS.BusinessLogic.ViewModels;
    using WHO.NAPHS.BusinessLogic.ViewModels.Country;
    using WHO.NAPHS.BusinessLogic.ViewModels.Language;
    using WHO.NAPHS.Core.Common;
    using WHO.NAPHS.Infrastructure.Helper;
    using WHO.NAPHS.Infrastructure.Models.DatabaseContext;

    /// <summary>
    /// Implements the <see cref="IValuesService"/> interface.
    /// </summary>
    public class ValuesService : IValuesService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesService"/> class.
        /// </summary>
        /// <param name="context"> DB Context.</param>
        /// <param name="mapper"> Mapper.</param>
        public ValuesService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<CountryViewModel>> GetAllCountriesAsync()
        {
            List<Models.Countries.Country> countries = await this.context.Countries.Include(x => x.Currency).AsNoTracking().OrderBy(x => x.Name).ToListAsync();
            return this.mapper.Map<List<CountryViewModel>>(countries);
        }

        /// <inheritdoc />
        public async Task<List<CountryViewModel>> GetCountriesFromRoleAsync(int userId)
        {
            List<Models.Countries.Country> countries = new ();
            List<SqlParameter> parameters = new ()
            {
                new SqlParameter()
                {
                    ParameterName = "@userId",
                    Value = userId,
                },
            };
            await this.context.ExecuteProcedureAsync(
                System.Data.CommandType.StoredProcedure,
                "sp_GetCountriesBasedUponUserRole",
                (ds) =>
                {
                    countries = ds.Tables[0].ConvertToList<Models.Countries.Country>();
                },
                parameters,
                1800);
            return this.mapper.Map<List<CountryViewModel>>(countries);
        }

        /// <inheritdoc />
        public async Task<List<LanguageViewModel>> GetLanguagesAsync()
        {
            return this.mapper.Map<List<LanguageViewModel>>(await this.context.Languages.AsNoTracking().OrderBy(x => x.Name).ToListAsync());
        }

        /// <inheritdoc />
        public async Task<List<RoleViewModel>> GetRolesAsync()
        {
            return this.mapper.Map<List<RoleViewModel>>(await this.context.Roles.AsNoTracking().OrderBy(x => x.Name).ToListAsync());
        }
    }
}
