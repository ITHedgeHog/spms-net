using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SPMS.Application.Common.Interfaces;

namespace SPMS.WebShared.Infrastructure.Services
{
    public class TenantResolver : ITenantResolver
    {
        private readonly string _host;

        public TenantResolver(IHttpContextAccessor context)
        {
            _host = context.HttpContext.Request.Host.Host;
        }

        public string GetHost()
        {
            return _host;
        }

        public async Task<string> GetHostAsync()
        {
            return await Task.FromResult(_host);
        }
    }
}
