// <copyright file="AddJEERecommendationCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.StrategicAction
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;
    using WHO.NAPHS.BusinessLogic.ViewModels.StrategicAction;

    /// <summary>
    /// Add JEE recommendation command.
    /// </summary>
    public class AddJEERecommendationCommand : Request
    {
        /// <summary>
        /// Gets or sets Plan Id.
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Gets or sets country Id.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="AddJeeRecommendationViewModel"/> models.
        /// </summary>
        public List<AddJeeRecommendationViewModel> JeeRecommendations { get; set; } = new ();
    }
}