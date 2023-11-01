// <copyright file="GetCountryInitialPlanDetailCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Command to get the initial plan details while creating the plan.
    /// </summary>
    public class GetCountryInitialPlanDetailCommand : Request
    {
        /// <summary>
        /// Gets or sets <see cref="PlanType"/> enum.
        /// </summary>
        public PlanType PlanType { get; set; }

        /// <summary>
        /// Gets or sets CountryId.
        /// </summary>
        public int CountryId { get; set; }
    }
}