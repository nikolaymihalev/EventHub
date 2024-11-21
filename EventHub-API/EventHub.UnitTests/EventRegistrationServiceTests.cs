using EventHub.Core.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.EventRegistration;
using EventHub.Core.Services;
using EventHub.Infrastructure.Common;
using EventHub.Infrastructure.Data;
using EventHub.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.UnitTests
{
    [TestFixture]
    public class EventRegistrationServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private IEventRegistrationService evRegistrationService;
        private EventRegistration evR;
        private EventRegistration evR2;
        private Event event1;
        private Event event2;
        private Event event3;
        private static string userId = Guid.NewGuid().ToString();

        [SetUp]
        public void SetUp()
        {
            event1 = new Event() { Id = 1, CategoryId = 1, CreatorId = "", Title = "Test", Date = DateTime.Now, Description = "", Location = "" };
            event2 = new Event() { Id = 2, CategoryId = 1, CreatorId = "", Title = "Test 2", Date = DateTime.Now, Description = "", Location = "" };
            event2 = new Event() { Id = 3, CategoryId = 1, CreatorId = "", Title = "Test 3", Date = DateTime.Now, Description = "", Location = "" };

            evR = new EventRegistration() { Id = 1, EventId = event1.Id, UserId = userId , RegisteredAt = new DateTime(2024, 10, 15)};
            evR2 = new EventRegistration() { Id = 2, EventId = event2.Id, UserId = userId, RegisteredAt = new DateTime(2024, 05, 25) };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "EventHubDb")
               .Options;

            this.context = new ApplicationDbContext(options);

            this.repository = new Repository(this.context);


            this.repository.AddAsync(event1);
            this.repository.AddAsync(event2);
            this.repository.AddAsync(event3);

            this.repository.AddAsync(evR);
            this.repository.AddAsync(evR2);

            this.repository.SaveChangesAsync();

            evRegistrationService = new EventRegistrationService(this.repository);
        }

        [Test]
        public async Task Test_AddAsyncShouldAddModel()
        {
            int exEvRegCount = 3;

            var eventRegistration = new RegistrationFormModel()
            {
                EventId = 3,
                UserId = userId,
            };

            await evRegistrationService.AddAsync(eventRegistration);

            var eventsRegistrations = await evRegistrationService.GetUserEventRegistrationsAsync(userId);

            Assert.IsTrue(exEvRegCount == eventsRegistrations.Count());
        }

        [Test]
        public void Test_AddAsyncShouldThrowException()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await evRegistrationService.AddAsync(null));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(ErrorMessages.OperationFailedErrorMessage == exception.Message);
        }

        [Test]
        public void Test_DeleteAsyncShouldThrowException_RegistrationDoesntExist()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await evRegistrationService.DeleteAsync(55, userId));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters") == exception.Message);
        }

        [Test]
        public void Test_DeleteAsyncShouldThrowException_UserDoesntExist()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await evRegistrationService.DeleteAsync(2, ""));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters") == exception.Message);
        }

        [Test]
        public async Task Test_DeleteShoudDeleteRegistration()
        {
            int exRegCount = 1;

            await evRegistrationService.DeleteAsync(evR2.Id, userId);

            var regitrations = await evRegistrationService.GetUserEventRegistrationsAsync(userId);

            Assert.IsTrue(exRegCount == regitrations.Count());
        }

        [Test]
        public async Task Test_GetUserEventRegistrationsShouldReturnEmptyList()
        {
            int exCount = 0;

            var list = await evRegistrationService.GetUserEventRegistrationsAsync("");

            Assert.IsTrue(exCount == list.Count());
        }

        [Test]
        public async Task Test_GetUserEventRegistrationsShouldReturnList()
        {
            int exCount = 2;

            var list = await evRegistrationService.GetUserEventRegistrationsAsync(userId);

            Assert.IsTrue(exCount == list.Count());
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
            this.context.Dispose();
        }
    }
}
