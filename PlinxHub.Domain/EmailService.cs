using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using System.IO;
using PlinxHub.Common.Models;
using PlinxHub.Common.Models.Email;

namespace FiLogger.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _SendGridKey;
        private readonly IOptions<AppSettings> _SenderDetails;

        public EmailService(
            IOptions<AppSecrets> secrets,
            IOptions<AppSettings> appSettings)
        {
            _SendGridKey = secrets.Value.SendGridApiKey;
            _SenderDetails = appSettings;
        }

        /// <summary>
        /// Send message without attachement
        /// </summary>
        /// <param name="email"></param>        
        public async Task<Response> Send(Email email)
        {
            BuildMessage(
                email,
                out SendGridClient client,
                out SendGridMessage msg);

            return await client.SendEmailAsync(msg);
        }

        /// <summary>
        /// Send message without template
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        public async Task<Response> Send(
            string from,
            string to,
            string subject,
            string message)
        {
            var msg = MailHelper.CreateSingleEmail(
                            new EmailAddress(from),
                           new EmailAddress(to),
                           subject,
                           message,
                           RemoveHtml(message));

            var client = new SendGridClient(_SendGridKey);
            return await client.SendEmailAsync(msg);
        }

        /// <summary>
        /// Send email and use string params list of file locations to add attachments and generate attachment names based on filename
        /// </summary>
        /// <param name="email"></param>
        /// <param name="attachments"></param>
        public async Task<Response> Send(
            Email email,
            params string[] attachments)
        {
            BuildMessage(
                email,
                out SendGridClient client,
                out SendGridMessage msg);

            foreach (var attachment in attachments)
            {
                var fileName = attachment.Split("\\").LastOrDefault();
                var bytes = File.ReadAllBytes(attachment);
                var file = Convert.ToBase64String(bytes);

                msg.AddAttachment(
                    fileName,
                    file);
            }

            return await client.SendEmailAsync(msg);
        }

        /// <summary>
        /// Send email and use params list of SendGrid attachment object to add attachments.
        /// </summary>
        /// <remarks>
        /// Attachment content must be in base 64 format
        /// </remarks>
        /// <param name="email"></param>
        /// <param name="attachments"></param>        
        public async Task<Response> Send(
            Email email,
            params Attachment[] attachments)
        {
            BuildMessage(
                email,
                out SendGridClient client,
                out SendGridMessage msg);

            foreach (var attachment in attachments)
                msg.AddAttachment(attachment);

            return await client.SendEmailAsync(msg);
        }

        #region Private helper methods
        private void BuildMessage(
            Email email,
            out SendGridClient client,
            out SendGridMessage msg)
        {
            client = new SendGridClient(_SendGridKey);
            var from = new EmailAddress(
                _SenderDetails.Value.Emailing.SenderAddress,
                _SenderDetails.Value.Emailing.SenderName);

            var plainTextContent = RemoveHtml(email.EmailTemplate.Content);

            msg = MailHelper.CreateSingleEmailToMultipleRecipients(
                from,
                email.Recipients.ToList(),
                email.EmailTemplate.Subject,
                plainTextContent,
                email.EmailTemplate.Content);
        }

        private string RemoveHtml(string html) =>
            Regex.Replace(html, "<.*?>", String.Empty);
        #endregion
    }
}
