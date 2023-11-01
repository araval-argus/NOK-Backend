// <copyright file="GetSharedPlansCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan
{
    using WHO.NAPHS.BusinessLogic.Helper;

    /// <summary>
    /// Get shared plans.
    /// </summary>
    public class GetSharedPlansCommand
    {
        /// <summary>
        /// Gets or sets Filter for pagination.
        /// </summary>
        public PaginationFilter? PaginationFilter { get; set; }
    }
}