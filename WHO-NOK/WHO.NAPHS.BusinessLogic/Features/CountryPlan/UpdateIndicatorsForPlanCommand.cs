// <copyright file="UpdateIndicatorsForPlanCommand.cs" company="WHO">
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
    /// Command to create strategic plan.
    /// </summary>
    public class UpdateIndicatorsForPlanCommand : Request
    {
        /// <summary>
        /// Gets or sets plan id.
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="TechnicalAreaIndicatorViewModel"/> that are being used to create new strategic plan.
        /// </summary>
        public List<TechnicalAreaIndicatorViewModel> Indicators { get; set; } = null!;

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets <see cref="AssessmentType"/> enum.
        /// </summary>
        public AssessmentType AssessmentType { get; set; }

        /// <summary>
        /// Gets or sets <see cref="PlanType"/> enum.
        /// </summary>
        public PlanType PlanType { get; set; }
    }
}