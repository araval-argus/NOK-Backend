// <copyright file="IndicatorsWithScoreViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.CountryPlan
{
    /// <summary>
    /// Indicator with score model.
    /// </summary>
    public class IndicatorsWithScoreViewModel : TechnicalAreaIndicatorViewModel
    {
        /// <summary>
        /// Gets or sets score for indicator.
        /// </summary>
        public override int Score { get; set; } = 1;

        /// <summary>
        /// Gets or sets goal for indicator.
        /// </summary>
        public override int Goal { get; set; } = 2;
    }
}