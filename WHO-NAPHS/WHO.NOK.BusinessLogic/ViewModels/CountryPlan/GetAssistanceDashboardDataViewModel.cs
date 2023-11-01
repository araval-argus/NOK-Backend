// <copyright file="GetAssistanceDashboardDataViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan
{
    using WHO.NAPHS.BusinessLogic.ViewModels.PaginatedResponses;

    /// <summary>
    /// Plan details view model to get the complete plan details for planning page.
    /// </summary>
    public class GetAssistanceDashboardDataViewModel
    {
        /// <summary>
        /// Gets or sets list of <see cref="TechnicalAreaViewModel"/> models for filtration.
        /// </summary>
        public List<TechnicalAreaViewModel> TechnicalAreas { get; set; } = new ();

        /// <summary>
        /// Gets or sets list of <see cref="TechnicalAreaViewModel"/> models for filtration.
        /// </summary>
        public List<TechnicalAreaIndicatorViewModel> TechnicalAreaIndicators { get; set; } = new ();

        /// <summary>
        /// Gets or sets list of <see cref="TechnicalAreaViewModel"/> models for filtration.
        /// </summary>
        public PaginatedResponse<AssistanceDataViewModel> AssistanceData { get; set; } = new ();
    }
}