// <copyright file="CustomTechnicalAreasCommand.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.Features.CountryPlan
{
    using WHO.NOK.BusinessLogic.ViewModels.RequestClaims;

    /// <summary>
    /// Command to create custom technical area.
    /// </summary>
    public class CustomTechnicalAreasCommand : Request
    {
        /// <summary>
        /// Gets or sets list of <see cref="CreateTechnicalAreaCommand"/> command.
        /// </summary>
        public List<CreateTechnicalAreaCommand> TechnicalAreas { get; set; } = new ();

        /// <summary>
        /// Gets or sets country id.
        /// </summary>
        public int CountryId { get; set; }
    }
}