using EventHub.Core.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.User;
using EventHub.Core.Services;
using EventHub.Infrastructure.Common;
using EventHub.Infrastructure.Data;
using EventHub.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventHub.UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private IUserService userService;
        private User user1;
        private static string userId = Guid.NewGuid().ToString();

        [SetUp]
        public void SetUp()
        {
            var passwordHasher = new PasswordHasher<User>();

            user1 = new User() { Id = userId , Email = "test@gmail.com", Username = "Test", FirstName = "Test", LastName = "Testov", CreatedAt = DateTime.Now};

            user1.PasswordHash = passwordHasher.HashPassword(user1, "test1234");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "EventHubDb")
               .Options;

            this.context = new ApplicationDbContext(options);

            this.repository = new Repository(this.context);

            this.repository.AddAsync(user1);

            this.repository.SaveChangesAsync();

            userService = new UserService(this.repository);
        }

        [Test]
        public async Task Test_LoginShouldLogUser()
        {
            string exResult = "User successfully logged in!";

            var model = new LoginModel()
            {
                Email = "test@gmail.com",
                Password = "test1234"
            };

            string result = await userService.LoginAsync(model);

            Assert.IsTrue(exResult == result);
        }

        [Test]
        public async Task Test_LoginShouldNotLogUser()
        {
            string exResult = ErrorMessages.InvalidLogin;

            var model = new LoginModel()
            {
                Email = "test@gmail.com",
                Password = "test12345678"
            };

            string result = await userService.LoginAsync(model);

            Assert.IsTrue(exResult == result);
        }

        [Test]
        public async Task Test_RegisterShouldThrowException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await userService.RegisterAsync(null));
            Assert.IsTrue(ErrorMessages.OperationFailedErrorMessage == ex.Message);
        }

        [Test]
        public async Task Test_RegisterShouldReturnAlreadyExists()
        {
            string exResult = "Already exists!";

            var model = new RegisterModel()
            {
                Email = "test@gmail.com",
            };

            string result = await userService.RegisterAsync(model);

            Assert.IsTrue(exResult == result);
        }

        [Test]
        public async Task Test_RegisterShouldRegisterUser()
        {
            string exResult = "You was successfully registered!";
            string exLogResult = "User successfully logged in!";

            var model = new RegisterModel()
            {
                Username = "New User",
                Email = "newuser@gmail.com",
                FirstName = "New",
                LastName = "User",
                Password = "newuser1234",
                ConfirmPassword = "newuser1234",
            };

            string result = await userService.RegisterAsync(model);
            string logResult = await userService.LoginAsync(new LoginModel() { Email = "newuser@gmail.com", Password = "newuser1234" });

            Assert.IsTrue(exResult == result);
            Assert.IsTrue(exLogResult == logResult);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
            this.context.Dispose();
        }
    }
}
