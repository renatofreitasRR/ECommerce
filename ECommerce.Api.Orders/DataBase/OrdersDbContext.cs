using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.DataBase
{
    public class OrdersDbContext: DbContext
    {
        public DbSet<Entities.Order> Orders { get; set; }

        public OrdersDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
