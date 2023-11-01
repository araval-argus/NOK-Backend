// <copyright file="IPlanRecommendationService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ServiceInterfaces
{
    using WHO.NOK.BusinessLogic.Features.PlanRecommendations;
    using WHO.NOK.BusinessLogic.ViewModels.PlanRecommendations;

    /// <summary>
    /// Plan Recommendations service.
    /// </summary>
    public interface IPlanRecommendationService : IBaseService
    {
        /// <summary>
        /// Create custom IHR action.
        /// </summary>
        /// <param name="request"><see cref="ImportIHRActionsCommand"/> command request to import IHR actions.</param>
        /// <returns>Returns list of <see cref="ImportIHRActionsViewModel"/> model.</returns>
        Task<List<ImportIHRActionsViewModel>> ImportIHRActionsAsync(ImportIHRActionsCommand request);

        /// <summary>
        /// Create custom NBW action.
        /// </summary>
        /// <param name="request"><see cref="ImportNBWActionsCommand"/> command request to import NBW actions.</param>
        /// <returns>Returns list of <see cref="ImportNBWActionsViewModel"/> model.</returns>
        Task<List<ImportNBWActionsViewModel>> ImportNBWActionsAsync(ImportNBWActionsCommand request);
    }
}