using GqlProduct.Models;
using Microsoft.EntityFrameworkCore;

namespace GqlProduct.Services
{
    public class ProductService : IService<Product>
    {
        private readonly ProductContext _dbContext;

        public ProductService(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            if (_dbContext.Products is null)
                return null;

            return await _dbContext.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> Get(int id)
        {
            if (_dbContext.Products is null)
                return null;

            return await _dbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> Post(Product model)
        {
            _dbContext.Products.Add(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<Result> Put(int id, Product model)
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

        public async Task<bool> Delete(int id)
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
