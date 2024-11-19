using EventHub.Core.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.Category;
using EventHub.Infrastructure.Common;
using EventHub.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository repository;

        public CategoryService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            return await repository.AllReadonly<Category>()
                .Select(x => new CategoryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();
        }

        public async Task<CategoryModel> GetByIdAsync(int id)
        {
            var category = await repository.GetByIdAsync<Category>(id);

            if (category == null)
                throw new ArgumentException(ErrorMessages.DoesntExistErrorMessage);

            return new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
            };
        }
    }
}
