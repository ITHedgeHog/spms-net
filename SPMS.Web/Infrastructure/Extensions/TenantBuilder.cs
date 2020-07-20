using Microsoft.Extensions.DependencyInjection;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.BackgroundService;
using SPMS.Web.Infrastructure.Services;

namespace SPMS.Web.Infrastructure.Extensions
{
    public class TenantBuilder<T> where T : TenantDto
    {
        private readonly IServiceCollection _services;

        public TenantBuilder(IServiceCollection services)
        {
            _services = services;
        }



        /// <summary>
        /// Register the tenant resolver implementation
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public TenantBuilder<T> WithResolutionStrategy<V>(ServiceLifetime lifetime = ServiceLifetime.Transient) where V : class, ITenantResolver
        {
            _services.AddSingleton<RouteTranslator>();
            _services.AddTransient<TenantAccessService<T>>();
            _services.AddTransient(typeof(ITenantAccessor<T>), typeof(TenantAccessor<T>));
            _services.AddTransient<ICurrentUserService, CurrentUserService>();
            _services.AddTransient<IApplicationVersion, ApplicationVersion>();
            _services.AddHttpContextAccessor();

            _services.AddHostedService<PublishService>();

            _services.Add(ServiceDescriptor.Describe(typeof(ITenantResolver), typeof(V), lifetime));
            return this;
        }

        /// <summary>
        /// Register the tenant store implementation
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public TenantBuilder<T> WithStore<V>(ServiceLifetime lifetime = ServiceLifetime.Transient) where V : class, ITenantProvider<T>
        {
            _services.Add(ServiceDescriptor.Describe(typeof(ITenantProvider<T>), typeof(V), lifetime));
            return this;
        }
    }
}