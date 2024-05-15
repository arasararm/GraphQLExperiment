using GqlProduct.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GqlProduct.Services
{
    public class CategoryService : IService<Category>
    {
        private readonly ProductContext _dbContext;

        public CategoryService(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            if (_dbContext.Categories is null)
                return null;

            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> Get(int id)
        {
            if (_dbContext.Categories is null)
                return null;

            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<Category> Post(Category model)
        {
            _dbContext.Categories.Add(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<Result> Put(int id, Category model)
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
            if (_dbContext.Categories is null)
                return false;

            var result = await _dbContext.Categories.FindAsync(id);
            if (result is null)
                return false;

            _dbContext.Categories.Remove(result);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private bool Exists(int id)
        {
            return (_dbContext.Categories?.Any(c => c.Id == id)).GetValueOrDefault();
        }
    }
}
