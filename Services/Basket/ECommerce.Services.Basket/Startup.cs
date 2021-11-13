using ECommerce.Services.Basket.Services;
using ECommerce.Services.Basket.Settings;
using ECommerce.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerce.Services.Basket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));

            services.AddSingleton<IRedisSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<RedisSettings>>().Value;
            });

            services.AddSingleton<RedisService>(sp =>
            {
                var settings = sp.GetRequiredService<IRedisSettings>();
                var redis = new RedisService(settings);

                redis.Connect();

                return redis;
            });
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();

            services.AddScoped<IBasketService, BasketService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce.Services.Basket", Version = "v1" });
            });

            var requreAuthorizePolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build(); ;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter(requreAuthorizePolicy));
            });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["IdentityServerURL"];
                    options.Audience = "resource_basket";
                    options.RequireHttpsMetadata = false;
                });

            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.Services.Basket v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
