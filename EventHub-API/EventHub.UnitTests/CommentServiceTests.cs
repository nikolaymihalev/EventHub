using EventHub.Core.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.Comment;
using EventHub.Core.Services;
using EventHub.Infrastructure.Common;
using EventHub.Infrastructure.Data;
using EventHub.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.UnitTests
{
    [TestFixture]
    public class CommentServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private ICommentService commentService;
        private Comment com;
        private Comment com2;
        private static string userId = Guid.NewGuid().ToString();

        [SetUp]
        public void SetUp()
        {
            com = new Comment() { Id = 1,Content = "Cool", CreatedAt = new DateTime(2024,10,11), EventId = 1, UserId = userId};
            com2 = new Comment() { Id = 2, Content = "Amazing", CreatedAt = new DateTime(2024, 11, 12), EventId = 6, UserId = userId };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "EventHubDb")
               .Options;

            this.context = new ApplicationDbContext(options);

            this.repository = new Repository(this.context);

            this.repository.AddAsync(com);
            this.repository.AddAsync(com2);

            this.repository.SaveChangesAsync();

            commentService = new CommentService(this.repository);
        }

        [Test]
        public async Task Test_AddAsyncShouldAddModel()
        {
            int exCommentsCount = 2;
            string exMessage = "Test content 3";

            var comment = new CommentFormModel()
            {
                Content = "Test content 3", 
                EventId = 6,
                UserId = userId,
            };

            await commentService.AddAsync(comment);

            var comments = await commentService.GetEventCommentsAsync(6);

            Assert.IsTrue(exCommentsCount == comments.Count());
            Assert.IsTrue(exMessage == comments.Last().Content);
        }

        [Test]
        public void Test_AddAsyncShouldThrowException()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await commentService.AddAsync(null));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(ErrorMessages.OperationFailedErrorMessage == exception.Message);
        }

        [Test]
        public void Test_DeleteAsyncShouldThrowException_CommentDoesntExist()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await commentService.DeleteAsync(55, userId));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters") == exception.Message);
        }

        [Test]
        public void Test_DeleteAsyncShouldThrowException_UserDoesntExist()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await commentService.DeleteAsync(6, ""));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters") == exception.Message);
        }

        [Test]
        public async Task Test_DeleteShoudDeleteComment()
        {
            int exComCount = 0;

            await commentService.DeleteAsync(com2.Id, userId);

            var comments = await commentService.GetEventCommentsAsync(6);

            Assert.IsTrue(exComCount == comments.Count());
        }

        [Test]
        public void Test_EditAsyncShouldThrowException_CommentIsEmpty()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await commentService.EditAsync(new CommentFormModel(), userId));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters") == exception.Message);
        }

        [Test]
        public void Test_EditAsyncShouldThrowException_UserDoesntExist()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await commentService.EditAsync(new CommentFormModel() { Id = 2}, ""));

            Assert.IsNotNull(exception.Message);
            Assert.IsTrue(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters") == exception.Message);
        }

        [Test]
        public async Task Test_EditShoudEditComment()
        {
            var newCom = new CommentFormModel()
            {
                Id = 2,
                Content = "Amazing!!!!!",
            };

            await commentService.EditAsync(newCom, userId);

            var comments = await commentService.GetEventCommentsAsync(6);
            
            Assert.IsNotNull(comments);
            Assert.IsTrue(newCom.Content == comments.First().Content);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
            this.context.Dispose();
        }
    }
}
