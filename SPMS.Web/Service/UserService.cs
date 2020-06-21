using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SPMS.Web.Models;

namespace SPMS.Web.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly SpmsContext _context;

        public UserService(IHttpContextAccessor httpContext, SpmsContext context)
        {
            _httpContext = httpContext;
            _context = context;
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

        public string GetName()
        {
            return _httpContext.HttpContext.User.Identity.Name;
        }

        public bool IsPlayer()
        {
            if (_httpContext.HttpContext != null && !_httpContext.HttpContext.User.Identity.IsAuthenticated)
                return false;
            var user = GetPlayer();
            return user.Roles.Any(u => u.PlayerRole.Name == StaticValues.PlayerRole);
        }

        public bool IsAdmin()
        {
            if (_httpContext.HttpContext != null && !_httpContext.HttpContext.User.Identity.IsAuthenticated)
                return false;

            var user = GetPlayer();
            return user != default(Player) && user.Roles.Any(u => u.PlayerRole.Name == StaticValues.AdminRole);
        }

        public int GetId()
        {
            if (_httpContext.HttpContext != null && !_httpContext.HttpContext.User.Identity.IsAuthenticated)
                return 0;

            var user = GetPlayer();
            return user?.Id ?? 0;
        }

        private Player GetPlayer()
        {
            var authId = GetAuthId();
            if (!_context.Player.Any(x => x.AuthString == authId))
            {
                var player = new Player()
                {
                    DisplayName = "SPMS User",
                    AuthString = authId
                };

                if(_context.Player.Count() == 0)
                {
                    foreach(var role in _context.PlayerRole)
                    {
                        player.Roles.Add(new PlayerRolePlayer() { PlayerRoleId = role.Id });
                    }
                }
                _context.Player.Add(player);
                _context.SaveChanges();
            }

            return _context.Player.Include(p => p.Roles).ThenInclude(role => role.PlayerRole).First(x => x.AuthString == authId);
        }

        public Player GetPlayerFromDatabase()
        {
            return GetPlayer();
        }
    };
    public interface IUserService
    {
        string GetAuthId();
        string GetName();
        bool IsPlayer();
        bool IsAdmin();
        int GetId();
        Player GetPlayerFromDatabase();
    }
}
