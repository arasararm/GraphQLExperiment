using GqlProduct.Helper;
using GqlProduct.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GqlProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _dbContext;

        public ProductController(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_dbContext.Products is null)
            {
                return NotFound();
            }
            return await _dbContext.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        // GET api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_dbContext.Products is null)
            {
                return NotFound();
            }
            var product = await _dbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }

        // POST api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(AddEditProduct request)
        {
            var category = await _dbContext.Categories.FindAsync(request.CategoryId);
            var product = ProductMapper.Map(request, category);
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT api/Product/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, AddEditProduct request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var category = await _dbContext.Categories.FindAsync(request.CategoryId);
            var product = ProductMapper.Map(request, category);

            _dbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductExists(id))
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

        private bool ProductExists(int id)
        {
            return (_dbContext.Products?.Any(p => p.Id == id)).GetValueOrDefault();
        }

        // DELETE api/Product/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (_dbContext.Products is null)
            {
                return NotFound();
            }

            var product = await _dbContext.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
