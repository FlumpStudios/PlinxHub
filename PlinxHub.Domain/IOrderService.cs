using PlinxHub.Common.Models.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlinxHub.Service
{
    public interface IOrderService
    {
        Task<Order> GenerateNewOrder(Order order);
        Task<Order> GetOrder(int id);
        Task<IEnumerable<Order>> GetOrdersByUser(Guid UserId);
    }
}