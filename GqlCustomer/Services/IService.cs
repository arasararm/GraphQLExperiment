using GqlCustomer.Models;

namespace GqlCustomer.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(int id);
        Task<T> Post(T model);
        Task<Result> Put(int id, T model);
        Task<bool> Delete(int id);
    }
}
