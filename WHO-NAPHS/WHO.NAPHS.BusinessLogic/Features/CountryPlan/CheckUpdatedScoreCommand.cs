// <copyright file="CheckUpdatedScoreCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Check for updated code updated command.
    /// </summary>
    public class CheckUpdatedScoreCommand : Request
    {
        /// <summary>
        /// Gets or sets plan id.
        /// </summary>
        public int PlanId { get; set; }
    }
}