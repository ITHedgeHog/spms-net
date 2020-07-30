using Microsoft.Extensions.DependencyInjection;
using SPMS.Application.Dtos;

namespace SPMS.WebShared.Infrastructure.Extensions
{
    /// <summary>
    /// ServiceCollectionExtensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the services (T tenant class).
        /// </summary>
        /// <typeparam name="T">Tenant Type.</typeparam>
        /// <param name="services">Service Collection.</param>
        /// <returns>Tenant Builder.</returns>
        public static TenantBuilder<T> AddSpmsMultiTenancy<T>(this IServiceCollection services)
            where T : TenantDto
            => new TenantBuilder<T>(services);

        /// <summary>
        /// Add the services (default tenant class).
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>Tenant Builder.</returns>
        public static TenantBuilder<TenantDto> AddSpmsMultiTenancy(this IServiceCollection services)
            => new TenantBuilder<TenantDto>(services);
    }
}
