using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PlinxHub.Common.Crypto
{
    public class ApiKeyGen : IApiKeyGen
    {
        public string CreateNew
        {
            get
            {
                var key = new byte[32];
                using (var generator = RandomNumberGenerator.Create())
                    generator.GetBytes(key);
                return Convert.ToBase64String(key);
            }
        }
    }
}
