// <copyright file="ImportExcelDataCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.Excel
{
    using WHO.NOK.BusinessLogic.ViewModels.Excel;
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Command to import excel data from the front end by parsing the excel file.
    /// </summary>
    public class ImportExcelDataCommand : Request
    {
        /// <summary>
        /// Gets or sets list of <see cref="ExcelImportTechnicalAreaViewModel"/> models.
        /// </summary>
        public List<ExcelImportTechnicalAreaViewModel> TechnicalAreas { get; set; } = new ();

        /// <summary>
        /// Gets or sets <see cref="ExcelTypes"/> enum.
        /// </summary>
        public ExcelTypes ExcelType { get; set; }

        /// <summary>
        /// Gets or sets planning tool id.
        /// </summary>
        public int PlanningToolId { get; set; }
    }
}