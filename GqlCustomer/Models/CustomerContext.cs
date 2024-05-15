using Microsoft.EntityFrameworkCore;

namespace GqlCustomer.Models
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null;

        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {

        }
    }
}
