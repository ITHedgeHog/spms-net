using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;

namespace SPMS.Web.Service
{
    /// <summary>
    /// Tenant access service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TenantAccessService<T> where T : TenantDto
    {
        private readonly ITenantResolver _tenantResolutionStrategy;
        private readonly ITenantProvider<T> _tenantProvider;

        public TenantAccessService(ITenantResolver tenantResolutionStrategy, ITenantProvider<T> tenantProvider)
        {
            _tenantResolutionStrategy = tenantResolutionStrategy;
            _tenantProvider = tenantProvider;
        }

        /// <summary>
        /// Get the current tenant
        /// </summary>
        /// <returns></returns>
        public async Task<T> GetTenantAsync()
        {
            var tenantIdentifier = await _tenantResolutionStrategy.GetHostAsync();
            return await _tenantProvider.GetTenantAsync(tenantIdentifier, cancellationToken:CancellationToken.None);
        }
    }
}
