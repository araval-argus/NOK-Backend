// <copyright file="InitialPlanDetailsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.CountryPlan
{
    /// <summary>
    /// Initial plan details.
    /// </summary>
    public class InitialPlanDetailsViewModel
    {
        /// <summary>
        /// Gets or sets list of <see cref="TechnicalAreaViewModel"/>.
        /// </summary>
        public List<TechnicalAreaViewModel> TechnicalAreas { get; set; } = null!;

        /// <summary>
        /// Gets or sets list of <see cref="CountryPlanViewModel"/> (Strategic plans).
        /// </summary>
        public List<CountryPlanViewModel> StrategicPlans { get; set; } = null!;

        /// <summary>
        /// Gets or sets list of <see cref="CountryPlanViewModel"/> (Operational plans).
        /// </summary>
        public List<CountryPlanViewModel> OperationalPlans { get; set; } = null!;
    }
}