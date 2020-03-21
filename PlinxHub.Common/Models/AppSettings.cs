namespace PlinxHub.Common.Models
{
    public class AppSettings
    {
        public bool UseInMemDB { get; set; }
        public bool EnableSocialLogins { get; set; }        
        public Emailing Emailing { get; set; }
    }

    public class Emailing
    {
        public string SenderAddress { get; set; }
        public string SenderName { get; set; }
        public bool SendConfirmationEmails { get; set; }
        public string ContactEmail { get; set; }
    }
}
