// <copyright file="CountryPlanReviewViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan
{
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// View model for review of the country plan.
    /// </summary>
    public class CountryPlanReviewViewModel
    {
        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanReviewId { get; set; }

        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanId { get; set; }

        /// <summary>
        /// Gets or sets review date.
        /// </summary>
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// Gets or sets the status of a review.
        /// </summary>
        public ReviewStatus ReviewStatus { get; set; }

        /// <summary>
        /// Gets or sets plan's Description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

         /// <summary>
        /// Gets or sets <see cref="PlanType"/> enum.
        /// </summary>
        public PlanType PlanTypeId { get; set; }

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
        public AssessmentType AssessmentTypeId { get; set; }

        /// <summary>
        /// Gets or sets <see cref="PlanStatus"/> enum.
        /// </summary>
        public PlanStatus PlanStatusId { get; set; }

        /// <summary>
        /// Gets or sets <see cref="PlanStage"/> enum.
        /// </summary>
        public PlanStage PlanStageId { get; set; }

        /// <summary>
        /// Gets or sets Plan code.
        /// </summary>
        public string PlanCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        public string CountryName { get; set; } = string.Empty;
    }
}