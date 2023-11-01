// <copyright file="GetJEERecommendationsCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Get JEE recommendation command.
    /// </summary>
    public class GetJEERecommendationsCommand : Request
    {
        /// <summary>
        /// Gets or sets plan id.
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int CountryId { get; set; }
    }
}