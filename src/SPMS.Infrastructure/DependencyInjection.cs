using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPMS.Common;

namespace SPMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTime, MachineDateTime>();
            return services;
        }
    }
}
