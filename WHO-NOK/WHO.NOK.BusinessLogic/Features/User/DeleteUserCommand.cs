// <copyright file="DeleteUserCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.User
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NOK.Core.Common;

    /// <summary>
    /// Request command to delete a user.
    /// </summary>
    public class DeleteUserCommand
    {
        /// <summary>
        /// Gets or sets Id of the user to be deleted.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Roles"/> enum.
        /// </summary>
        public Roles RoleId { get; set; }
    }
}