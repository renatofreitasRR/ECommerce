using AutoMapper;
using ECommerce.Api.Customers.DataBase;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext _dbContext;
        private readonly ILogger<ICustomersProvider> _logger;
        private readonly IMapper _mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<ICustomersProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (_dbContext.Customers.Any() is false)
            {
                _dbContext
                    .Customers
                    .Add(new Entities.Customer()
                    {
                        Id = 1,
                        Name = "Renato",
                        Address = "Rua dos bobos",
                    });

                _dbContext
                   .Customers
                   .Add(new Entities.Customer()
                   {
                       Id = 2,
                       Name = "Mickey",
                       Address = "Rua dos trouxas",
                   });

                _dbContext
                   .Customers
                   .Add(new Entities.Customer()
                   {
                       Id = 3,
                       Name = "Rodrigo",
                       Address = "Rua dos becos",
                   });

                _dbContext
                   .Customers
                   .Add(new Entities.Customer()
                   {
                       Id = 4,
                       Name = "Rafael",
                       Address = "Rua dos monges",
                   });

                _dbContext.SaveChanges();
            }
        }

        public async Task<(
            bool IsSuccess,
            IEnumerable<Models.Customer> customers,
            string ErrorMessage)>
            GetCustomersAsync()
        {
            try
            {
                var customers = await _dbContext
                    .Customers
                    .ToListAsync();

                if (customers != null && customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<Entities.Customer>, IEnumerable<Models.Customer>>(customers);

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

        public async Task<(
            bool IsSuccess,
            Models.Customer customer,
            string ErrorMessage)>
            GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _dbContext
                    .Customers
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (customer != null)
                {
                    var result = _mapper.Map<Entities.Customer, Models.Customer>(customer);

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
