// <copyright file="AppSettingsConfigurationProvider.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.API.Helper.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using WHO.NAPHS.Core.Constant;
    using WHO.NAPHS.Infrastructure.Models.DatabaseContext;

    /// <summary>
    /// Provides a configuration from the database.
    /// </summary>
    public class AppSettingsConfigurationProvider : ConfigurationProvider
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsConfigurationProvider"/> class.
        /// </summary>
        /// <param name="context"><see cref="ApplicationDbContext"/> context.</param>
        public AppSettingsConfigurationProvider(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Loads (or reloads) the data for this provider.
        /// </summary>
        public override void Load()
        {
            this.Data["AppSettings"] = this.LoadAppSettingsFromDataFromDatabase();
        }

        /// <summary>
        /// Loads the application settings from the database.
        /// </summary>
        /// <returns>json string of instance of <see cref="ApplicationSettings"/> class.</returns>
        private string LoadAppSettingsFromDataFromDatabase()
        {
            ApplicationSettings appSettings = new ();

            var table = this.context.Configurations.AsNoTracking().ToList();

            try
            {
                foreach (var keyValuePair in table)
                {
                    var property = appSettings.GetType().GetProperty(keyValuePair.Key);
                    var propertyType = property != null ? property.PropertyType : throw new Exception("Cannot find the property in ApplicationSettings class");

                    var convertedValue = Convert.ChangeType(keyValuePair.Value, propertyType);
                    property.SetValue(appSettings, convertedValue);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return appSettings.ToJson();
        }
    }
}