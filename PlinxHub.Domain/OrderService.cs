using PlinxHub.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlinxHub.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public GenerateNewOrder(OrderRepository order)
        {
            throw new NotImplementedException();
        }
    }
}
