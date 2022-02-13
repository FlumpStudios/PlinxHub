using System.Collections.Generic;

namespace PlinxHub.Common.Models
{
    public class AppSecrets
    {
        public Authentication Authentication { get; set; }
        public string SendGridApiKey { get; set; }
        public Authorisation Authorisation { get; set; }
    }
    public class Authentication
    {
        public SocialDetails Twitter { get; set; }
        public SocialDetails Facebook { get; set; }
        public SocialDetails LinkedIn { get; set; }
        public SocialDetails Google { get; set; }
    }

    public class SocialDetails
    {
        public string AppID { get; set; }
        public string AppSecret { get; set; }
    }

    public class Authorisation
    {
        public IEnumerable<string> Roles { get; set; }
        public SuperUser SuperUser { get; set; }
    }

    public class SuperUser
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
