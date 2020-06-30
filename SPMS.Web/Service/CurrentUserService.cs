using System.Linq;
using Microsoft.AspNetCore.Http;
using SPMS.Application.Common.Interfaces;

namespace SPMS.Web.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetAuthId()
        {
            if (_httpContext.HttpContext != null && _httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return _httpContext.HttpContext.User.Claims
                    .FirstOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                    ?.Value;
            }

            return string.Empty;
        }

        public bool IsAuthenticated()
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }

        public string GetName()
        {
            return _httpContext.HttpContext.User.Identity.Name;
        }

        public string GetEmail()
        {
            return _httpContext.HttpContext.User.Claims.First(x =>
                x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
        }
    }
}