using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GraphQLExperiment.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLExperiment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _productContext;

        public ProductController(ProductContext productContext)
        {
            _productContext = productContext;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_productContext.Products is null)
            {
                return NotFound();
            }
            return await _productContext.Products.ToListAsync();
        }

        // GET api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_productContext.Products is null)
            {
                return NotFound();
            }
            var product = await _productContext.Products.FindAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }

        // POST api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _productContext.Products.Add(product);
            await _productContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT api/Products/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _productContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _productContext.SaveChangesAsync();
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
            return (_productContext.Products?.Any(p => p.Id == id)).GetValueOrDefault();
        }

        // DELETE api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (_productContext.Products is null)
            {
                return NotFound();
            }

            var product = await _productContext.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound();
            }

            _productContext.Products.Remove(product);
            await _productContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
