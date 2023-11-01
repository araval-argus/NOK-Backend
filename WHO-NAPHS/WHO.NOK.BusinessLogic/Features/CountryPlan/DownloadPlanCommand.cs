// <copyright file="DownloadPlanCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Request Command to download a plan.
    /// </summary>
    public class DownloadPlanCommand : Request
    {
        /// <summary>
        /// Gets or sets the id of the plan to be downloaded.
        /// </summary>
        public int CountryPlanId { get; set; }
    }
}