// <copyright file="Configuration.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Configurations
{
    /// <summary>
    /// Configuration Model.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Gets or sets Key of the configuration parameter.
        /// </summary>
        public string Key { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets or sets Value of the configuration parameter.
        /// </summary>
        public string Value { get; protected set; } = string.Empty;
    }
}