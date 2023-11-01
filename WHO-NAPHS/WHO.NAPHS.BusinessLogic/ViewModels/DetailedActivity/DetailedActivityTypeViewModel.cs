// <copyright file="DetailedActivityTypeViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.DetailedActivity
{
    /// <summary>
    /// Detailed Activity View Model.
    /// </summary>
    public class DetailedActivityTypeViewModel
    {
        /// <summary>
        /// Gets or sets activity type id.
        /// </summary>
        public int ActivityTypeId { get; set; }

        /// <summary>
        /// Gets or sets activity name.
        /// </summary>
        public string? Activity { get; set; }
    }
}