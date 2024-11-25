using EventHub.Core.Contracts;
using EventHub.Core.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetCategories()
        {
            var model = await categoryService.GetAllCategoriesAsync();

            return Ok(model);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = new CategoryModel();

            try
            {
                model = await categoryService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);
        }
    }
}
