using EventHub.Core.Models.EventRegistration;

namespace EventHub.Core.Contracts
{
    /// <summary>
    /// Defines the operations related to managing event registrations.
    /// </summary>
    public interface IEventRegistrationService
    {
        /// <summary>
        /// Retrieves all event registrations for a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="RegistrationInfoModel"/> for the specified user.</returns>
        Task<IEnumerable<RegistrationInfoModel>> GetUserEventRegistrationsAsync(string userId);

        /// <summary>
        /// Adds a new user event registration asynchronously.
        /// </summary>
        /// <param name="model">The <see cref="RegistrationFormModel"/> containing the data for the new registration.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(RegistrationFormModel model);

        /// <summary>
        /// Deletes an event registration by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the registration to delete.</param>
        /// <param name="userId">The identifier of the user who owns the registration. Used for authorization checks.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(int id, string userId);
    }
}
