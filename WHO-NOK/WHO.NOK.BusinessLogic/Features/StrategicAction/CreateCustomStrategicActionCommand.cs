// <copyright file="CreateCustomStrategicActionCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.StrategicAction
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Command to create custom strategic action for strategic or operation plans.
    /// </summary>
    public class CreateCustomStrategicActionCommand : Request
    {
        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        public int CountryPlanId { get; set; }

        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        public int PlanIndicatorId { get; set; }

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

        /// <summary>
        /// Gets or sets old score for Strategic action.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets target score for Strategic action.
        /// </summary>
        public int Goal { get; set; }
    }
}