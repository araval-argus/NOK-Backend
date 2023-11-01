// <copyright file="StrategicAction.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Plans
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Strategic action DB model.
    /// </summary>
    public class StrategicAction : ITrackable, IDeletable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrategicAction"/> class.
        /// </summary>
        public StrategicAction()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategicAction"/> class.
        /// </summary>
        /// <param name="planIndicatorId"> Plan indicator id.</param>
        /// <param name="objective"> Objective of the pan.</param>
        /// <param name="action"> Strategic action details. </param>
        /// <param name="feasibility"> Feasibility.</param>
        /// <param name="impact"> Impact.</param>
        /// <param name="priority"> Priority.</param>
        /// <param name="implementationStatus"> Implementation status.</param>
        /// <param name="responsibleAuthority"> Responsible authority.</param>
        /// <param name="estimatedCost"> Estimated cost.</param>
        /// <param name="comments"> Comments.</param>
        /// <param name="source"> Action source.</param>
        /// <param name="score"> score.</param>
        /// <param name="goal"> Goal.</param>
        public StrategicAction(
            int planIndicatorId,
            string? objective,
            string? action,
            int feasibility,
            int impact,
            int priority,
            Core.Common.PlanStage implementationStatus,
            string? responsibleAuthority,
            double? estimatedCost,
            string? comments,
            int? source = null,
            int? score = null,
            int? goal = null)
        {
            this.Impact = impact;
            this.PlanIndicatorId = planIndicatorId;
            this.Objective = objective;
            this.Action = action;
            this.Feasibility = feasibility;
            this.Priority = priority;
            this.ImplementationStatus = (int)implementationStatus;
            this.ResponsibleAuthority = responsibleAuthority;
            this.EstimatedCost = estimatedCost;
            this.Comments = comments;
            this.Source = source;
            this.Score = score;
            this.Goal = goal;
        }

        /// <summary>
        /// Gets or sets strategic action id.
        /// </summary>
        [Key]
        public int StrategicActionId { get; protected set; }

        /// <summary>
        /// Gets or sets ReferenceId.
        /// </summary>
        public int? ReferenceId { get; protected set; }

        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        public int PlanIndicatorId { get; protected set; }

        /// <summary>
        /// Gets or sets descriptions.
        /// </summary>
        public string? Objective { get; protected set; }

        /// <summary>
        /// Gets or sets the action information.
        /// </summary>
        public string? Action { get; protected set; }

        /// <summary>
        /// Gets or sets feasibility.
        /// </summary>
        public int Feasibility { get; protected set; }

        /// <summary>
        /// Gets or sets impact.
        /// </summary>
        public int Impact { get; protected set; }

        /// <summary>
        /// Gets or sets priority of this action.
        /// </summary>
        public int Priority { get; protected set; }

        /// <summary>
        /// Gets or sets ImplementationStatus.
        /// </summary>
        public int ImplementationStatus { get; protected set; }

        /// <summary>
        /// Gets or sets responsible authority.
        /// </summary>
        public string? ResponsibleAuthority { get; protected set; }

        /// <summary>
        /// Gets or sets estimated cost.
        /// </summary>
        public double? EstimatedCost { get; protected set; }

        /// <summary>
        /// Gets or sets comments.
        /// </summary>
        public string? Comments { get; protected set; }

        /// <summary>
        /// Gets or sets source.
        /// </summary>
        public int? Source { get; protected set; }

        /// <summary>
        /// Gets or sets old score for IHR Benchmark.
        /// </summary>
        public int? Score { get; protected set; }

        /// <summary>
        /// Gets or sets target score for IHR Benchmark.
        /// </summary>
        public int? Goal { get; protected set; }

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
        /// Gets or sets <see cref="StrategicActionFeasibility"/> object.
        /// </summary>
        public virtual StrategicActionFeasibility StrategicActionFeasibility { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="StrategicActionImpact"/> object.
        /// </summary>
        public virtual StrategicActionImpact StrategicActionImpact { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="CountryPlanIndicator"/> object.
        /// </summary>
        public virtual CountryPlanIndicator CountryPlanIndicator { get; set; } = null!;

        /// <summary>
        /// Gets or sets <see cref="StrategicActionPriority"/> object.
        /// </summary>
        public virtual StrategicActionPriority StrategicActionPriority { get; set; } = null!;

        /// <summary>
        /// Updates a strategic action.
        /// </summary>
        /// <param name="objective">Objective.</param>
        /// <param name="action">Action.</param>
        /// <param name="feasibility">Feasibility.</param>
        /// <param name="impact">Impact.</param>
        /// <param name="priority">Priority.</param>
        /// <param name="implementationStatus">Immplementation status.</param>
        /// <param name="responsibleAuthority">Responsible Authority.</param>
        /// <param name="estimatedCost">Estimated Cost.</param>
        /// <param name="comments">Comments.</param>
        public void UpdateStrategicAction(
            string? objective,
            string? action,
            int feasibility,
            int impact,
            int priority,
            int implementationStatus,
            string? responsibleAuthority,
            double? estimatedCost,
            string? comments)
        {
            this.Impact = impact;
            this.Objective = objective;
            this.Action = action;
            this.Feasibility = feasibility;
            this.Priority = priority;
            this.ImplementationStatus = implementationStatus;
            this.ResponsibleAuthority = responsibleAuthority;
            this.EstimatedCost = estimatedCost;
            this.Comments = comments;
        }

        /// <summary>
        /// Deactivates the Strategic action.
        /// </summary>
        public void DeactivateStrategicAction()
        {
            this.IsDeleted = true;
        }

            /// <summary>
        /// Edit detailed activity.
        /// </summary>
        public void EditDetailedActivity()
        {
            this.ReferenceId = this.StrategicActionId;
            this.StrategicActionId = 0;
        }
    }
}