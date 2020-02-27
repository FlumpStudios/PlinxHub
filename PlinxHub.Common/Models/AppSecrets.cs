namespace PlinxHub.Common.Models
{
    public class AppSecrets
    {
        public Authentication Authentication { get; set; }
        public EncryptionCipher EncryptionCipher { get; set; }

        public string SendGridApiKey { get; set; }
    }
    public class Authentication
    {
        public SocialDetails Twitter { get; set; }
        public SocialDetails Facebook { get; set; }
        public SocialDetails LinkedIn { get; set; }
    }

    public class SocialDetails
    {
        public string AppID { get; set; }
        public string AppSecret { get; set; }
    }

    public class EncryptionCipher
    {
        public string InputString { get; set; }

        public string Salt { get; set; }
    }
}
