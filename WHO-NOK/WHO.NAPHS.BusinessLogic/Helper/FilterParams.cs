// <copyright file="FilterParams.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Helper
{
    /// <summary>
    /// Class for filter parameters.
    /// </summary>
    /// <typeparam name="T">Generic Class T.</typeparam>
    public class FilterParams<T>
    {
        /// <summary>
        /// Gets or sets Column name.
        /// </summary>
        public string Column { get; set; } = default!;

        /// <summary>
        /// Gets or sets Values.
        /// </summary>
        public HashSet<string> Values { get; set; } = new HashSet<string>();

        /// <summary>
        /// Gets or sets list of <see cref="Predicate"/> functions to be applied while filtering.
        /// </summary>
        public List<Predicate<T>> FilterFunc { get; set; } = new ();
    }
}
