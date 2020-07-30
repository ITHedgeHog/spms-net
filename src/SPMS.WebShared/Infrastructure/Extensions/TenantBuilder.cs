using SPMS.WebShared.Infrastructure.Services;

namespace SPMS.WebShared.Infrastructure.Extensions
{
    /// <summary>
    /// Tenant Builder.
    /// </summary>
    /// <typeparam name="T">Tenant.</typeparam>
    public class TenantBuilder<T>
        where T : TenantDto
    {
        private readonly IServiceCollection _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantBuilder{T}"/> class.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        public TenantBuilder(IServiceCollection services)
        {
            _services = services;
        }



        /// <summary>
        /// Register the tenant resolver implementation.
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
        /// Register the tenant store implementation.
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