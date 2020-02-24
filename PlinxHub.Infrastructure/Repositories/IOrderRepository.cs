using System;
using PlinxHub.Common.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlinxHub.Common.Models.ApiKeys;

namespace PlinxHub.Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        void Delete(int id);
        void Dispose();
        Task<IEnumerable<Order>> Get();
        Task<IEnumerable<Order>> GetByUser(Guid UserID);
        Task<Order> Get(Guid id);
        Task<bool> Exists(Guid id);
        Order Create(Order order);
        void Update(Order order);
        Task SaveAsync();
        void Save();
        ApiKey CreateApiKey(ApiKey apiKey);
        Task<Order> GetByApiKey(string apiKey);
    }
}