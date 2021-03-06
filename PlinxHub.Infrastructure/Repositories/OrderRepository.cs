﻿using Microsoft.EntityFrameworkCore;
using PlinxHub.Common.Enumerations;
using PlinxHub.Common.Extensions;
using PlinxHub.Common.Models.ApiKeys;
using PlinxHub.Common.Models.Filters;
using PlinxHub.Common.Models.Orders;
using PlinxHub.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace PlinxHub.Infrastructure.Repositories
{
    public class OrderRepository : IDisposable, IOrderRepository
    {
        /// <summary>
        /// Defualt take count for order
        /// </summary>
        /// TODO: Move to app settings
        const int DEFAULT_TAKE_COUNT = 10;

        private readonly ApplicationDbContext _context;

        public OrderRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> Get(OrderFilters filters)
        {
            return await Query(filters)
            .AsNoTracking()
            .Include(x => x.Status)
            .ToListAsync();
        }

        public async Task<Order> Get(Guid id) =>
            await _context.Order
                .AsNoTracking()
                .Include(x => x.Status)
                .SingleAsync(x => x.OrderNumber == id);

        public async Task<IEnumerable<Order>> GetByUser(
            Guid UserID, 
            OrderFilters filters) =>
                await Query(filters)
                .AsNoTracking()
                .Include(x => x.Status)
                .Where(x => x.UserId == UserID)
                .ToListAsync();

        public async Task<Order> GetByApiKey(string apiKey) =>
           await _context.ApiKey
            .Where(x => x.Key == apiKey)
            .Select(x =>  x.Order)
            .SingleAsync();

        public async Task<string> GetApiKeyByOrder(Guid orderNumber) =>
            await _context.ApiKey.Where(x => x.OrderNumber == orderNumber).Select(x => x.Key).SingleAsync();

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
   
        private IQueryable<Order> Query(OrderFilters filters) =>
            _context.Order.Where(x => x.StatusId == (OrderStatus)filters.StatusId || filters.StatusId == 0)
                .Where(x => x.OrderNumber.ToString().Contains(filters.OrderNumber.NullToEmpty()))
                .Where(x => x.CompanyName.Contains(filters.CompanyName.NullToEmpty()))
                .Where(x => string.Concat(x.FirstName, x.Surname).Contains(filters.Name.NullToEmpty().RemoveWhiteSpace()))
                .Where(x => x.TemplateNumber == filters.TemplateNumber || filters.TemplateNumber == 0)
                .Where(x => x.EmailAddress.Contains(filters.EmailAddress.NullToEmpty()))
                .OrderBy(string.Concat(filters.OrderBy ?? $"{nameof(Order.CreatedDate)} descending", " ", filters.Decending ? "descending" : string.Empty))
                .Skip(filters.Skip)
                .Take(filters.Take > 0 ? filters.Take : DEFAULT_TAKE_COUNT);

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
