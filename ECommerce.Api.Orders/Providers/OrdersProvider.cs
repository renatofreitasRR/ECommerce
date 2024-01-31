using AutoMapper;
using ECommerce.Api.Orders.DataBase;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext _dbContext;
        private readonly ILogger<IOrdersProvider> _logger;
        private readonly IMapper _mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<IOrdersProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (_dbContext.Orders.Any() is false)
            {
                _dbContext
                    .Orders
                    .Add(new Entities.Order()
                    {
                        Id = 1,
                        CustomerId = 1,
                        OrderDate = DateTime.Now,
                        Total = 500,
                        Items = new List<Entities.OrderItem>()
                        {
                            new Entities.OrderItem()
                            {
                                Id=1,
                                ProductId = 1,
                                Quantity = 1,
                                UnitPrice = 1
                            },
                        }
                    });

                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await _dbContext
                    .Orders
                    .Where(x => x.CustomerId == customerId)
                    .Include(x => x.Items)
                    .ToListAsync();

                if (orders != null && orders.Any())
                {
                    var result = _mapper.Map<IEnumerable<Entities.Order>, IEnumerable<Models.Order>>(orders);

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
