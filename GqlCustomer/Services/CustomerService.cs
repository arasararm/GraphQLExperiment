using GqlCustomer.Models;
using GqlCustomer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GqlCustomer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerContext _dbContext;

        public CustomerService(CustomerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Customer> GetCustomersAsync()
        {
            return  _dbContext.Customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var res = await _dbContext.Customers
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.Id == id);

            return res;
        }

        public async Task<Customer> PostCustomerCreateAsync(CustomerCreateRequestModel model)
        {
            var mod = new Customer()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Phone = model.Phone,
            };

            var res = _dbContext.Customers.Add(mod);
            await _dbContext.SaveChangesAsync();

            return res.Entity;
        }

        public async Task<Result> PutCustomerUpdateAsync(int id, Customer model)
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

        public async Task<bool> DeleteCustoemrDeleteAsync(int id)
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
