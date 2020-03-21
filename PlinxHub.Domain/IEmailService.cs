using PlinxHub.Common.Models.Email;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace FiLogger.Service.Services
{
    public interface IEmailService
    {
        Task<Response> Send(string from, string to, string subject, string message);
        Task<Response> Send(Email email);
        Task<Response> Send(Email email, params Attachment[] attachments);
        Task<Response> Send(Email email, params string[] attachments);
    }
}