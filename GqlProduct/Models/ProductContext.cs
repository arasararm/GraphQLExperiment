using Microsoft.EntityFrameworkCore;

namespace GqlProduct.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null;
        public DbSet<Category> Categories { get; set; } = null;

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {

        }
    }
}
