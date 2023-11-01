// <copyright file="CountryPlan.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

// to suppress constructor error.
#pragma warning disable CS8618

namespace WHO.NAPHS.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;
    using Common = WHO.NAPHS.Core.Common;

    /// <summary>
    /// Country Plan DB Model.
    /// </summary>
    public class CountryPlan : ITrackable, IDeletable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryPlan"/> class.
        /// </summary>
        public CountryPlan()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryPlan"/> class.
        /// </summary>
        /// <param name="countryId">The Country Id.</param>
        /// <param name="planStartDate">Plan Start Date.</param>
        /// <param name="planEndDate">Plan End Date.</param>
        /// <param name="countryISOCode">Country ISO Code.</param>
        /// <param name="description">Description.</param>
        /// <param name="planType">Plan Type.</param>
        /// <param name="assessmentType">Assessment Type.</param>
        /// <param name="strategicPlanId"> Strategic plan id.</param>
        /// <param name="planStatus">Plan Status.</param>
        /// <param name="planStage">Plan Stage.</param>
        /// <param name="sendReviewReminder">Send Review Reminder.</param>
        /// <param name="advancedDaysForReviewReminder">Advanced days for review reminder.</param>
        /// <param name="strategicPlanId">Strategic Plan Id.</param>
        /// <param name="hasOfficiallyApprovedPlan">Plan is Officially Approved.</param>
        public CountryPlan(
            int countryId,
            DateTime planStartDate,
            DateTime planEndDate,
            string countryISOCode,
            string description = "",
            int planType = (int)Common.PlanType.Operational,
            int assessmentType = (int)Common.AssessmentType.JEE,
            int? strategicPlanId = null,
            int planStatus = (int)Common.PlanStatus.Draft,
            int planStage = (int)Common.PlanStage.NotStarted,
            bool sendReviewReminder = false,
            int? advancedDaysForReviewReminder = 2,
            bool hasOfficiallyApprovedPlan = false)
        {
            this.Description = description;
            this.PlanTypeId = planType;
            this.CountryId = countryId;
            this.AssessmentTypeId = assessmentType;
            this.PlanStatusId = planStatus;
            this.PlanEndDate = planEndDate.Date;
            this.PlanStartDate = planStartDate.Date;
            this.IsDeleted = false;
            this.PlanStageId = planStage;
            this.SendReviewReminder = sendReviewReminder;
            this.AdvancedDaysForReviewReminder = advancedDaysForReviewReminder;
            this.CountryISOCode = countryISOCode;
            this.HasOfficiallyApprovedPlan = hasOfficiallyApprovedPlan;
            this.VisibleToAnotherCountries = false;
            this.StrategicPlanId = strategicPlanId;
        }

        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        [Key]
        public int CountryPlanId { get; protected set; }

        /// <summary>
        /// Gets or sets strategic plan id.
        /// </summary>
        public int? StrategicPlanId { get; protected set; }

        /// <summary>
        /// Gets or sets Description.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Gets or sets PlanType.
        /// </summary>
        public int PlanTypeId { get; protected set; }

        /// <summary>
        /// Gets or sets CountryId.
        /// </summary>
        public int CountryId { get; protected set; }

        /// <summary>
        /// Gets or sets PlanStartDate.
        /// </summary>
        public DateTime PlanStartDate { get; protected set; }

        /// <summary>
        /// Gets or sets PlanEndDate.
        /// </summary>
        public DateTime PlanEndDate { get; protected set; }

        /// <summary>
        /// Gets or sets AssessmentType.
        /// </summary>
        public int AssessmentTypeId { get; protected set; }

        /// <summary>
        /// Gets or sets PlanStatus.
        /// </summary>
        public int PlanStatusId { get; protected set; }

        /// <summary>
        /// Gets or sets PlanStage.
        /// </summary>
        public int PlanStageId { get; protected set; }

        /// <summary>
        /// Gets or sets Plan code.
        /// </summary>
        public string PlanCode { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets a value indicating whether send reminder for review.
        /// </summary>
        public bool SendReviewReminder { get; protected set; }

        /// <summary>
        /// Gets or sets days before review needs to be send.
        /// </summary>
        public int? AdvancedDaysForReviewReminder { get; protected set; }

        /// <summary>
        /// Gets or sets Country ISO Code.
        /// </summary>
        public string CountryISOCode { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether plan is officially approved or not.
        /// </summary>
        public bool HasOfficiallyApprovedPlan { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether plan is visible to another country.
        /// </summary>
        public bool VisibleToAnotherCountries { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="CountryPlanFrequency"/> enum.
        /// </summary>
        public CountryPlanFrequency? CountryPlanFrequency { get; protected set; }

        /// <inheritdoc/>
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc/>
        public int CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime LastUpdatedAt { get; set; }

        /// <inheritdoc/>
        public int LastUpdatedBy { get; set; }

        /// <inheritdoc/>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets <see cref="AssessmentType"/> enum.
        /// </summary>
        public virtual AssessmentType AssessmentType { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="PlanStage"/> enum.
        /// </summary>
        public virtual PlanStage PlanStage { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="PlanStatus"/> enum.
        /// </summary>
        public virtual PlanStatus PlanStatus { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="PlanType"/> enum.
        /// </summary>
        public virtual PlanType PlanType { get; set; } = null!;

        /// <summary>
        /// Gets or sets list of <see cref="CountryPlanIndicator"/>.
        /// </summary>
        public virtual ICollection<CountryPlanIndicator> CountryPlanIndicators { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="CountryPlanReview"/>.
        /// </summary>
        public virtual ICollection<CountryPlanReview> CountryPlanReviews { get; set; }

        /// <summary>
        /// Update plan details.
        /// </summary>
        /// <param name="countryId"> Country Id.</param>
        /// <param name="planStartDate"> Plan start date.</param>
        /// <param name="planEndDate"> Plan end date.</param>
        /// <param name="assessmentType"> Assessment Type.</param>
        /// <param name="planStatus"> Plan status.</param>
        /// <param name="planStage"> Plan stage.</param>
        public void UpdateCountryPlan(
            int countryId,
            DateTime planStartDate,
            DateTime planEndDate,
            int assessmentType,
            int planStatus,
            int planStage)
        {
            this.CountryId = countryId;
            this.AssessmentTypeId = assessmentType;
            this.PlanEndDate = planEndDate;
            this.PlanStartDate = planStartDate;
            this.PlanStatusId = planStatus;
            this.PlanStageId = planStage;
        }

        /// <summary>
        /// Delete plan.
        /// </summary>
        public void DeletePlan()
        {
            this.IsDeleted = true;
        }

        /// <summary>
        /// Generate the plan code.
        /// </summary>
        public void GeneratePlanCode() => this.PlanCode = $"{this.CountryISOCode}-{this.GetPlanType(this.PlanTypeId)}-{this.PlanStartDate:yyyyMM}-{this.PlanEndDate:yyyyMM}";

        /// <summary>
        /// Get Plan type.
        /// </summary>
        /// <param name="planTypeId"> Plan type id.</param>
        /// <returns> Returns the plan type.</returns>
        public string GetPlanType(int planTypeId)
        {
            if (planTypeId == (int)Common.PlanType.Strategic)
            {
                return "SP";
            }

            return "OP";
        }

        /// <summary>
        /// Set plan status to cancel.
        /// </summary>
        public void CancelPlan()
        {
            this.PlanStatusId = (int)Common.PlanStatus.Cancelled;
        }

        /// <summary>
        /// Activate the plan.
        /// </summary>
        /// <param name="isPlanOfficialApproved"> IsPlanOfficialApproved.</param>
        /// /// <param name="isPlanVisibleToOther"> IsPlanVisibleToOther.</param>
        public void ActivatePlan(bool isPlanOfficialApproved, bool isPlanVisibleToOther)
        {
            this.PlanStatusId = (int)Common.PlanStatus.Active;
            this.HasOfficiallyApprovedPlan = isPlanOfficialApproved;
            this.VisibleToAnotherCountries = isPlanVisibleToOther;
        }
    }
}