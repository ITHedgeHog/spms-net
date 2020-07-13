using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SPMS.Application.Common.Interfaces;

namespace SPMS.Web.Service
{
    public class HostProvider : IHostProvider
    {
        private readonly string host;

        public HostProvider(IHttpContextAccessor context)
        {
            host = context.HttpContext.Request.Host.Host;
        }

        public string GetHost()
        {
            return host;
        }
    }
}
