// <copyright file="SPARPointOfEntryViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Assessments
{
    /// <summary>
    /// View model for point of entries.
    /// </summary>
    public class SPARPointOfEntryViewModel
    {
        /// <summary>
        /// Gets or sets airport.
        /// </summary>
        public int Airport { get; set; }

        /// <summary>
        /// Gets or sets port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets ground.
        /// </summary>
        public int Ground { get; set; }

        /// <summary>
        /// Gets or sets AuthorizePortsToIssueSanitationCert.
        /// </summary>
        public string AuthorizePortsToIssueSanitationCert { get; set; } = string.Empty;
    }
}