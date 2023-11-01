// <copyright file="UpdateDetailedActivityCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.DetailedActivity
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command for update detailed activity.
    /// </summary>
    public class UpdateDetailedActivityCommand : Request
    {
        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanId { get; set; }

        /// <summary>
        /// Gets or sets detailed activity id.
        /// </summary>
        public int DetailedActivityId { get; set; }
    }
}