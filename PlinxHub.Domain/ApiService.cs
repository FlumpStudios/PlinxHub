using PlinxHub.Common.Models.ApiKeys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlinxHub.Service
{
    public class ApiService : IApiService
    {
        public Task<bool> ApiExists(string apiKey)
        {
            throw new NotImplementedException();
        }
        public Task<Guid> GetUserFromApiKey(string apiKey)
        {
            throw new NotImplementedException();
        }
        public ApiKey AddApiKey(ApiKey apiKey)
        {
            throw new NotImplementedException();
        }
    }
}