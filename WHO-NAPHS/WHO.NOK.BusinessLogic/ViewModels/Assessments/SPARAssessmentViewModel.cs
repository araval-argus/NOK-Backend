// <copyright file="SPARAssessmentViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.Assessments
{
    /// <summary>
    /// View model for catch up the response from the spar.
    /// </summary>
    public class SPARAssessmentViewModel
    {
        /// <summary>
        /// Gets or sets ISO3 code.
        /// </summary>
        public string ISO3Code { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets country name.
        /// </summary>
        public string CountryName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets submission date.
        /// </summary>
        public string SubmissionStatus { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets global capacity score.
        /// </summary>
        public int? GlobalCapacityScore { get; set; }

        /// <summary>
        /// Gets or sets regional capacity score.
        /// </summary>
        public int? RegionalCapacityScore { get; set; }

        /// <summary>
        /// Gets or sets country capacity score.
        /// </summary>
        public int? CountryCapacityScore { get; set; }

        /// <summary>
        /// Gets or sets lists of capacity.
        /// </summary>
        public List<SPARCapacitiesViewModel> Capacities { get; set; } = new List<SPARCapacitiesViewModel>();

        /// <summary>
        /// Gets or sets timeline.
        /// </summary>
        public SPARTimelineViewModel? Timeline { get; set; }

        /// <summary>
        /// Gets or sets point of entry.
        /// </summary>
        public SPARPointOfEntryViewModel? PointOfEntry { get; set; }
    }
}