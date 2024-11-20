using EventHub.Core.Models.User;

namespace EventHub.Core.Contracts
{
    /// <summary>
    /// Defines the operations for managing user authentication and registration.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="model">The <see cref="RegisterModel"/> containing the user's registration details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RegisterAsync(RegisterModel model);

        /// <summary>
        /// Authenticates a user and generates a login token asynchronously.
        /// </summary>
        /// <param name="model">The <see cref="LoginModel"/> containing the user's login credentials.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a string representing the authentication token.</returns>
        Task<string> LoginAsync(LoginModel model);
    }
}
