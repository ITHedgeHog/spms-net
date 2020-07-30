using Microsoft.AspNetCore.Http;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.WebShared.Infrastructure.Extensions;

namespace SPMS.WebShared.Infrastructure.Services
{
    public class TenantAccessor<T> : ITenantAccessor<T> where T : TenantDto
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public T Instance => HttpContextExtensions.GetTenant<T>(_httpContextAccessor.HttpContext);
    }
}
