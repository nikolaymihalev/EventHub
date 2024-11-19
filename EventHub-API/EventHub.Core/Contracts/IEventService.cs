using EventHub.Core.Models.Event;

namespace EventHub.Core.Contracts
{
    /// <summary>
    /// Defines the operations related to managing events.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Retrieves a paginated list of events asynchronously.
        /// </summary>
        /// <param name="currentPage">The current page number (default is 1).</param>
        /// <param name="userId">The user identifier (default is null).</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="EventPageModel"/> with the events for the specified page.</returns>
        Task<EventPageModel> GetEventsForPageAsync(int currentPage = 1, string? userId = null);

        /// <summary>
        /// Retrieves detailed information about a specific event asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="EventInfoModel"/> for the specified event.</returns>
        Task<EventInfoModel> GetEventByIdAsync(int id);

        /// <summary>
        /// Adds a new event asynchronously.
        /// </summary>
        /// <param name="model">The <see cref="EventFormModel"/> containing the data for the new event.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(EventFormModel model);

        /// <summary>
        /// Edits an existing event asynchronously.
        /// </summary>
        /// <param name="model">The <see cref="EventFormModel"/> containing the updated data for the event.</param>
        /// <param name="creatorId">The identifier of the user who created the event. Used for authorization checks.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task EditAsync(EventFormModel model, string creatorId);

        /// <summary>
        /// Deletes an event by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the event to delete.</param>
        /// <param name="creatorId">The identifier of the user who created the event. Used for authorization checks.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(int id, string creatorId);
    }
}
