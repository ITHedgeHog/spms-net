using Microsoft.AspNetCore.Http;
using SPMS.Application.Dtos;
using SPMS.Common;

namespace SPMS.WebShared.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions to HttpContext to make multi-tenancy easier to use
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Returns the current tenant
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static T GetTenant<T>(this HttpContext context) where T : TenantDto
        {
            if (!context.Items.ContainsKey(StaticValues.HttpContextTenantKey))
                return null;
            return context.Items[StaticValues.HttpContextTenantKey] as T;
        }

        /// <summary>
        /// Returns the current Tenant
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static TenantDto GetTenant(this HttpContext context)
        {
            return context.GetTenant<TenantDto>();
        }
    }
}