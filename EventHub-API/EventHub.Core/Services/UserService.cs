using EventHub.Core.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.User;
using EventHub.Infrastructure.Common;
using EventHub.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<string> LoginAsync(LoginModel model)
        {
            var user = await repository.AllReadonly<User>()
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (user != null)
            {
                if (user.Email == model.Email) 
                {
                    var passwordHasher = new PasswordHasher<User>();

                    string hashedPass = passwordHasher.HashPassword(user, model.Password);

                    if (user.PasswordHash == hashedPass)
                        return "User successfully logged in!";
                }
            }

            return string.Format(ErrorMessages.DoesntExistErrorMessage, "user");
        }

        public async Task RegisterAsync(RegisterModel model)
        {
            try
            {
                var user = new User()
                {
                    Username = model.UserName, 
                    Email = model.Email, 
                    FirstName = model.FirstName, 
                    LastName = model.LastName,
                    CreatedAt = DateTime.Now,
                };

                var passwordHasher = new PasswordHasher<User>();

                user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

                await repository.AddAsync(user);
                await repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException(ErrorMessages.OperationFailedErrorMessage);
            }
        }
    }
}
