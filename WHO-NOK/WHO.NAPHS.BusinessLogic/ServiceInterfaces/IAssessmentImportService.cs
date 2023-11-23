// <copyright file="IAssessmentImportService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ServiceInterfaces
{
    using WHO.NOK.BusinessLogic.ViewModels.Assessments;

    /// <summary>
    /// Service to fetch assessments like JEE and SPAR from the plan creation time.
    /// </summary>
    public interface IAssessmentImportService : IBaseService
    {
        /// <summary>
        /// Get auth token for JEE to get the country's latest score details for indicators.
        /// </summary>
        /// <returns> Returns the Auth token.</returns>
        Task<string> GetJEEAuthTokenAsync();

        /// <summary>
        /// Get JEE Recommendation for JEE technical areas.
        /// </summary>
        /// <param name="countryISOCode"> Country ISO Code.</param>
        /// <returns> Returns the <see cref="JEEAssessmentViewModel"/> model for technical areas of country.</returns>
        Task<JEEAssessmentViewModel> GetJEERecommendationAsync(string countryISOCode);

        /// <summary>
        /// Get JEE Scores for JEE technical area's indicator for country.
        /// </summary>
        /// <param name="countryISOCode"> Country ISO Code.</param>
        /// <returns> Returns the <see cref="JEEAssessmentViewModel"/> for technical areas of country.</returns>
        Task<JEEAssessmentViewModel> GetJEEScoreAsync(string countryISOCode);

        /// <summary>
        /// Get auth token for SPAR to get the country's latest score details for indicators.
        /// </summary>
        /// <returns> Returns the auth token.</returns>
        Task<string> GetSPARAuthTokenAsync();

        /// <summary>
        /// Get score details for country and year from the SPAR.
        /// </summary>
        /// <param name="countryISO3Code"> Country ISO3 code.</param>
        /// <returns> Returns the <see cref="SPARAssessmentViewModel"/> model that include indicator wise scores.</returns>
        Task<SPARAssessmentViewModel> GetSPARAssessmentAsync(string countryISO3Code);
    }
}