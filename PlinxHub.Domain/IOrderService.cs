using PlinxHub.Common.Models.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlinxHub.Service
{
    public interface IOrderService
    {
        Task<Order> GenerateNewOrder(Order order);
        Task<Order> GetOrder(Guid id);
        Task<IEnumerable<Order>> GetOrdersByUser(Guid UserId);
        Task<bool> UpdateOrder(Order order);
        Task<Order> GetOrderByApiKey(string apiKey);
    }
}