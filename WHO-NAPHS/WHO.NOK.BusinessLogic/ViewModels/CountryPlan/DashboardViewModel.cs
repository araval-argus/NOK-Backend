// <copyright file="DashboardViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.CountryPlan
{
    using WHO.NOK.BusinessLogic.ViewModels.Country;
    using WHO.NOK.BusinessLogic.ViewModels.PaginatedResponses;

    /// <summary>
    /// View model to show dashboard.
    /// </summary>
    public class DashboardViewModel
    {
        /// <summary>
        /// Gets or sets list of <see cref="CountryViewModel"/> models.
        /// </summary>
      public List<CountryViewModel> Countries { get; set; } = null!;

      /// <summary>
      /// Gets or sets paginated list of <see cref="CountryPlanViewModel"/> model.</returns>
      /// </summary>
      public PaginatedResponse<CountryPlanViewModel> CountryPlans { get; set; } = null!;
    }
}