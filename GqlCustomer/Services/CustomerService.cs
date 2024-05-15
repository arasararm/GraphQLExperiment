using GqlCustomer.Models;
using Microsoft.EntityFrameworkCore;

namespace GqlCustomer.Services
{
    public class CustomerService : IService<Customer>
    {
        private readonly CustomerContext _dbContext;

        public CustomerService(CustomerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> Get()
        {
            if (_dbContext.Customers is null)
                return null;

            return await _dbContext.Customers
                .Include(c => c.Addresses)
                .ToListAsync();
        }

        public async Task<Customer> Get(int id)
        {
            if (_dbContext.Customers is null)
                return null;

            return await _dbContext.Customers
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> Post(Customer model)
        {
            _dbContext.Customers.Add(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<Result> Put(int id, Customer model)
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
            if (_dbContext.Customers is null)
                return false;

            var result = await _dbContext.Customers.FindAsync(id);
            if (result is null)
                return false;

            _dbContext.Customers.Remove(result);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private bool Exists(int id)
        {
            return (_dbContext.Customers?.Any(c => c.Id == id)).GetValueOrDefault();
        }
    }
}
