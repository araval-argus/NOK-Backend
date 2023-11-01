// <copyright file="Permission.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Infrastructure.Models.Permissions
{
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// Model class for all type of permissions.
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Gets or sets id of the permission.
        /// </summary>
        public int PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the value of the activity that the user wants to perform.
        /// </summary>
        public string Activity { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the minimal role id that the user having at least this role can perform the activity.
        /// </summary>
        public Roles MinimalRoleId { get; set; }
     }
}