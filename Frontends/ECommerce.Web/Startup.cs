using ECommerce.Shared.Services;
using ECommerce.Web.Handler;
using ECommerce.Web.Services;
using ECommerce.Web.Services.Interfaces;
using ECommerce.Web.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;

namespace ECommerce.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ServiceApiSettings>(Configuration.GetSection("ServiceApiSettings"));
            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));
            services.AddSingleton(sp =>
            {
                return sp.GetRequiredService<IOptions<ServiceApiSettings>>().Value;
            });
            services.AddSingleton(sp =>
            {
                return sp.GetRequiredService<IOptions<ClientSettings>>().Value;
            });
            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddScoped<IClientCredentialTokenService, ClientCredentialTokenService>();
            services.AddScoped<IPhotoStockService, PhotoStockService>();
            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            services.AddScoped<ClientCredentialTokenHandler>();
            services.AddAccessTokenManagement();
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddHttpClient<IIdentityService, IdentityService>();
            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<ICatalogService, CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}{serviceApiSettings.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}{serviceApiSettings.PhotoStock.Path}");
            });

            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();


            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
                {
                    opts.LoginPath = "/Auth/SignIn";
                    opts.ExpireTimeSpan = TimeSpan.FromDays(60);
                    opts.SlidingExpiration = true;
                    opts.Cookie.Name = "ECommerceWebCookie";
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
