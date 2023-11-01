// <copyright file="UserRole.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.Infrastructure.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using Common = WHO.NOK.Core.Common;

    /// <summary>
    /// UserRole database model.
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole"/> class.
        /// </summary>
        public UserRole()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole"/> class.
        /// </summary>
        /// <param name="roleId"> <see cref="Common.Roles"/> enum.</param>
        public UserRole(Common.Roles roleId)
        {
            this.RoleId = roleId;
        }

        /// <summary>
        /// Gets or sets Id of user role.
        /// </summary>
        [Key]
        public int UserRoleId { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public int UserId { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets <see cref="Common.Roles"/> enum.
        /// </summary>
        public Common.Roles RoleId { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets <see cref="Role"/> object.
        /// </summary>
        public virtual Role? Role { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="User"/> object.
        /// </summary>
        public virtual User? User { get; protected set; }

        /// <summary>
        /// Changes the role of the user.
        /// </summary>
        /// <param name="role"><see cref="Common.Role"/> enum.</param>
        public void ChangeRole(Common.Roles role)
        {
            this.RoleId = role;
        }
    }
}