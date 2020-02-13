// using System;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.UI;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using PlinxHub.Common.Data;

// [assembly: HostingStartup(typeof(PlinxHub.Areas.Identity.IdentityHostingStartup))]
// namespace PlinxHub.Areas.Identity
// {
//     public class IdentityHostingStartup : IHostingStartup
//     {
//         public void Configure(IWebHostBuilder builder)
//         {
//             builder.ConfigureServices((context, services) => {
//                 services.AddDbContext<PlinxHubContext>(options =>
//                     options.UseSqlServer(
//                         context.Configuration.GetConnectionString("DefaultConnection")));

//                 services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
//                     .AddEntityFrameworkStores<PlinxHubContext>();
//             });
//         }
//     }
// }