using ECommerce.Services.Order.Application.Consumers;
using ECommerce.Services.Order.Application.Mapping;
using ECommerce.Services.Order.Infrastructure;
using ECommerce.Shared.Services;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerce.Services.Order.API
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
            services.AddDbContext<OrderDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"), configure =>
                {
                    configure.MigrationsAssembly("ECommerce.Services.Order.Infrastructure");
                });
            });

            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateOrderMessageCommandConsumer>();
                x.AddConsumer<CourseNameChangedEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    //Port : 5672
                    cfg.Host(Configuration["RabbitMQUrl"], "/", host =>
                    {
                        host.Username("root");
                        host.Password("123456789");
                    });

                    cfg.ReceiveEndpoint("create-order-service",e => {
                        e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("course-name-changed-event-order-service", e =>
                    {
                        e.ConfigureConsumer<CourseNameChangedEventConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();

            services.AddMediatR(typeof(CustomMapping).Assembly);
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();

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
                    options.Audience = "resource_order";
                    options.RequireHttpsMetadata = false;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce.Services.Order.API", Version = "v1" });
            });

            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.Services.Order.API v1"));
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
