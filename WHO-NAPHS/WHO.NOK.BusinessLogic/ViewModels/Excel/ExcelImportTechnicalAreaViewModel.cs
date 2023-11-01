// <copyright file="ExcelImportTechnicalAreaViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.Excel
{
    /// <summary>
    /// Excel import view model.
    /// </summary>
    public class ExcelImportTechnicalAreaViewModel
    {
        /// <summary>
        /// Gets or sets technical area.
        /// </summary>
        public string TechnicalArea { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Area code.
        /// </summary>
        public string AreaCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets Area code id.
        /// </summary>
        public string AreaCodeId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets list of <see cref="ExcelImportIndicatorViewModel"/> representing technical area indicators.
        /// </summary>
        public List<ExcelImportIndicatorViewModel> TechnicalAreaIndicators { get; set; } = new ();
    }
}