using Microsoft.AspNetCore.Http;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Web.Infrastructure.Extensions;

namespace SPMS.Web.Infrastructure.Services
{
    public class TenantAccessor<T> : ITenantAccessor<T> where T : TenantDto
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public T Instance => _httpContextAccessor.HttpContext.GetTenant<T>();
    }
}
