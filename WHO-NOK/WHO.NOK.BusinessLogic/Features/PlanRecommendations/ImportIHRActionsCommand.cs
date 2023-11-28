// <copyright file="ImportIHRActionsCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.PlanRecommendations
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to import excel data from the front end by parsing the excel file.
    /// </summary>
    public class ImportIHRActionsCommand : Request
    {
        /// <summary>
        /// Gets or Sets Country Plan id.
        /// </summary>
        public int CountryPlanId { get; set; }

        /// <summary>
        /// Gets or sets indicator code id.
        /// </summary>
        public string? IndicatorCodeId { get; set; }

        /// <summary>
        /// Gets or Sets indicator code.
        /// </summary>
        public string? IndicatorCode { get; set; }
    }
}