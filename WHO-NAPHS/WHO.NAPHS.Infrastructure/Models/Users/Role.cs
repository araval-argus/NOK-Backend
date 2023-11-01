// <copyright file="Role.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Role Database Model.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets or sets <see cref="Roles"/> enum.
        /// </summary>
        [Key]
        public Roles RoleId { get; protected set; }

        /// <summary>
        /// Gets or sets Name of the Role.
        /// </summary>
        public string Name { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets description of role.
        /// </summary>
        public string Description { get; protected set; } = default!;

        /// <summary>
        /// Gets or sets list of <see cref="UserRole"/> belonging to current role.
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; protected set; } = new List<UserRole>();
    }
}
