using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using PlinxHub.Common.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace PlinxHub.API.Emailing
{
    /// <summary>
    /// Email sender class
    /// </summary>
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// Email sender constructor message
        /// </summary>
        /// <param name="optionsAccessor"></param>
        /// <param name="optionsAccessorSettings"></param>
        public EmailSender(
            IOptions<AppSecrets> optionsAccessor,
            IOptions<AppSettings> optionsAccessorSettings)
        {
            Secrets = optionsAccessor.Value;
            Settings = optionsAccessorSettings.Value;
        }

        /// <summary>
        /// secrets props
        /// </summary>
        public AppSecrets Secrets { get; }

        /// <summary>
        /// settings prop
        /// </summary>
        public AppSettings Settings { get; }

        /// <summary>
        /// Send email method
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Secrets.SendGridApiKey, subject, message, email);
        }

        /// <summary>
        /// Execute method for email sender
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task Execute(
            string apiKey,
            string subject,
            string message,
            string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Settings.Emailing.SenderAddress, Settings.Emailing.SenderName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
