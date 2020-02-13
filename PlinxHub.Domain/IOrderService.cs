using PlinxHub.Common.Models.Orders;
using System.Threading.Tasks;

namespace PlinxHub.Service
{
    public interface IOrderService
    {
        Task<Order> GenerateNewOrder(Order order);
    }
}