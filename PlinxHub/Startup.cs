using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
using PlinxHub.Common.Data;

namespace PlinxHub
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private AppSecrets _secrets;
        
        private AppSettings _settings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSecrets>(Configuration);
            services.Configure<AppSettings>(Configuration);

            _secrets = Configuration.Get<AppSecrets>();
            _settings = Configuration.Get<AppSettings>();

            if (_settings.UseInMemDB)
            {
                services.AddDbContext<PlinxHubContext>(options =>
                    options.UseInMemoryDatabase(databaseName:"Identity"));
                    
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "PlinxHubDB"));

            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));

                services.AddDbContext<PlinxHubContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddDefaultIdentity<IdentityUser>()
                        .AddEntityFrameworkStores<PlinxHubContext>();

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

            if(_settings.EnableSocialLogins)
            {
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
            }

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
