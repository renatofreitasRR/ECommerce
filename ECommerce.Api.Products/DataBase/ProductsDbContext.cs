using ECommerce.Api.Products.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.DataBase
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
