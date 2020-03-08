using Microsoft.EntityFrameworkCore;
using PlinxHub.Common.Contracts;
using PlinxHub.Common.Enumerations;
using PlinxHub.Common.Models.ApiKeys;
using PlinxHub.Common.Models.Email;
using PlinxHub.Common.Models.Orders;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlinxHub.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Order> Order { get; set; }

        public DbSet<ApiKey> ApiKey { get; set; }

        public DbSet<EmailTemplate> EmailTemplate { get; set; }

        public DbSet<Status> Status { get; set; }

        public override int SaveChanges()
        {
            RunSaveChangeOverrides();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            RunSaveChangeOverrides();
            return base.SaveChangesAsync();
        }

        private void RunSaveChangeOverrides()
        {
            var entries = ChangeTracker
                    .Entries()
                    .Where(e => e.Entity is IAuditable && (
                            e.State == EntityState.Added
                            || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((IAuditable)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((IAuditable)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailTemplate>().HasData(new EmailTemplate
            {
                EmailTemplatesId = EmailTemplateOptions.NewOrderAlert,
                Subject = "Order confirmation",
                Content = @"<p>Thank you very much for ordering your website through Plinx!</p>
                            <p>Your order is being processed and we will be in contact with you shortly</p>
                            <p>Your order reference is {{ORDER}}. If you have any questions, please do not hesitate to contact us at info@plinx-tech.co.uk"
            },
            new EmailTemplate
            {
                EmailTemplatesId = EmailTemplateOptions.OrderConfirmation,
                Content = "Someone has placed a new website order",
                Subject = "New order - {{ORDER}}"
            });

            modelBuilder.Entity<Status>().HasData(new Status { 
                StatusId = OrderStatus.ProcessingOrder,
                Name = "Processing Order"
            },
            new Status {
                StatusId = OrderStatus.InBuild,
                Name = "In Build"
            },
            new Status
            {
                StatusId = OrderStatus.Ready,
                Name = "Ready"
            },
            new Status
            {
                StatusId = OrderStatus.Live,
                Name = "Live"
            },
            new Status
            {
                StatusId = OrderStatus.Cancelled,
                Name = "Cancelled"
            },
            new Status
            {
                StatusId = OrderStatus.OnHold,
                Name = "On Hold"
            });
        }
    }
}
