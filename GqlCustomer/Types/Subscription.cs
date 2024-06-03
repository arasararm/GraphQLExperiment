using GqlCustomer.Models;
using Microsoft.EntityFrameworkCore;

namespace GqlCustomer.Types
{
    public class Subscription
    {
        [Subscribe]
        [Topic("OnCareateCustomer")]
        public async Task<Customer> OnCareateCustomerAsync(
            [EventMessage] int customerId,
            [Service] CustomerContext customerContext)
        {
            return await customerContext.Customers.Where(c => c.Id == customerId).FirstAsync();
        }
    }
}
