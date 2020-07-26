using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;
using SPMS.Infrastructure.Services;

namespace SPMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddHttpClient();
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddScoped<IDiscordService, DiscordService>();
            return services;
        }
    }
}
