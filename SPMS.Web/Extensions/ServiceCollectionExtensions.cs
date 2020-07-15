using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Web.Service;

namespace SPMS.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static TenantBuilder<T> AddSpmsMultiTenancy<T>(this IServiceCollection services) where T : TenantDto
            => new TenantBuilder<T>(services);

        /// <summary>
        /// Add the services (default tenant class)
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static TenantBuilder<TenantDto> AddSpmsMultiTenancy(this IServiceCollection services)
            => new TenantBuilder<TenantDto>(services);
    }

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
            _services.AddTransient<TenantAccessService<T>>();
            _services.AddTransient<ICurrentUserService, CurrentUserService>();
            _services.AddTransient<IApplicationVersion, ApplicationVersion>();
            _services.AddHttpContextAccessor();

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
