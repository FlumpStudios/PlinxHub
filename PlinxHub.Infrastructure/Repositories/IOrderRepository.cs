using PlinxHub.Common.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlinxHub.Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        void Delete(int id);
        void Dispose();
        Task<IEnumerable<Order>> Get();
        Task<Order> Get(int id);
        Task<bool> Exists(int id);
        Order Create(Order order);
        void Update(Order order);
        Task SaveAsync();
        void Save();
    }
}