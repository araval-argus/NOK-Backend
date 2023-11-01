// <copyright file="EditStrategicActionCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Command to update strategic action.
    /// </summary>
    public class EditStrategicActionCommand : Request
    {
        /// <summary>
        /// Gets or sets strategic action id.
        /// </summary>
        public int StrategicActionId { get; set; }

        /// <summary>
        /// Gets or sets <see cref="PlanStage"/> enum.
        /// </summary>
        public PlanStage ImplementationStatus { get; set; }

        /// <summary>
        /// Gets or sets Objective.
        /// </summary>
        public string? Objective { get; set; }

        /// <summary>
        /// Gets or sets the action information.
        /// </summary>
        public string? Action { get; set; }

        /// <summary>
        /// Gets or sets <see cref="ActionFeasibility"/> enum.
        /// </summary>
        public ActionFeasibility Feasibility { get; set; }

        /// <summary>
        /// Gets or sets <see cref="ActionImpact"/> enum.
        /// </summary>
        public ActionImpact Impact { get; set; }

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
    }
}