using System;
using System.Collections.Generic;
using System.Text;
using SendGrid.Helpers.Mail;

namespace PlinxHub.Common.Models.Email
{
    public class Email
    {
        public IEnumerable<EmailAddress> Recipients { get; set; }
        public EmailTemplate EmailTemplate { get; set; }
    }
}
