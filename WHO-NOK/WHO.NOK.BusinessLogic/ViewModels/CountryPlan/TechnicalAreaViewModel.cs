// <copyright file="TechnicalAreaViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ViewModels.CountryPlan
{
    /// <summary>
    /// Technical area view model.
    /// </summary>
    public class TechnicalAreaViewModel
    {
        /// <summary>
        /// Gets or sets Technical area id.
        /// </summary>
        public int TechnicalAreaId { get; set; }

        /// <summary>
        /// Gets or sets Technical area name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets Area code.
        /// </summary>
        public string? AreaCode { get; set; }

        /// <summary>
        /// Gets or sets area code id.
        /// </summary>
        public string? AreaCodeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active or not.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets Technical source id.
        /// </summary>
        public int SourceId { get; set; }

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="TechnicalAreaIndicatorViewModel"/> for technical area.
        /// </summary>
        public virtual List<TechnicalAreaIndicatorViewModel> TechnicalAreaIndicator { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether is custom technical area.
        /// </summary>
        public bool IsCustomTechnicalArea { get; set; }

        /// <summary>
        /// Gets or sets a value indicating comma separated technical area ids.
        /// </summary>
        public string TechnicalAreaIds { get; set; } = string.Empty;
    }
}