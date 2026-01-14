using Blog.Application.Abstractions;
using Blog.Application.Behaviors;
using Blog.Application.Common.Interfaces;
using BlogApi.Application;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddSignalR();          
            services.AddMemoryCache();
            services.AddScoped<ICacheInvalidationService, CacheInvalidationService>();    
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AutomapperProfile>();
            });

            return services;
        }
    }
}