// <copyright file="DeactivateUserCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.User
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Request command to activate a user.
    /// </summary>
    public class DeactivateUserCommand
    {
        /// <summary>
        /// Gets or sets the id of the user to be activated.
        /// </summary>
        public int UserId { get; set; }
    }
}