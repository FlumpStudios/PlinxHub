using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using PlinxHub.Service;

namespace PlinxHub.MiddleWare
{
    /// <summary>
    /// Middleware class for varifying api keys
    /// </summary>
    public class UserKeyValidatorsMiddleware
    {
        private readonly RequestDelegate _next;
        private IApiService ContactsRepo { get; set; }

        /// <summary>
        /// user Key Validation Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="_repo"></param>
        public UserKeyValidatorsMiddleware(RequestDelegate next, IApiService _repo)
        {
            _next = next;
            ContactsRepo = _repo;
        }

        /// <summary>
        /// Middleware invoke method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.Keys.Contains("user-key"))
            {
                context.Response.StatusCode = 400; //Bad Request                
                await context.Response.WriteAsync("User Key is missing");
                return;
            }
            else
            {
                if(! await ContactsRepo.ApiExists(context.Request.Headers["user-key"]))
                {
                    context.Response.StatusCode = 401; //UnAuthorized
                    await context.Response.WriteAsync("Invalid User Key");
                    return;
                }
            }

            await _next.Invoke(context);
        }

    }

    #region ExtensionMethod
    /// <summary>
    /// user validation extension
    /// </summary>
    public static class UserKeyValidatorsExtension
    {
        /// <summary>
        /// ApplyUserKeyValidation constructor
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder ApplyUserKeyValidation(this IApplicationBuilder app)
        {
            app.UseMiddleware<UserKeyValidatorsMiddleware>();
            return app;
        }
    }
    #endregion
}