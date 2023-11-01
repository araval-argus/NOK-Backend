// <copyright file="IDetailedActivityService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ServiceInterfaces
{
    using WHO.NAPHS.BusinessLogic.Features.DetailedActivity;
    using WHO.NAPHS.BusinessLogic.ViewModels.CountryPlan;
    using WHO.NAPHS.BusinessLogic.ViewModels.DetailedActivity;

    /// <summary>
    /// Detailed activity service interface.
    /// </summary>
    public interface IDetailedActivityService : IBaseService
    {
        /// <summary>
        /// Creates a detailed activity.
        /// </summary>
        /// <param name="request"> <see cref="CreateDetailedActivityCommand"/> command request to create detailed activity.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> CreateDetailedActivityAsync(CreateDetailedActivityCommand request);

        /// <summary>
        /// Edit detailed activity.
        /// </summary>
        /// <param name="request"> <see cref="EditDetailedActivityCommand"/> command request to edit detailed activity.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> EditDetailedActivity(EditDetailedActivityCommand request);

        /// <summary>
        /// Creates a detailed activity.
        /// </summary>
        /// <param name="request"> <see cref="UpdateDetailedActivityCommand"/> command request create detailed activity.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> UpdateDetailedActivityAsync(UpdateDetailedActivityCommand request);

        /// <summary>
        /// Creates a detailed activity.
        /// </summary>
        /// <param name="request"> <see cref="DeleteDetailedActivityCommand"/> command request delete detailed activity.</param>
        /// <returns> Returns <see cref="CompletePlanDetailsViewModel"/> model representing the latest plan details with indicator change.</returns>
        Task<CompletePlanDetailsViewModel> DeleteDetailedActivityAsync(DeleteDetailedActivityCommand request);

        /// <summary>
        /// Creates a detailed activity.
        /// </summary>
        /// <param name="request"> <see cref="GetDetailedActivityCommand"/> command request get detailed activities.</param>
        /// <returns>Returns list of <see cref="DetailedActivityTypeViewModel"/> model.</returns>
        Task<List<DetailedActivityTypeViewModel>> GetDetailedActivityTypesAsync(GetDetailedActivityCommand request);

        /// <summary>
        /// Creates a detailed activity.
        /// </summary>
        /// <param name="request"> <see cref="CreateDetailedActivityTypeCommand"/> command request create detailed activity type.</param>
        /// <returns>Returns the newly created <see cref="DetailedActivityTypeViewModel"/> model.</returns>
        Task<DetailedActivityTypeViewModel> CreateDetailedActivityTypeAsync(CreateDetailedActivityTypeCommand request);
    }
}