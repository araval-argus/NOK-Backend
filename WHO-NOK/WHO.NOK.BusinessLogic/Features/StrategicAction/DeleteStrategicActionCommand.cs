// <copyright file="DeleteStrategicActionCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.StrategicAction
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to create custom strategic action for strategic or operation plans.
    /// </summary>
    public class DeleteStrategicActionCommand : Request
    {
        /// <summary>
        /// Gets or sets strategic action id.
        /// </summary>
        public int StrategicActionId { get; set; }

        /// <summary>
        /// Gets or sets country plan id.
        /// </summary>
        public int CountryPlanId { get; set; }
    }
}