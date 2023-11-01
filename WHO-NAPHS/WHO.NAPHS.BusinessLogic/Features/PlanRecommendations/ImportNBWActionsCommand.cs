// <copyright file="ImportNBWActionsCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.PlanRecommendations
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Command to import excel data from the front end by parsing the excel file.
    /// </summary>
    public class ImportNBWActionsCommand : Request
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