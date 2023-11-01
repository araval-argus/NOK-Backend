// <copyright file="GetDetailedActivityCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.DetailedActivity
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to get detailed activity by country id.
    /// </summary>
    public class GetDetailedActivityCommand : Request
    {
        /// <summary>
        /// Gets or sets Detailed activity id.
        /// </summary>
        public int CountryId { get; set; }
    }
}