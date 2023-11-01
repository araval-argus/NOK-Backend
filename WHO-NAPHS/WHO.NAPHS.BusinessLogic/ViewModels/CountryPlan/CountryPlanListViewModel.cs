// <copyright file="CountryPlanListViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan
{
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Country Plan view model.
    /// </summary>
    public class CountryPlanListViewModel
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int CountryPlanId { get; set; }

        /// <summary>
        /// Gets or sets Description.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Gets or sets <see cref="PlanType"/> enum.
        /// </summary>
        public PlanType PlanType { get; set; }

        /// <summary>
        /// Gets or sets CountryId.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets PlanStartDate.
        /// </summary>
        public DateTime PlanStartDate { get; set; }

        /// <summary>
        /// Gets or sets PlanEndDate.
        /// </summary>
        public DateTime PlanEndDate { get; set; }

        /// <summary>
        /// Gets or sets <see cref="AssessmentType"/> enum.
        /// </summary>
        public AssessmentType AssessmentType { get; set; }

        /// <summary>
        /// Gets or sets <see cref="PlanStatus"/> enum.
        /// </summary>
        public PlanStatus PlanStatus { get; set; }

        /// <summary>
        /// Gets or sets <see cref="PlanType"/> enum.
        /// </summary>
        public PlanStage PlanStage { get; set; }

        /// <summary>
        /// Gets or sets Plan code.
        /// </summary>
        public string PlanCode { get; set; } = default!;

        /// <summary>
        /// Gets or sets a value indicating whether reminder for review.
        /// </summary>
        public bool SendReviewReminder { get; set; }

        /// <summary>
        /// Gets or sets days before review needs to be send.
        /// </summary>
        public int? AdvancedDaysForReviewReminder { get; set; }

        /// <summary>
        /// Gets or sets StrategicPlanId.
        /// </summary>
        public int? StrategicPlanId { get; set; }

        /// <summary>
        /// Gets or sets Country ISO Code.
        /// </summary>
        public string? CountryISOCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether plan is officially approved or not.
        /// </summary>
        public bool HasOfficiallyApprovedPlan { get; set; }

        /// <summary>
        /// Gets or sets last updated at.
        /// </summary>
        public DateTime LastUpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets <see cref="CountryPlanFrequency"/> enum.
        /// </summary>
        public CountryPlanFrequency? CountryPlanFrequency { get; set; }
    }
}
