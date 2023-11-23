// <copyright file="CreateOperationalPlanCommand.cs" company="WHO">
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
    /// Command to create operational plan.
    /// </summary>
    public class CreateOperationalPlanCommand : Request
    {
        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets <see cref="CountryPlanViewModel"/> representing Strategic plan.
        /// </summary>
        public CountryPlanViewModel? StrategicPlan { get; set; }

        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="IndicatorsWithScoreViewModel"/> that are being used to create new operational plan.
        /// </summary>
        public List<IndicatorsWithScoreViewModel> Indicators { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="AssessmentType"/> enum.
        /// </summary>
        public AssessmentType AssessmentType { get; set; }
    }
}