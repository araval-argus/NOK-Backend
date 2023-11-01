// <copyright file="RoleViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels
{
    using WHO.NAPHS.Core.Common;

    /// <summary>
    /// View model for role.
    /// </summary>
    public class RoleViewModel
    {
        /// <summary>
        /// Gets or sets <see cref="Roles"/> enum.
        /// </summary>
        public Roles RoleId { get; set; }

        /// <summary>
        /// Gets or sets Name of the Role.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets description of role.
        /// </summary>
        public string Description { get; set; } = default!;
    }
}
