// <copyright file="ImportExcelViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.Excel
{
    using Microsoft.AspNetCore.Http;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Import excel file view model.
    /// </summary>
    public class ImportExcelViewModel
    {
        /// <summary>
        /// Gets or sets <see cref="ExcelTypes"/> enum.
        /// </summary>
        public ExcelTypes ExcelType { get; set; }

        /// <summary>
        /// Gets or sets excel file.
        /// </summary>
        public IFormFile? ExcelFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether first row is column name.
        /// </summary>
        public bool IsFirstRowColumnName { get; set; }

        /// <summary>
        /// Gets or sets sheet name.
        /// </summary>
        public string? SheetName { get; set; }

        /// <summary>
        /// Gets or sets User id.
        /// </summary>
        public int UserId { get; set; }
    }
}