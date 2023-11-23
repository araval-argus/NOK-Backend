// <copyright file="AuditCountryPlan.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Audit
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Audit detailed activities DB model.
    /// </summary>
    public class AuditCountryPlan : ITrackable
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        [Key]
        public int AuditCountryPlanId { get; protected set; }

        /// <summary>
        /// Gets or sets audit log status.
        /// </summary>
        public AuditLogRowStatus AuditLogRowStatus { get; set; }

        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int CountryPlanId { get; protected set; }

        /// <summary>
        /// Gets or sets strategic plan id.
        /// </summary>
        public int? StrategicPlanId { get; protected set; }

        /// <summary>
        /// Gets or sets Description.
        /// </summary>
        public string? Description { get; protected set; }

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
        public string? CountryISOCode { get; protected set; }

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
    }
}