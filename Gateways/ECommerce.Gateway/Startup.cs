using ECommerce.Gateway.DelegateHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ECommerce.Gateway
{
    public class Startup
    {
        IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<TokenExchangeDelegateHandler>();
            services
                .AddAuthentication()
                .AddJwtBearer("GatewayAuthenticationScheme",options => {
                    options.Authority = Configuration["IdentityServerURL"];
                    options.Audience = "resource_gateway";
                    options.RequireHttpsMetadata = false;
                });
            services
                .AddOcelot()
                .AddDelegatingHandler<TokenExchangeDelegateHandler>();
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            await app.UseOcelot();
        }
    }
}
