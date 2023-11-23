// <copyright file="GetAssistanceDataCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan
{
    using WHO.NOK.BusinessLogic.Helper;
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to get actions that requires financial/technical assistance.
    /// </summary>
    public class GetAssistanceDataCommand : Request
    {
        /// <summary>
        /// Gets or sets <see cref="PaginationFilter"/> for pagination.
        /// </summary>
        public PaginationFilter? PaginationFilter { get; set; } = new ();

        /// <summary>
        /// Gets or sets a value indicating whether financial assistance is required or not.
        /// </summary>
        public bool NeedFinancialAssistance { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether filtration data is required or not.
        /// </summary>
        public bool NeedFilterData { get; set; } = false;
    }
}