using PlinxHub.Common.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace PlinxHub.Common.Models.Email
{
    public class EmailTemplate
    {
        [Key]
        public EmailTemplateOptions EmailTemplatesId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
