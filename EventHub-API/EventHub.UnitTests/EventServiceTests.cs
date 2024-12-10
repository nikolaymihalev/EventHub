using EventHub.Core.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.Comment;
using EventHub.Core.Models.Event;
using EventHub.Core.Services;
using EventHub.Infrastructure.Common;
using EventHub.Infrastructure.Data;
using EventHub.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.UnitTests
{
    [TestFixture]
    public class EventServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private IEventService eventService;
        private Event ev;
        private static string userId = Guid.NewGuid().ToString();

        [SetUp]
        public void SetUp()
        {
            ev = new Event() { Id = 1, Title  = "Test" , Description = "Test Description", Date = new DateTime(2024, 12, 15), CreatorId = userId, CategoryId = 1, Location = "NY York"};

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "EventHubDb")
               .Options;

            this.context = new ApplicationDbContext(options);

            this.repository = new Repository(this.context);

            this.repository.AddAsync(ev);

            this.repository.SaveChangesAsync();

            eventService = new EventService(this.repository);
        }        

        [Test]
        public void Test_AddAsyncShouldThrowException()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await eventService.AddAsync(null));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(ErrorMessages.OperationFailedErrorMessage == exception.Message);
        }

        [Test]
        public void Test_DeleteAsyncShouldThrowException_EventDoesntExist()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await eventService.DeleteAsync(55, userId));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters") == exception.Message);
        }

        [Test]
        public void Test_DeleteAsyncShouldThrowException_UserDoesntExist()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await eventService.DeleteAsync(1, ""));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters") == exception.Message);
        }

        [Test]
        public async Task Test_DeleteShoudDeleteComment()
        {
            int exEventCount = 0;

            await eventService.DeleteAsync(ev.Id, userId);

            var events = await eventService.GetEventsForPageAsync();

            Assert.IsTrue(exEventCount == events.Events.Count());
            Assert.IsTrue(exEventCount == events.PagesCount);
        }

        [Test]
        public void Test_EditAsyncShouldThrowException_EventIsNull()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await eventService.EditAsync(new EventFormModel(), userId));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters") == exception.Message);
        }

        [Test]
        public void Test_EditAsyncShouldThrowException_UserDoesntExist()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await eventService.EditAsync(new EventFormModel() { Id = 1 }, ""));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters") == exception.Message);
        }

        [Test]
        public async Task Test_EditShoudEditComment()
        {
            string exTitle = "Edit model";
            string exDesc = "Edit Description";
            int exCategoryId = 3;
            string exLocation = "France";

            var newEv = new EventFormModel()
            {
                Id = 1,
                Title = "Edit model",
                Description = "Edit Description",
                Date = new DateTime(2024, 07, 25),
                CreatorId = userId,
                CategoryId = 3,
                Location = "France"
            };

            await eventService.EditAsync(newEv, userId);

            var eventModel = await eventService.GetEventByIdAsync(1);

            Assert.IsTrue(exTitle == eventModel.Title);
            Assert.IsTrue(exDesc == eventModel.Description);
            Assert.IsTrue(exCategoryId == eventModel.CategoryId);
            Assert.IsTrue(exLocation == eventModel.Location);
        }

        [Test]
        public void Test_GetByIdShouldThrowException()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await eventService.GetEventByIdAsync(100));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(string.Format(ErrorMessages.DoesntExistErrorMessage, "event") == exception.Message);
        }

        [Test]
        public async Task Test_GetEventsForPageShouldReturnTrueModel()
        {
            int exPagesCount = 1;
            int exCurrentPage = 1;
            int exEventsCount = 1;

            var result = await eventService.GetEventsForPageAsync();

            Assert.IsTrue(exCurrentPage == result.CurrentPage);
            Assert.IsTrue(exPagesCount == result.PagesCount);
            Assert.IsTrue(exEventsCount == result.Events.Count());
        }

        [Test]
        public async Task Test_GetEventsForPageShouldReturnEmptyModel()
        {
            int exPagesCount = 1;
            int exCurrentPage = 2;
            int exEventsCount = 0;

            var result = await eventService.GetEventsForPageAsync(2);

            Assert.IsTrue(exCurrentPage == result.CurrentPage);
            Assert.IsTrue(exPagesCount == result.PagesCount);
            Assert.IsTrue(exEventsCount == result.Events.Count());
        }

        [Test]
        public async Task Test_GetEventsForPageShouldReturnEmptyModelForUser()
        {
            int exPagesCount = 0;
            int exCurrentPage = 1;
            int exEventsCount = 0;

            var result = await eventService.GetEventsForPageAsync(1, "");

            Assert.IsTrue(exCurrentPage == result.CurrentPage);
            Assert.IsTrue(exPagesCount == result.PagesCount);
            Assert.IsTrue(exEventsCount == result.Events.Count());
        }

        [Test]
        public async Task Test_GetEventsForPageShouldReturnTrueModelForUser()
        {
            int exPagesCount = 1;
            int exCurrentPage = 1;
            int exEventsCount = 1;

            var result = await eventService.GetEventsForPageAsync(1, userId);

            Assert.IsTrue(exCurrentPage == result.CurrentPage);
            Assert.IsTrue(exPagesCount == result.PagesCount);
            Assert.IsTrue(exEventsCount == result.Events.Count());
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
            this.context.Dispose();
        }
    }
}
