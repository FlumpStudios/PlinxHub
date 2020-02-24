using PlinxHub.Common.Crypto;
using PlinxHub.Common.Models.ApiKeys;
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
        private readonly IApiKeyGen _apiKeyGen;

        public OrderService(
            IOrderRepository orderRepository,
            IApiKeyGen apiKeyGen)
        {
            _orderRepository = orderRepository;
            _apiKeyGen = apiKeyGen;
        }

        public async Task<Order> GetOrder(Guid id) =>
            await _orderRepository.Get(id);

        public async Task<Order> GenerateNewOrder(Order order)
        {
            var newOrderNumber = Guid.NewGuid();

            order.OrderNumber = newOrderNumber;

            var response = _orderRepository.Create(order);
            var newApiKey = new ApiKey
            {
                Key = _apiKeyGen.CreateNew,
                OrderNumber = newOrderNumber
            };

            _orderRepository.CreateApiKey(newApiKey);

            await _orderRepository.SaveAsync();
            return response;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUser(Guid UserId)
        {
            if (UserId == Guid.Empty) throw new ArgumentOutOfRangeException(nameof(UserId));

            return await _orderRepository.GetByUser(UserId);
        }

        public async Task<Order> GetOrderByApiKey(string apiKey)
        {
            return await _orderRepository.GetByApiKey(apiKey);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            if (!await _orderRepository.Exists(order.OrderNumber)) return false;

            _orderRepository.Update(order);
            await _orderRepository.SaveAsync();
            return true;
        }
    }
}

