using GqlProduct.Models;
using GqlProduct.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GqlProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IService<Category> _categoryService;

        public CategoryController(IService<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var result = await _categoryService.Get();
            if(result is null)
                return NotFound();

            return Ok(result);
        }

        // GET api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var result = await _categoryService.Get(id);
            if(result is null)
                return NotFound();

            return Ok(result);
        }

        // POST api/Category
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _categoryService.Post(category);

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        // PUT api/Category/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCategory(int id, Category category)
        {
            var result = await _categoryService.Put(id, category);
            if (!result.Success)
            {
                if (string.IsNullOrWhiteSpace(result.ErrorMessage))
                    return BadRequest();
                else
                    return NotFound(result.ErrorMessage);
            }
            return NoContent();
        }

        // DELETE api/Category/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.Delete(id);
            if(!result)
                return NotFound();

            return NoContent();
        }
    }
}
