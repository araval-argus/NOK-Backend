// <copyright file="ImportExcelFileCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.Excel
{
    using Microsoft.AspNetCore.Http;
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Command to get excel file sheets details to parse a particular sheet.
    /// </summary>
    public class ImportExcelFileCommand : Request
    {
        /// <summary>
        /// Gets or sets uploaded file.
        /// </summary>
        public IFormFile? File { get; set; } = null;

        /// <summary>
        /// Gets or sets <see cref="ExcelTypes"/> enum.
        /// </summary>
        public ExcelTypes ExcelType { get; set; }

        /// <summary>
        /// Gets or sets sheet name.
        /// </summary>
        public string? SheetName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether first row a column name.
        /// </summary>
        public bool IsFirstRowColumnName { get; set; }
    }
}