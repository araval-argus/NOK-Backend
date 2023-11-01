// <copyright file="GetScoreForSelectedIndicatorsCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan
{
    using WHO.NOK.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Get score details for selected indicators while plan creation.
    /// </summary>
    public class GetScoreForSelectedIndicatorsCommand : Request
    {
        /// <summary>
        /// Gets or sets list of selected <see cref="TechnicalAreaIndicatorViewModel"/> indicators.
        /// </summary>
        public List<TechnicalAreaIndicatorViewModel> Indicators { get; set; } = new ();

        /// <summary>
        /// Gets or sets <see cref="AssessmentType"/> enum.
        /// </summary>
        public AssessmentType AssessmentType { get; set; }

        /// <summary>
        /// Gets or sets country Id.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets <see cref="PlanType"/> enum.
        /// </summary>
        public PlanType PlanType { get; set; }
    }
}