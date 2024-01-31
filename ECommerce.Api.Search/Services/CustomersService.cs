using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ICustomersService> _logger;
        public CustomersService(IHttpClientFactory httpClientFactory, ILogger<ICustomersService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int customerId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customers/{customerId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();

                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var result = JsonSerializer.Deserialize<dynamic> (content, options);

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
