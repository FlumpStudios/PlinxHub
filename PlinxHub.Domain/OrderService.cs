using PlinxHub.Common.Models.Orders;
using PlinxHub.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace PlinxHub.Service
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GenerateNewOrder(Order order)
        {
            var response = _orderRepository.Create(order);
            await _orderRepository.SaveAsync();
            return response;
        }
    }
}
