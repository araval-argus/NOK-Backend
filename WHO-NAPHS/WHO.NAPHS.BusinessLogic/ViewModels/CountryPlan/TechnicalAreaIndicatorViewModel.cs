// <copyright file="TechnicalAreaIndicatorViewModel.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan
{
    using WHO.NAPHS.BusinessLogic.ViewModels.StrategicAction;

    /// <summary>
    /// Technical area indicator view model.
    /// </summary>
    public class TechnicalAreaIndicatorViewModel
    {
        /// <summary>
        /// Gets or sets Indicator Id.
        /// </summary>
        public int TechnicalAreaIndicatorId { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets Technical area.
        /// </summary>
        public int? TechnicalAreaId { get; set; }

        /// <summary>
        /// Gets or sets Plan indicator id.
        /// </summary>
        public int? PlanIndicatorId { get; set; }

        /// <summary>
        /// Gets or sets Indicator code.
        /// </summary>
        public string? IndicatorCode { get; set; }

        /// <summary>
        /// Gets or sets indicator Id.
        /// </summary>
        public string? IndicatorCodeId { get; set; }

        /// <summary>
        /// Gets or sets score.
        /// </summary>
        public virtual int Score { get; set; }

        /// <summary>
        /// Gets or sets goal.
        /// </summary>
        public virtual int Goal { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="StrategicActionViewModel"/> for indicator.
        /// </summary>
        public List<StrategicActionViewModel> StrategicActions { get; set; } = new ();

        /// <summary>
        /// Gets or sets a value indicating whether this indicator exists or not.
        /// </summary>
        public bool IsExists { get; set; }
    }
}