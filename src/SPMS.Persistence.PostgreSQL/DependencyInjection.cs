using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPMS.Application.Common.Interfaces;

namespace SPMS.Persistence.PostgreSQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SpmsContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("SpmsContext")));
            try
            {
                services.AddScoped<ISpmsContext>(provider => provider.GetService<SpmsContext>());
            }
            catch (Exception ex)
            {
                var i = 1;
            }

            return services;
        }
    }
}