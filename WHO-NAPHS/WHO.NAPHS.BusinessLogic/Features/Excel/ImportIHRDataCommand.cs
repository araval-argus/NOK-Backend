// <copyright file="ImportIHRDataCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.Excel
{
    using WHO.NAPHS.BusinessLogic.ViewModels.Excel;
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to import excel data from the front end by parsing the excel file.
    /// </summary>
    public class ImportIHRDataCommand : Request
    {
        /// <summary>
        /// Gets or sets list of <see cref="ExcelImportIHRRecommendationsViewModel"/> models.
        /// </summary>
        public List<IHRRecommendationsViewModel> IHRRecommendations { get; set; } = new ();

        /// <summary>
        /// Gets or sets planning tool id.
        /// </summary>
        public int PlanningToolId { get; set; }

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        // public int CountryId { get; set; }
    }
}