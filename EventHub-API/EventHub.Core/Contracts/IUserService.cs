using EventHub.Core.Models.User;

namespace EventHub.Core.Contracts
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterModel model);
        Task<string> LoginAsync(LoginModel model);
    }
}
