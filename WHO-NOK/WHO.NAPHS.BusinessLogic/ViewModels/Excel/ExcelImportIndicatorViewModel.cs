// <copyright file="ExcelImportIndicatorViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Excel
{
    /// <summary>
    /// Excel import indicator view model.
    /// </summary>
    public class ExcelImportIndicatorViewModel
    {
        /// <summary>
        /// Gets or sets indicator name.
        /// </summary>
        public string IndicatorName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets indicator id.
        /// </summary>
        public string IndicatorId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets indicator code.
        /// </summary>
        public string? IndicatorCode { get; set; }
    }
}