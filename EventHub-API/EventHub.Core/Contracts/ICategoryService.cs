using EventHub.Core.Models.Category;

namespace EventHub.Core.Contracts
{
    /// <summary>
    /// Defines the operations related to category management.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieves all categories asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. 
        ///     The task result contains a collection of 
        ///     <see cref="CategoryModel"/>.</returns>
        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();

        /// <summary>
        /// Retrieves a category by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <returns>A task that represents the asynchronous operation. 
        ///     The task result contains the 
        ///     <see cref="CategoryModel"/> corresponding to the specified ID.</returns>
        Task<CategoryModel> GetByIdAsync(int id);
    }
}
