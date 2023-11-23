// <copyright file="PaginationFilter.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Helper
{
    /// <summary>
    /// Pagination filter class.
    /// </summary>
    public class PaginationFilter
    {
        /// <summary>
        /// Gets or sets OrderBy.
        /// </summary>
        public string? OrderBy { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether data should be ordered in ascending order or not.
        /// </summary>
        public bool OrderByAscending { get; set; } = true;

        /// <summary>
        /// Gets or sets PageIndex.
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Gets or sets PageSize.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets Columns and its Values for filter.
        /// </summary>
        public Dictionary<string, HashSet<string>> ColumnsAndValuesForFilter { get; set; } = new Dictionary<string, HashSet<string>>();
    }
}
