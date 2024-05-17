using GqlProduct.Models;
using GqlProduct.Services;
using GqlProduct.ViewModels.RequestModels;

namespace GqlProduct.Types
{
    public class Mutation
    {

        public async Task<Product> PostProductCreate(ProductCreateRequestModel product, [Service] IProductService productService)
        {
            return await productService.PostCareateProductAsync(product);
        }

        public async Task<Result> PutProductUpdate(int id, Product product, [Service] IProductService productService)
        {
            return await productService.PutUpdateProductAsync(id,product);
        }

        public async Task<bool> DeleteProductDelete(int id, [Service] IProductService productService)
        {
            return await productService.DeleteProductDeleteAsync(id);
        }
    }
}
