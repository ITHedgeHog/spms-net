using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;
using SPMS.Domain.Models;

namespace SPMS.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ISpmsContext _context;

        public UserService(IHttpContextAccessor httpContext, ISpmsContext context)
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
        
        private async Task<Player> GetPlayerAsync(CancellationToken token)
        {
            var authId = GetAuthId();

            if (!await _context.Player.AnyAsync(x => x.AuthString == authId, cancellationToken: token))
            {
                var entity = new Player()
                {
                    DisplayName = _httpContext.HttpContext.User.Identity.Name,
                    AuthString = authId,
                    Email = _httpContext.HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value
                };

                if (!await _context.Player.AnyAsync(cancellationToken: token))
                {
                    foreach (var role in _context.PlayerRole)
                    {
                        entity.Roles.Add(new PlayerRolePlayer() { PlayerRoleId = role.Id });
                    }
                }
                await _context.Player.AddAsync(entity, token);
                await _context.SaveChangesAsync(token);
            }

            var player = _context.Player.Include(p => p.Roles).ThenInclude(role => role.PlayerRole)
                .First(x => x.AuthString == authId);

            //TODO: Remove this fix
            if (string.IsNullOrEmpty(player.Email))
            {
                player.Email = _httpContext.HttpContext.User.Claims.First(x =>
                    x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                await _context.SaveChangesAsync(token);
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

        public async Task<string> GetEmailAsync(CancellationToken token)
        {
            var player = await GetPlayerAsync(token);

            return player.Email;
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
        Task<string> GetEmailAsync(CancellationToken token);
        bool IsAuthenticated();
    }
}
