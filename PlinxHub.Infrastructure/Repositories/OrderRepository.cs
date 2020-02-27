using Microsoft.EntityFrameworkCore;
using PlinxHub.Common.Models.ApiKeys;
using PlinxHub.Common.Models.Orders;
using PlinxHub.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlinxHub.Infrastructure.Repositories
{
    public class OrderRepository : IDisposable, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> Get() =>
            await _context.Order
                .AsNoTracking()
                .ToListAsync();

        public async Task<Order> Get(Guid id) =>
            await _context.Order
                .AsNoTracking()
                .SingleAsync(x => x.OrderNumber == id);

        public async Task<IEnumerable<Order>> GetByUser(Guid UserID) =>
            await _context.Order
            .AsNoTracking()
            .Where(x => x.UserId == UserID)
            .ToListAsync();

        public async Task<Order> GetByApiKey(string apiKey) =>
           await _context.ApiKey
            .Where(x => x.Key == apiKey)
            .Select(x =>  x.Order)
            .SingleAsync();
        

        public void Update(Order order) =>
            _context.Entry(order).State = EntityState.Modified;

        public Order Create(Order order) =>
            _context.Order.Add(order).Entity;

        public ApiKey CreateApiKey(ApiKey apiKey) =>
            _context.ApiKey.Add(apiKey).Entity;

        public void Delete(int id) =>
            _context.Order.Remove(_context.Order.Find(id));

        public async Task<bool> Exists(Guid id) =>
            await _context.Order
            .AsNoTracking()
            .AnyAsync(x => x.OrderNumber == id);

        public void Save() =>
            _context.SaveChanges();

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
