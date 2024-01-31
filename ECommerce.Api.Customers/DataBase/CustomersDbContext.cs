using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.DataBase
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<Entities.Customer> Customers { get; set; }

        public CustomersDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
