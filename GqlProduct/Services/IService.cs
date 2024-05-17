using GqlProduct.Models;

namespace GqlProduct.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetProducts();
        Task<T> GetProductById(int id);
        Task<T> Post(T model);
        Task<Result> Put(int id, T model);
        Task<bool> Delete(int id);
    }
}
