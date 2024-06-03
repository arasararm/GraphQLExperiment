using GqlCustomer.Models;
using GqlCustomer.Services;
using HotChocolate.Data.Filters;

namespace GqlCustomer.Types
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Customer> GetCustomers([Service] ICustomerService customerService)
        {
            return customerService.GetCustomersAsync();
        }

        public async Task<Customer> GetCustomerById(int id, [Service] ICustomerService customerService)
        {
            return await customerService.GetCustomerByIdAsync(id);
        }
    }
}
