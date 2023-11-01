// <copyright file="StrategicActionViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.StrategicAction
{
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Strategic action view model for strategic plan.
    /// </summary>
    public class StrategicActionViewModel
    {
        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        public int StrategicActionId { get; set; }

        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        public int PlanIndicatorId { get; set; }

        /// <summary>
        /// Gets or sets descriptions.
        /// </summary>
        public string? Objective { get; set; }

        /// <summary>
        /// Gets or sets actions.
        /// </summary>
        public string? Action { get; set; }

        /// <summary>
        /// Gets or sets feasibility.
        /// </summary>
        public int Feasibility { get; set; }

        /// <summary>
        /// Gets or sets impact.
        /// </summary>
        public int Impact { get; set; }

        /// <summary>
        /// Gets or sets priority of this action.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets <see cref="PlanStage"/> enum.
        /// </summary>
        public PlanStage ImplementationStatus { get; protected set; }

        /// <summary>
        /// Gets or sets responsible authority.
        /// </summary>
        public string? ResponsibleAuthority { get; set; }

        /// <summary>
        /// Gets or sets estimated cost.
        /// </summary>
        public double? EstimatedCost { get; set; }

        /// <summary>
        /// Gets or sets comments.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Gets or sets source.
        /// </summary>
        public int? Source { get; set; }

        /// <summary>
        /// Gets or sets score for Strategic action.
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        /// Gets or sets goal for Strategic action.
        /// </summary>
        public int? Goal { get; set; }

        /// <summary>
        /// Gets or sets Technical AreaIndicator Id.
        /// </summary>
        public int TechnicalAreaIndicatorId { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="DetailActivityViewModel"/> representing detailed activities.
        /// </summary>
        public List<DetailActivityViewModel> DetailActivities { get; set; } = null!;
    }
}