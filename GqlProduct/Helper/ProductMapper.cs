using GqlProduct.Models;
using Microsoft.EntityFrameworkCore;

namespace GqlProduct.Helper
{
    public class ProductMapper
    {
        public static Product Map(AddEditProduct source, Category category)
        {
            var destination = new Product()
            {
                Id = source.Id,
                Name = source.Name,
                Price = source.Price,
                Description = source.Description,
                Category = category
            };

            return destination;
        }
    }
}
