﻿using System.Linq;
using Microsoft.AspNetCore.Http;
using SPMS.Application.Common.Interfaces;

namespace SPMS.Web.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContext _httpContext;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _httpContext = contextAccessor.HttpContext;
        }

        public string GetAuthId()
        {

            if (_httpContext != null && _httpContext.User.Identity.IsAuthenticated)
            {
                return _httpContext.User.Claims
                    .FirstOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                    ?.Value;
            }

            return string.Empty;
        }

        public bool IsAuthenticated()
        {
            return _httpContext.User.Identity.IsAuthenticated;
        }

        public string GetName()
        {
            return _httpContext.User.Identity.Name;

        }

        public string GetEmail()
        {
            return _httpContext.User.Claims.First(x =>
                x.Type == "emails").Value;
        }

        public bool IsNew()
        {
            return bool.TryParse(_httpContext.User.Claims.FirstOrDefault(x => x.Type == "newUser")?.Value, out var isNewClaim) && isNewClaim;
        }

        public string GetFirstname()
        {
            return _httpContext.User.Claims.First(x =>
                x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value;
        }

        public string GetSurname()
        {
            return _httpContext.User.Claims.First(x =>
                x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").Value;
        }
    }
}