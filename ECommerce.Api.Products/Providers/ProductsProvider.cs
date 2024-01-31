using AutoMapper;
using ECommerce.Api.Products.DataBase;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<IProductsProvider> _logger;
        private readonly IMapper _mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<IProductsProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(_dbContext.Products.Any() is false) 
            {
                _dbContext
                    .Products
                    .Add(new Entities.Product()
                    {
                        Id = 1,
                        Name = "Keyboard",
                        Price = 20,
                        Inventory = 12
                    });

                _dbContext
                   .Products
                   .Add(new Entities.Product()
                   {
                       Id = 2,
                       Name = "Mouse",
                       Price = 30,
                       Inventory = 50
                   });

                _dbContext
                   .Products
                   .Add(new Entities.Product()
                   {
                       Id = 3,
                       Name = "Monitor",
                       Price = 200,
                       Inventory = 34
                   });

                _dbContext
                   .Products
                   .Add(new Entities.Product()
                   {
                       Id = 4,
                       Name = "CPU",
                       Price = 150,
                       Inventory = 55
                   });

                _dbContext.SaveChanges();
            }
        }

        public async Task<(
            bool IsSuccess, 
            IEnumerable<Models.Product> products, 
            string ErrorMessage)>
            GetProductsAsync()
        {
            try
            {
                var products = await _dbContext
                    .Products
                    .ToListAsync();

                if(products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Entities.Product>, IEnumerable<Models.Product>>(products);

                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(
            bool IsSuccess,
            Models.Product product,
            string ErrorMessage)>
            GetProductAsync(int id)
        {
            try
            {
                var product = await _dbContext
                    .Products
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (product != null)
                {
                    var result = _mapper.Map<Entities.Product, Models.Product>(product);

                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
