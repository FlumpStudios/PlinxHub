using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlinxHub.Common.Models;
using PlinxHub.Common.Models.Orders;

namespace PlinxHub.Common.Data
{
    public class PlinxHubContext : DbContext
    {
        public PlinxHubContext(DbContextOptions<PlinxHubContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
