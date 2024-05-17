using GqlCustomer.Models;
using GqlCustomer.Services;

namespace GqlCustomer.Types
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<List<Customer>> GetCustomers([Service] ICustomerService customerService)
        {
            var res = await customerService.GetCustomersAsync();
            return res.ToList();
        }

        public async Task<Customer> GetCustomerById(int id, [Service] ICustomerService customerService)
        {
            return await customerService.GetCustomerByIdAsync(id);
        }
    }
}
