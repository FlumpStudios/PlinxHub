using PlinxHub.Common.Models;
using System;
using System.Collections.Generic;

namespace PlinxHub.Common.Extensions
{
    public static class SecretsExtensions
    {
        public static AppSecrets MapAppSecretsToEnvVariables(this AppSecrets _secrets)
        {
            if (_secrets == null) throw new ArgumentNullException(nameof(_secrets));

            _secrets.Authentication = new Authentication
            {
                Facebook = new SocialDetails
                {
                    AppID = Environment.GetEnvironmentVariable("FACEBOOK_APPID"),
                    AppSecret = Environment.GetEnvironmentVariable("FACEBOOK_APPSECRET")
                },
                LinkedIn = new SocialDetails
                {
                    AppID = Environment.GetEnvironmentVariable("LINKEDIN_APPID "),
                    AppSecret = Environment.GetEnvironmentVariable("LINKEDIN_APPSECRET ")
                },
                Twitter = new SocialDetails
                {
                    AppID = Environment.GetEnvironmentVariable("TWITTER_APPID "),
                    AppSecret = Environment.GetEnvironmentVariable("TWITTER_APPSECRET ")
                }
            };
            _secrets.Authorisation = new Authorisation
            {
                Roles = new List<string>
                    {
                        Environment.GetEnvironmentVariable("SUPER_USER_ROLE")
                    },
                SuperUser = new SuperUser
                {
                    Email = Environment.GetEnvironmentVariable("SUPER_USER_EMAIL"),
                    Password = Environment.GetEnvironmentVariable("SUPER_USER_PASSWORD ")
                }
            };
            _secrets.SendGridApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            

            return _secrets;
        }
    }
}
