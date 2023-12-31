// <copyright file="ValuesController.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WHO.NOK.API.Helper;
    using WHO.NOK.BusinessLogic.ServiceInterfaces;
    using WHO.NOK.BusinessLogic.ViewModels;
    using WHO.NOK.BusinessLogic.ViewModels.Country;
    using WHO.NOK.BusinessLogic.ViewModels.Language;
    using WHO.NOK.Core.Common;
    using WHO.NOK.Core.Wrappers;

    /// <summary>
    /// Controller to get the common values for the drop-downs.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class ValuesController : BaseController
    {
        private readonly IValuesService valuesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        /// <param name="valuesService"><see cref="IValuesService"/> Service.</param>
        public ValuesController(IValuesService valuesService) => this.valuesService = valuesService;

        /// <summary>
        /// Get all the countries.
        /// </summary>
        /// <returns>Returns list of <see cref="CountryViewModel"/> model.</returns>
        // GET: Values/Countries
        [HttpGet("Countries")]
        [ProducesResponseType(typeof(ApiResponse<List<CountryViewModel>>), 200)]
        public async Task<ActionResult<List<CountryViewModel>>> Countries() => this.Ok(await this.valuesService.GetAllCountriesAsync());
    }
}
