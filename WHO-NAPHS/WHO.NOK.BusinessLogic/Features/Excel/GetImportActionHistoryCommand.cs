// <copyright file="GetImportActionHistoryCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.Excel
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Command to import excel data from the front end by parsing the excel file.
    /// </summary>
    public class GetImportActionHistoryCommand : Request
    {
        /// <summary>
        /// Gets or sets <see cref="ExcelTypes"/> enum.
        /// </summary>
        public ExcelTypes ExcelType { get; set; }

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int? CountryId { get; set; }
    }
}