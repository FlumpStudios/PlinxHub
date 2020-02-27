using PlinxHub.Common.Models.Email;
using PlinxHub.Infrastructure.Data;
using System.Threading.Tasks;
using PlinxHub.Common.Enumerations;
using System;

namespace PlinxHub.Infrastructure.Repositories
{
    public class EmailRepository : IDisposable, IEmailRepository
    {
        private readonly ApplicationDbContext _context;

        public EmailRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmailTemplate> Get(EmailTemplateOptions id) =>
            await _context.EmailTemplate.FindAsync(id);

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
    }
}
