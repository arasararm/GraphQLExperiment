using GqlProduct.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GqlProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ProductContext _dbContext;

        public CategoryController(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            if (_dbContext.Categories is null)
            {
                return NotFound();
            }
            return await _dbContext.Categories.ToListAsync();
        }

        // GET api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            if (_dbContext.Categories is null)
            {
                return NotFound();
            }
            var category = await _dbContext.Categories.FindAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            return category;
        }

        // POST api/Category
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        // PUT api/Category/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(category).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CategoryExists(id))
                {
                    return NotFound(ex.Message);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_dbContext.Categories?.Any(c => c.Id == id)).GetValueOrDefault();
        }

        // DELETE api/Category/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            if (_dbContext.Categories is null)
            {
                return NotFound();
            }

            var category = await _dbContext.Categories.FindAsync(id);
            if (category is null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
