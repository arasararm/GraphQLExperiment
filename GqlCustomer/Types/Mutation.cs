using GqlCustomer.Models;
using GqlCustomer.Services;
using GqlCustomer.ViewModels;

namespace GqlCustomer.Types
{
    public class Mutation
    {
        public async Task<Customer> PostProductCreate(CustomerCreateRequestModel product, [Service] ICustomerService customerService)
        {
            return await customerService.PostCustomerCreateAsync(product);
        }

        public async Task<Result> PutProductUpdate(int id, Customer product, [Service] ICustomerService customerService)
        {
            return await customerService.PutCustomerUpdateAsync(id, product);
        }

        public async Task<bool> DeleteProductDelete(int id, [Service] ICustomerService customerService)
        {
            return await customerService.DeleteCustoemrDeleteAsync(id);
        }
    }
}
