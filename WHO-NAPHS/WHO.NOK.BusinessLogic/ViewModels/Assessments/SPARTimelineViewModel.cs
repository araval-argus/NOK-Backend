// <copyright file="SPARTimelineViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.Assessments
{
    /// <summary>
    /// View model to spar timeline.
    /// </summary>
    public class SPARTimelineViewModel
    {
        /// <summary>
        /// Gets or sets submission year.
        /// </summary>
        public string? SubmissionYear { get; set; }

        /// <summary>
        /// Gets or sets submission start date.
        /// </summary>
        public string SubmissionStartDate { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets submission date.
        /// </summary
        public string SubmissionEndDate { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets publish date.
        /// </summary>
        public string PublishDate { get; set; } = string.Empty;
    }
}