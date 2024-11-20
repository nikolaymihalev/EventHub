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
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user != null) 
            {
                var passwordHasher = new PasswordHasher<User>();

                var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    return "User successfully logged in!";
                }
            }         

            return string.Format(ErrorMessages.DoesntExistErrorMessage, "user");
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            try
            {
                var user = await repository.AllReadonly<User>()
                    .FirstOrDefaultAsync(x => x.Email == model.Email);

                if (user != null)
                    return "Already exists!";

                user = new User()
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

                return "You was successfully registered!";
            }
            catch (Exception)
            {
                throw new ArgumentException(ErrorMessages.OperationFailedErrorMessage);
            }
        }
    }
}
