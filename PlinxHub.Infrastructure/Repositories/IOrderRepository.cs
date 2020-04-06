using System;
using PlinxHub.Common.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlinxHub.Common.Models.ApiKeys;
using PlinxHub.Common.Models.Filters;

namespace PlinxHub.Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        void Delete(int id);
        void Dispose();
        Task<IEnumerable<Order>> Get(OrderFilters filters);
        Task<IEnumerable<Order>> GetByUser(Guid UserID, OrderFilters filters);
        Task<Order> Get(Guid id);
        Task<bool> Exists(Guid id);
        Order Create(Order order);
        void Update(Order order);
        Task SaveAsync();
        void Save();
        ApiKey CreateApiKey(ApiKey apiKey);
        Task<Order> GetByApiKey(string apiKey);
        Task<string> GetApiKeyByOrder(Guid orderNumber);
    }
}