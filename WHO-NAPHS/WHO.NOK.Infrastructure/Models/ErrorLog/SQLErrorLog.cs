// <copyright file="SQLErrorLog.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.ErrorLog
{
    /// <summary>
    /// SQL Error logs DB Model.
    /// </summary>
    public class SQLErrorLog
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets ErrorSeverity.
        /// </summary>
        public string? ErrorSeverity { get; set; }

        /// <summary>
        /// Gets or sets ErrorState.
        /// </summary>
        public string? ErrorState { get; set; }

        /// <summary>
        /// Gets or sets ErrorProcedure.
        /// </summary>
        public string? ErrorProcedure { get; set; }

        /// <summary>
        /// Gets or sets ErrorLine.
        /// </summary>
        public string? ErrorLine { get; set; }

        /// <summary>
        /// Gets or sets ErrorMessage.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets ErrorDate.
        /// </summary>
        public DateTime ErrorDate { get; set; }

        /// <summary>
        /// Gets or sets ModuleName.
        /// </summary>
        public string? ModuleName { get; set; }
    }
}