// <copyright file="CreateStrategicPlanCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan
{
    using WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Command to create strategic plan.
    /// </summary>
    public class CreateStrategicPlanCommand : Request
    {
        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets Start Date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets End Date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets <see cref="IndicatorsWithScoreViewModel"/> that are being used to create new strategic plan.
        /// </summary>
        public List<IndicatorsWithScoreViewModel> Indicators { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="AssessmentType"/> enum.
        /// </summary>
        public AssessmentType AssessmentType { get; set; }
    }
}