using Microsoft.EntityFrameworkCore;
using PlinxHub.Common.Enumerations;
using PlinxHub.Common.Models.ApiKeys;
using PlinxHub.Common.Models.Email;
using PlinxHub.Common.Models.Orders;


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
        }
    }
}
