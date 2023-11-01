// <copyright file="ICurrentUserService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ServiceInterfaces
{
    /// <summary>
    /// Current user service interface.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Gets User Id.
        /// </summary>
        string UserId { get; }
    }
}