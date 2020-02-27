using PlinxHub.Common.Enumerations;
using PlinxHub.Common.Models.Email;
using System.Threading.Tasks;

namespace PlinxHub.Infrastructure.Repositories
{
    public interface IEmailRepository
    {
        void Dispose();
        Task<EmailTemplate> Get(EmailTemplateOptions id);
    }
}