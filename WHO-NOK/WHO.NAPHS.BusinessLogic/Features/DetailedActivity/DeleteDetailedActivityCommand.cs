// <copyright file="DeleteDetailedActivityCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.DetailedActivity
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command for create detailed activity.
    /// </summary>
    public class DeleteDetailedActivityCommand : Request
    {
        /// <summary>
        /// Gets or sets Detailed activity id.
        /// </summary>
        public int DetailedActivityId { get; set; }

        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanId { get; set; }
    }
}