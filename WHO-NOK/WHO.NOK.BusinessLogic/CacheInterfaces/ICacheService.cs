// <copyright file="ICacheService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.CacheInterfaces
{
    /// <summary>
    /// ICacheInterface service is used as a Base Interface for Cache Implementation.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Clears the Cache.
        /// </summary>
        void ClearCache();
    }
}
