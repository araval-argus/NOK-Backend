// <copyright file="GetCustomNBWActionsForPlanCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.Excel
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to get custom nbw recommendations for plan.
    /// </summary>
    public class GetCustomNBWActionsForPlanCommand : Request
    {
        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanId { get; set; }
    }
}