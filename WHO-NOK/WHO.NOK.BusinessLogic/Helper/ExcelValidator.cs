// <copyright file="ExcelValidator.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Helper
{
    using DocumentFormat.OpenXml.Packaging;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Excel validator class to validate a excel file.
    /// </summary>
    public static class ExcelValidator
    {
        /// <summary>
        /// Validate excel file.
        /// </summary>
        /// <param name="file"> File to validate.</param>
        /// <returns> Returns boolean representing whether excel file is valid or not.</returns>
        public static bool ValidateExcelFile(IFormFile file)
        {
            bool status = false;
            try
            {
                using Stream stream = file.OpenReadStream();
                using SpreadsheetDocument document = SpreadsheetDocument.Open(stream, false);

                // If the document opens without exceptions, it's a valid Excel file.
                status = true;
            }
            catch (Exception)
            {
            }

            return status;
        }
    }
}