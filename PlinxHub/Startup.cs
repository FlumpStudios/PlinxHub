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
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;
using PlinxHub.Common.Crypto;
using CryptoLib;
using FiLogger.Service.Services;
using System.Threading.Tasks;

namespace PlinxHub
{
    /// <summary>
    /// startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Config prop
        /// </summary>
        /// <value></value>
        public IConfiguration Configuration { get; }

        private AppSecrets _secrets;
        
        private AppSettings _settings;

        /// <summary>
        /// Startup method
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configure services method
        /// </summary>
        /// <param name="services"></param>
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

            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
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
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Plinx Orders", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddCryptoManager(
               _secrets.EncryptionCipher.InputString,
               _secrets.EncryptionCipher.Salt);

            services.AddTransient<IEmailRepository, EmailRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IApiKeyGen, ApiKeyGen>();
            MapperConfiguration config = MapperConfig.Get();
            services.AddSingleton<IMapper>(new Mapper(config));
        }

        /// <summary>
        /// Configure method
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
    /// /// <param name="serviceProvider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
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
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Plinx Orders");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            CreateRoles(serviceProvider).GetAwaiter().GetResult();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            IdentityResult roleResult;

            foreach (var roleName in _secrets.Authorisation.Roles)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var suEmail = _secrets.Authorisation.SuperUser.Email;
            var suPwd = _secrets.Authorisation.SuperUser.Password;

            var poweruser = new IdentityUser
            {
                Email = suEmail,
                UserName = suEmail
            };

            var _user = await UserManager.FindByEmailAsync(suEmail);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, suPwd);
                if (createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }
        }
    }
}
