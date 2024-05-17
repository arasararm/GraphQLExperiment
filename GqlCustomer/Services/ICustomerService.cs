using GqlCustomer.Models;
using GqlCustomer.ViewModels;

namespace GqlCustomer.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> PostCustomerCreateAsync(CustomerCreateRequestModel model);
        Task<Result> PutCustomerUpdateAsync(int id, Customer model);
        Task<bool> DeleteCustoemrDeleteAsync(int id);
    }
}
