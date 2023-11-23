// <copyright file="ActivatePlanCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Command to activate plan.
    /// </summary>
    public class ActivatePlanCommand : Request
    {
        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanId { get; set; }

        /// <summary>
        /// Gets or sets <see cref="CountryPlanFrequency"/> enum.
        /// </summary>
        public CountryPlanFrequency Frequency { get; set; }

        /// <summary>
        /// Gets or sets next review date.
        /// </summary>
        public DateTime NextReviewDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether plan is visible to other countries.
        /// </summary>
        public bool IsPlanVisibleToOther { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether plan is officially approved.
        /// </summary>
        public bool IsPlanOfficialApproved { get; set; }
    }
}