// <copyright file="IStrategicActionService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NOK.BusinessLogic.ServiceInterfaces
{
    using WHO.NOK.BusinessLogic.Features;
    using WHO.NOK.BusinessLogic.Features.StrategicAction;
    using WHO.NOK.BusinessLogic.ViewModels.CountryPlan;

    /// <summary>
    /// Service interface to perform operations on strategic actions.
    /// </summary>
    public interface IStrategicActionService : IBaseService
    {
        /// <summary>
        /// Create custom strategic action for plan.
        /// </summary>
        /// <param name="request"><see cref="CreateCustomStrategicActionCommand"/> Command request to create strategic action.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> CreateCustomStrategicActionAsync(CreateCustomStrategicActionCommand request);

        /// <summary>
        /// Add JEE recommendations.
        /// </summary>
        /// <param name="request"><see cref="AddJEERecommendationCommand"/> Command request to add JEE recommendations for indicator.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> AddJEERecommendationAsync(AddJEERecommendationCommand request);

        /// <summary>
        /// Add JEE recommendations.
        /// </summary>
        /// <param name="request"><see cref="DeleteStrategicActionCommand"/> Command request to delete strategic action.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> DeleteStrategicActionAsync(DeleteStrategicActionCommand request);

        /// <summary>
        /// Update a strategic action.
        /// </summary>
        /// <param name="request"><see cref="UpdateStrategicActionCommand"/> Command request to update a strategic action.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> UpdateStrategicAction(UpdateStrategicActionCommand request);

        /// <summary>
        /// Update a strategic action.
        /// </summary>
        /// <param name="request"><see cref="EditStrategicActionCommand"/> Command request to edit a strategic action.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> EditStrategicAction(EditStrategicActionCommand request);
    }
}