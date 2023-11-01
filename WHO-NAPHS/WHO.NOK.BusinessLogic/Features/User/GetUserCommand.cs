// <copyright file="GetUserCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.User
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Get User Command class.
    /// </summary>
    public class GetUserCommand : Request
    {
        /// <summary>
        /// Gets or sets the value of user id.
        /// </summary>
        public int UserId { get; set; }
    }
}