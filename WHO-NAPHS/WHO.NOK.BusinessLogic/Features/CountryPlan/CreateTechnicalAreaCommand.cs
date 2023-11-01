// <copyright file="CreateTechnicalAreaCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.Features.CountryPlan
{
    using WHO.NAPHS.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to create custom technical area.
    /// </summary>
    public class CreateTechnicalAreaCommand : Request
    {
        /// <summary>
        /// Gets or sets Technical area name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets list of <see cref="CreateTechnicalAreaIndicatorCommand"/> command.
        /// </summary>
        public List<CreateTechnicalAreaIndicatorCommand> Indicators { get; set; } = new ();
    }
}