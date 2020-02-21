using PlinxHub.Common.Models.Orders;
using PlinxHub.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlinxHub.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetOrder(int id) => 
            await _orderRepository.Get(id);

        public async Task<Order> GenerateNewOrder(Order order)
        {
            var response = _orderRepository.Create(order);
            await _orderRepository.SaveAsync();
            return response;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUser(Guid UserId)
        {
            if (UserId == Guid.Empty) throw new ArgumentOutOfRangeException(nameof(UserId));

            return await _orderRepository.GetByUser(UserId);
        }

        public async Task UpdateOrder(Order order)
        {
            _orderRepository.Update(order);
            await _orderRepository.SaveAsync();
        }
    }
}
