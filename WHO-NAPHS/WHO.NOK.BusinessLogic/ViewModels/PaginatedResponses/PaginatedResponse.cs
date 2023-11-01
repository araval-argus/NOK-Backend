// <copyright file="PaginatedResponse.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.PaginatedResponses
{
    /// <summary>
    /// A response class for paginated data.
    /// </summary>
    /// <typeparam name="T">T type.</typeparam>
    public class PaginatedResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedResponse{T}"/> class.
        /// </summary>
        public PaginatedResponse()
        {
        }

        /// <summary>
        /// Gets or sets data.
        /// </summary>
        public List<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// Gets or sets total rows.
        /// </summary>
        public int TotalRows { get; set; } = 0;
    }
}