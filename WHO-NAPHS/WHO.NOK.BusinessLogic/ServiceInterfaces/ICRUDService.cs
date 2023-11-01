// <copyright file="ICRUDService.cs" company="WHO">
// This source code is owned by WHO and is not allowed to be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from WHO.
// </copyright>

namespace WHO.NAPHS.BusinessLogic.ServiceInterfaces
{
    /// <summary>
    /// I CRUD service interface.
    /// </summary>
    /// <typeparam name="T"> Generic class.</typeparam>
    /// <typeparam name="TM"> Type generic class.</typeparam>
    public interface ICRUDService<T, TM>
        where T : class
    {
        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns> Returns the get all of the model.</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Get By Id.
        /// </summary>
        /// <param name="id"> Id.</param>
        /// <returns> Returns the object by the Id.</returns>
        Task<T> GetByIdAsync(TM id);

        /// <summary>
        /// Save object.
        /// </summary>
        /// <param name="model"> Model to save.</param>
        /// <returns> Returns the affected rows count.</returns>
        Task<int> SaveAsync(T model);

        /// <summary>
        /// Update model.
        /// </summary>
        /// <param name="model"> Model to update.</param>
        /// <returns> Returns the affected rows count.</returns>
        Task<int> UpdateAsync(T model);

        /// <summary>
        /// Delete object.
        /// </summary>
        /// <param name="id"> Id to delete.</param>
        /// <returns> Returns the status of the operation.</returns>
        Task<bool> DeleteAsync(TM id);
    }
}