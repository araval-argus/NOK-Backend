// <copyright file="AppSettingsConfigurationSource.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.API.Helper.Configuration
{
    using WHO.NAPHS.Infrastructure.Models.DatabaseContext;

    /// <summary>
    /// Configuration source class for AppSettings.
    /// </summary>
    public class AppSettingsConfigurationSource : IConfigurationSource
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsConfigurationSource"/> class.
        /// </summary>
        /// <param name="context"><see cref="ApplicationDbContext"/> context.</param>
        public AppSettingsConfigurationSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Builds the configuration source.
        /// </summary>
        /// <param name="builder"><see cref="IConfigurationBuilder"/>.</param>
        /// <returns>Returns instance of <see cref="AppSettingsConfigurationProvider"/> class.</returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AppSettingsConfigurationProvider(this.context);
        }
    }
}