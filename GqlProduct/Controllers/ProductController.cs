using GqlProduct.Helper;
using GqlProduct.Models;
using GqlProduct.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GqlProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IService<Product> _productService;
        private readonly IService<Category> _categoryService;

        public ProductController(IService<Product> productService, IService<Category> categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var result = await _productService.Get();
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        // GET api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var result = await _productService.Get(id);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        // POST api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(AddEditProduct request)
        {
            var productCategory = await _categoryService.Get(request.CategoryId);
            var product = ProductMapper.Map(request, productCategory);

            await _productService.Post(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT api/Product/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, AddEditProduct request)
        {
            var productCategory = await _categoryService.Get(request.CategoryId);
            var product = ProductMapper.Map(request, productCategory);

            var result = await _productService.Put(id, product);
            if (!result.Success)
            {
                if (string.IsNullOrWhiteSpace(result.ErrorMessage))
                    return BadRequest();
                else
                    return NotFound(result.ErrorMessage);
            }
            return NoContent();
        }

        // DELETE api/Product/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await _productService.Delete(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
