namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Models.Customer customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
