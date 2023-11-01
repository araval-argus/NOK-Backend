// <copyright file="GetDataForDashboardChartCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Get data for dashboard chart command.
    /// </summary>
    public class GetDataForDashboardChartCommand : Request
    {
        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int CountryId { get; set; }
    }
}