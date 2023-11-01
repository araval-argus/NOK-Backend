// <copyright file="GetCountryDashboardDetailsCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan
{
    using WHO.NOK.BusinessLogic.Helper;
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command for get the plan details.
    /// </summary>
    public class GetCountryDashboardDetailsCommand : Request
    {
        /// <summary>
        /// Gets or sets <see cref="PaginationFilter"/> object for pagination.
        /// </summary>
        public PaginationFilter? PaginationFilter { get; set; }
    }
}