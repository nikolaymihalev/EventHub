using EventHub.Core.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Services;
using EventHub.Infrastructure.Common;
using EventHub.Infrastructure.Data;
using EventHub.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.UnitTests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private ApplicationDbContext context;
        private IRepository repository;
        private ICategoryService categoryService;
        private Category category;
        private Category category2;

        [SetUp]
        public void SetUp()
        {
            category = new Category { Id = 1, Name = "Category1" };
            category2 = new Category { Id = 2, Name = "Category2" };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "EventHubDb")
               .Options;

            this.context = new ApplicationDbContext(options);

            this.repository = new Repository(this.context);

            this.repository.AddAsync(category);
            this.repository.AddAsync(category2);

            this.repository.SaveChangesAsync();

            categoryService = new CategoryService(this.repository);
        }

        [Test]
        public async Task GetAllCategoriesAsync_ShouldReturnAllCategories()
        {
            // Act
            var result = await categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
            this.context.Dispose();
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            var categoryId = 1;

            // Act
            var result = await categoryService.GetByIdAsync(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(categoryId, result.Id);
            Assert.AreEqual("Category1", result.Name);
        }

        [Test]
        public void GetByIdAsync_ShouldThrowArgumentException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryId = 60;

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await categoryService.GetByIdAsync(categoryId));
            Assert.AreEqual(string.Format(ErrorMessages.DoesntExistErrorMessage, "category"), ex.Message); 
        }
    }
}
