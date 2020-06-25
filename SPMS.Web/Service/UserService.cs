using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

            if (_httpContext.HttpContext != null && !_httpContext.HttpContext.User.Identity.IsAuthenticated)
                return null;
            var user = GetPlayer();
            return user.DisplayName;
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
        
        private async Task<Player> GetPlayerAsync()
        {
            var authId = GetAuthId();

            if (!await _context.Player.AnyAsync(x => x.AuthString == authId))
            {
                var entity = new Player()
                {
                    DisplayName = _httpContext.HttpContext.User.Identity.Name,
                    AuthString = authId,
                    Email = _httpContext.HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value
                };

                if (!await _context.Player.AnyAsync())
                {
                    foreach (var role in _context.PlayerRole)
                    {
                        entity.Roles.Add(new PlayerRolePlayer() { PlayerRoleId = role.Id });
                    }
                }
                await _context.Player.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            var player = _context.Player.Include(p => p.Roles).ThenInclude(role => role.PlayerRole)
                .First(x => x.AuthString == authId);

            //TODO: Remove this fix
            if (string.IsNullOrEmpty(player.Email))
            {
                player.Email = _httpContext.HttpContext.User.Claims.First(x =>
                    x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                await _context.SaveChangesAsync();
            }

            return player;
        }

        private Player GetPlayer()
        {
            var authId = GetAuthId();

            if (!_context.Player.Any(x => x.AuthString == authId))
            {
                var player = new Player()
                {
                    DisplayName = _httpContext.HttpContext.User.Identity.Name,
                    AuthString = authId,
                    Email = _httpContext.HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value
                };

                if (!_context.Player.Any())
                {
                    foreach (var role in _context.PlayerRole)
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

        public async Task<string> GetEmailAsync()
        {
            var player = await GetPlayerAsync();

            return player.Email;
        }

        public async Task<string> GetGravatarHash()
        {
            var player = await GetPlayerAsync();
            var tmpSource = Encoding.ASCII.GetBytes(player.Email);
            var tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            var hash = new StringBuilder();
            for (int i = 0; i < tmpHash.Length; i++)
            {
                hash.Append(tmpHash[i].ToString("X2"));
            }
            return hash.ToString();
        }

        public bool IsAuthenticated()
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
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
        Task<string> GetEmailAsync();
        Task<string> GetGravatarHash();
        bool IsAuthenticated();
    }
}
