using GqlCustomer.Models;
using GqlCustomer.Services;
using GqlCustomer.ViewModels;
using HotChocolate.Subscriptions;

namespace GqlCustomer.Types
{
    public class Mutation
    {
        public async Task<Customer> PostProductCreate(
            CustomerCreateRequestModel product,
            [Service] ICustomerService customerService,
            [Service] ITopicEventSender sender)
        {
            var customer = await customerService.PostCustomerCreateAsync(product);
            await sender.SendAsync("OnCareateCustomer", customer.Id);
            return customer;
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
