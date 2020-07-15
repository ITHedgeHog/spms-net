using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Web.Extensions;

namespace SPMS.Web.Service
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
