// <copyright file="ImportIHRActionsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.PlanRecommendations
{
    /// <summary>
    /// Area technical indicators view model.
    /// </summary>
    public class ImportIHRActionsViewModel
    {
        /// <summary>
        /// Gets or sets technical area.
        /// </summary>
        public string? TechnicalArea { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="IHRIndicatorsListViewModel"/>.
        /// </summary>
        public List<IHRIndicatorsListViewModel> Indicators { get; set; } = new ();
    }
}