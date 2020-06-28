﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPMS.Application.Common.Interfaces;

namespace SPMS.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SpmsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SpmsContextSql")), ServiceLifetime.Transient);

            services.AddScoped<ISpmsContext>(provider => provider.GetService<SpmsContext>());

            return services;
        }
    }
}