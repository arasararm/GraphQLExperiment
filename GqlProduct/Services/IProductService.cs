using GqlProduct.Models;
using GqlProduct.ViewModels.RequestModels;

namespace GqlProduct.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> PostCareateProductAsync(ProductCreateRequestModel model);
        Task<Result> PutUpdateProductAsync(int id, Product model);
        Task<bool> DeleteProductDeleteAsync(int id);
    }
}
