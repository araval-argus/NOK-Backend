// <copyright file="CountryPlanViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.CountryPlan
{
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Country plans view model.
    /// </summary>
    public class CountryPlanViewModel
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int CountryPlanId { get; set; }

        /// <summary>
        /// Gets or sets Description.
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
        /// Gets or sets a value indicating whether send reminder for review.
        /// </summary>
        public bool SendReviewReminder { get; set; }

        /// <summary>
        /// Gets or sets days before review needs to be send.
        /// </summary>
        public int? AdvancedDaysForReviewReminder { get; set; }

        /// <summary>
        /// Gets or sets Country ISO Code.
        /// </summary>
        public string CountryISOCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether plan is officially approved or not.
        /// </summary>
        public bool HasOfficiallyApprovedPlan { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether plan is visible to another country.
        /// </summary>
        public bool VisibleToAnotherCountries { get; protected set; }

        /// <summary>
        /// Gets or sets list of ids of TechnicalAreaIndicators id for country plan.
        /// </summary>
        public List<int>? TechnicalAreaIndicators { get; set; }

        /// <summary>
        /// Gets or sets list of operational plans.
        /// </summary>
        public List<CountryPlanViewModel>? OperationalPlans { get; set; }

        /// <summary>
        /// Gets or sets last updated at.
        /// </summary>
        public DateTime LastUpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets strategic plan id.
        /// </summary>
        public int? StrategicPlanId { get; set; }

        /// <summary>
        /// Gets or sets <see cref="CountryPlanFrequency"/> enum.
        /// </summary>
        public CountryPlanFrequency? CountryPlanFrequency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether plan is downloaded or not.
        /// </summary>
        public bool IsPlanDownloaded { get; set; }
    }
}