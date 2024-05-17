using GqlProduct.Models;
using GqlProduct.Services;

namespace GqlProduct.Types
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<List<Product>> GetProducts([Service] IProductService productService)
        {
            return await productService.GetProducts();
        }

        public async Task<Product> GetProductById(int id, [Service] IProductService productService)
        {
            return await productService.GetProductById(id);
        }
    }
}
