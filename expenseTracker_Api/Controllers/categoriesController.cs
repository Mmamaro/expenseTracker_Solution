using expenseTracker_Api.Models;
using expenseTracker_Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace expenseTracker_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoriesController : ControllerBase
    {
        private readonly categoriesRepository _categoriesRepository;

        public categoriesController(categoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            IEnumerable<Category> categories = await _categoriesRepository.GetAllCategoriesAsync();

            if (categories == null)
            {
                return BadRequest();
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            Category? category = await _categoriesRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
    }

}
