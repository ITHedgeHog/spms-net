using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SPMS.Web.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetAuthId()
        {
            return _httpContext.HttpContext.User.Claims
                .First(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }

        public string GetName()
        {
            return _httpContext.HttpContext.User.Identity.Name;
        }

        public bool IsPlayer()
        {
            return _httpContext.HttpContext.User.IsInRole(StaticValues.PlayerRole);
        }

        public bool IsAdmin()
        {
            return _httpContext.HttpContext.User.IsInRole(StaticValues.AdminRole);
        }
    };
    public interface IUserService
    {
        string GetAuthId();
        string GetName();
        bool IsPlayer();
        bool IsAdmin();
    }
}
