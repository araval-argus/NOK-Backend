// <copyright file="IDeletable.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.Core.Common
{
    /// <summary>
    /// Interface to set the delete property of the model.
    /// </summary>
    public interface IDeletable
    {
        /// <summary>
        /// Gets or sets a value indicating whether is deleted or not.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}