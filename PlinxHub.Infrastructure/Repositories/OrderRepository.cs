using Microsoft.EntityFrameworkCore;
using PlinxHub.Common.Data;
using PlinxHub.Common.Models.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlinxHub.Infrastructure.Repositories
{
    public class OrderRepository : IDisposable, IOrderRepository
    {
        private readonly PlinxHubContext _context;

        public OrderRepository(
            PlinxHubContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> Get() =>
            await _context.Order
                .AsNoTracking()
                .ToListAsync();

        public async Task<Order> Get(int id) =>
            await _context.Order
                .AsNoTracking()
                .SingleAsync(
                    x => x.OrderNumber == id);

        public void Update(Order order) =>
            _context.Entry(order).State = EntityState.Modified;

        public Order Create(Order order) =>
            _context.Order.Add(order).Entity;

        public void Delete(int id) =>
            _context.Order.Remove(_context.Order.Find(id));

        public async Task<bool> Exists(int id) =>
            await _context.Order
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
