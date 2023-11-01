// <copyright file="AssistanceDataViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.CountryPlan
{
    /// <summary>
    /// Assistance Data view model.
    /// </summary>
    public class AssistanceDataViewModel
    {
        /// <summary>
        /// Gets or sets detailed activity id.
        /// </summary>
        public int DetailedActivityId { get; set; }

        /// <summary>
        /// Gets or sets Plan code.
        /// </summary>
        public string PlanCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets technical area.
        /// </summary>
        public string TechnicalArea { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets technical area indicator.
        /// </summary>
        public string TechnicalAreaIndicator { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets technical area indicator id.
        /// </summary>
        public int TechnicalAreaIndicatorId { get; set; }

        /// <summary>
        /// Gets or sets strategic action.
        /// </summary>
        public string Action { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets description of detailed activity.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets estimated cost.
        /// </summary>
        public double? EstimatedCost { get; set; } = 0;

        /// <summary>
        /// Gets or sets country.
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets end date.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}