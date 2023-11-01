// <copyright file="NBWActionsViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.PlanRecommendations
{
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Area technical indicators view model.
    /// </summary>
    public class NBWActionsViewModel
    {
        /// <summary>
        /// Gets or Sets NBW Recommendation id.
        /// </summary>
        public int NBWRecommendationId { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ActionFeasibility"/> enum.
        /// </summary>
        public ActionFeasibility Feasibility { get; set; }

        /// <summary>
        /// Gets or sets <see cref="ActionImpact"/> enum.
        /// </summary>
        public ActionImpact Impact { get; set; }

        /// <summary>
        /// Gets or sets Action.
        /// </summary>
        public string? StrategicAction { get; set; }

        /// <summary>
        /// Gets or sets detailed activity.
        /// </summary>
        public string? DetailedActivity { get; set; }

        /// <summary>
        /// Gets or sets responsible authority.
        /// </summary>
        public string? ResponsibleAuthority { get; set; }

        /// <summary>
        /// Gets or Sets Start date.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or Sets end date.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}