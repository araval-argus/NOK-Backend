// <copyright file="UploadPlanResponseViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.CountryPlan
{
    /// <summary>
    /// View model <see cref="UploadPlanResponseViewModel"/> of upload plan response.
    /// </summary>
    public class UploadPlanResponseViewModel
    {
        /// <summary>
        /// Gets or sets plan indicator id.
        /// </summary>
        public string? PlanIndicatorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether row is success or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets row status.
        /// </summary>
        public string? RowStatus { get; set; }
    }
}