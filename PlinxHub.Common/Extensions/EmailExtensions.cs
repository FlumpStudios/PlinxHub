using PlinxHub.Common.Models.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlinxHub.Common.Extensions
{
    public static class EmailExtensions
    {
        public static EmailTemplate ReplaceOrderNumber(
            this EmailTemplate e,
            string orderNumber)
        {
            return new EmailTemplate
            {
                Content = e.Content.Replace("{{ORDER}}", orderNumber),
                Subject = e.Subject.Replace("{{ORDER}}", orderNumber),
                EmailTemplatesId = e.EmailTemplatesId
            };
        }
    }
}
