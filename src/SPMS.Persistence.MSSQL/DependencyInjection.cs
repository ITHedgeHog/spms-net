using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPMS.Application.Common.Interfaces;

namespace SPMS.Persistence.MSSQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SpmsContextSql");
            services.AddDbContext<SpmsContext>(options =>
                options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            }), ServiceLifetime.Transient);

            services.AddScoped<ISpmsContext>(provider => provider.GetService<SpmsContext>());

            return services;
        }
    }
}