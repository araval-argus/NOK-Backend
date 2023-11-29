// <copyright file="UploadCustomNBWDataCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.Excel
{
    using WHO.NOK.BusinessLogic.ViewModels.Excel;
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to import excel data from the front end by parsing the excel file.
    /// </summary>
    public class UploadCustomNBWDataCommand : Request
    {
        /// <summary>
        /// Gets or sets list of <see cref="ExcelImportNBWRecommendationsViewModel"/> models.
        /// </summary>
        public List<NBWRecommendationsViewModel> NBWRecommendations { get; set; } = new ();

        /// <summary>
        /// Gets or sets planning tool id.
        /// </summary>
        public int PlanningToolId { get; set; }

        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanId { get; set; }
    }
}