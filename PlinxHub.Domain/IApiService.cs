using PlinxHub.Common.Models.ApiKeys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlinxHub.Service
{
   public interface IApiService
    {
        ApiKey AddApiKey(ApiKey apiKey);
        Task<bool> ApiExists(string apiKey);
        Task<Guid> GetUserFromApiKey(string apiKey);
    }
}