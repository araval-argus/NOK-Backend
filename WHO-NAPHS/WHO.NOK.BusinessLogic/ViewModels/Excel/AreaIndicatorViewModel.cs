// <copyright file="AreaIndicatorViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Excel
{
    /// <summary>
    /// Area technical indicators view model.
    /// </summary>
    public class AreaIndicatorViewModel
    {
        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Gets or sets indicator id.
        /// </summary>
        public string IndicatorId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Parent index.
        /// </summary>
        public int ParentIndex { get; set; }

        /// <summary>
        /// Gets or sets indicator code.
        /// </summary>
        public string? IndicatorCode { get; set; }
    }
}