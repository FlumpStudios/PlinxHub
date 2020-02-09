using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using PlinxHub.Common.Models;
using PlinxHub.Infrastructure.Data;
using PlinxHub.Infrastructure.Repositories;
using PlinxHub.Ioc.Config;
using PlinxHub.Service;

namespace PlinxHub
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private AppSecrets _secrets;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var secrets = Configuration;

            services.Configure<AppSecrets>(secrets);
            _secrets = secrets.Get<AppSecrets>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddRazorPages();
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId =_secrets.Authentication.Facebook.AppID;
                facebookOptions.AppSecret = _secrets.Authentication.Facebook.AppSecret;
            }).AddTwitter(o => {
                o.ConsumerKey = _secrets.Authentication.Twitter.AppID;
                o.ConsumerSecret = _secrets.Authentication.Twitter.AppSecret;
            }).AddLinkedIn(o => {
                o.ClientId = _secrets.Authentication.LinkedIn.AppID;
                o.ClientSecret = _secrets.Authentication.LinkedIn.AppID;
            });

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderService, OrderService>();
            MapperConfiguration config = MapperConfig.Get();
            services.AddSingleton<IMapper>(new Mapper(config));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
