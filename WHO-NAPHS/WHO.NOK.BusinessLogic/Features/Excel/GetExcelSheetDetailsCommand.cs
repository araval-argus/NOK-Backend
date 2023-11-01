// <copyright file="GetExcelSheetDetailsCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.Excel
{
    using Microsoft.AspNetCore.Http;
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to get excel file sheets details to parse a particular sheet.
    /// </summary>
    public class GetExcelSheetDetailsCommand : Request
    {
        /// <summary>
        /// Gets or sets uploaded file.
        /// </summary>
        public IFormFile? File { get; set; } = null;
    }
}