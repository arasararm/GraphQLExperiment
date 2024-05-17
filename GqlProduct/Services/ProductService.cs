using GqlProduct.Models;
using GqlProduct.ViewModels.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace GqlProduct.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _dbContext;

        public ProductService(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> PostCareateProductAsync(ProductCreateRequestModel model)
        {
            var prod = new Product()
            {
                Description = model.Description,
                Name = model.Name,
                Price = model.Price,
                Category = new Category()
                {
                    Name = model.Category.Name,
                }
            };

            var entity = _dbContext.Products.Add(prod);
            await _dbContext.SaveChangesAsync();

            return entity.Entity;
        }

        public async Task<Result> PutUpdateProductAsync(int id, Product model)
        {
            if (id != model.Id)
                return new Result { Success = false };

            _dbContext.Entry(model).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(id))
                    return new Result { Success = false, ErrorMessage = ex.Message };
                else
                    throw;
            }

            return new Result { Success = true };
        }

        public async Task<bool> DeleteProductDeleteAsync(int id)
        {
            if (_dbContext.Products is null)
                return false;

            var result = await _dbContext.Products.FindAsync(id);
            if (result is null)
                return false;

            _dbContext.Products.Remove(result);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private bool Exists(int id)
        {
            return (_dbContext.Products?.Any(c => c.Id == id)).GetValueOrDefault();
        }
    }
}
