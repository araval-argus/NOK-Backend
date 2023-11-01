// <copyright file="DeleteUserCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.User
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Request command to delete a user.
    /// </summary>
    public class DeleteUserCommand : Request
    {
        /// <summary>
        /// Gets or sets Id of the user to be deleted.
        /// </summary>
        public int UserId { get; set; }
    }
}