﻿namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        public Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
