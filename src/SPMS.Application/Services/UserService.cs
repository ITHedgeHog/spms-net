using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;
using SPMS.Domain.Models;
using NotImplementedException = System.NotImplementedException;

namespace SPMS.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ICurrentUserService _currentUser;
        private readonly ISpmsContext _context;

        public UserService(ISpmsContext context, ICurrentUserService currentUser)
        {
            
            _context = context;
            _currentUser = currentUser;
        }

        public string GetAuthId()
        {
            return _currentUser.GetAuthId();
        }

        public string GetName()
        {

            if (!_currentUser.IsAuthenticated())
                return null;
            var user = GetPlayer();
            return user.DisplayName;
        }

        public bool IsPlayer()
        {
            if (!_currentUser.IsAuthenticated())
                return false;
            var user = GetPlayer();
            return user.Roles.Any(u => u.PlayerRole.Name == StaticValues.PlayerRole);
        }

        public bool IsAdmin()
        {
            if (!_currentUser.IsAuthenticated())
                return false;

            var user = GetPlayer();
            return user != default(Player) && user.Roles.Any(u => u.PlayerRole.Name == StaticValues.AdminRole);
        }

        public int GetId()
        {
            if (!_currentUser.IsAuthenticated())
                return 0;

            var user = GetPlayer();
            return user?.Id ?? 0;
        }
        
        private async Task<Player> GetPlayerAsync(CancellationToken token)
        {
            var authId = GetAuthId();

            if (!await _context.Player.AnyAsync<Player>(x => x.AuthString == authId, cancellationToken: token))
            {
                var entity = new Player()
                {
                    DisplayName = _currentUser.GetName(),
                    AuthString = authId,
                    Email = _currentUser.GetEmail() 
                };

                if (!await _context.Player.AnyAsync<Player>(cancellationToken: token))
                {
                    foreach (var role in _context.PlayerRole)
                    {
                        entity.Roles.Add(new PlayerRolePlayer() { PlayerRoleId = role.Id });
                    }
                }
                await _context.Player.AddAsync(entity, token);
                await _context.SaveChangesAsync(token);
            }

            var player = _context.Player.Include(p => p.Roles).ThenInclude(role => role.PlayerRole).First<Player>(x => x.AuthString == authId);

            //TODO: Remove this fix
            if (string.IsNullOrEmpty(player.Email))
            {
                player.Email = _currentUser.GetEmail();
                await _context.SaveChangesAsync(token);
            }

            return player;
        }

        private Player GetPlayer()
        {
            //var isNew = _currentUser.IsNew();
            var authId = GetAuthId();

            //if (isNew && !_context.Player.Any(x => x.AuthString == authId))
            //{
            //    var player = new Player()
            //    {
            //        DisplayName = _currentUser.GetName(),
            //        AuthString = authId,
            //        Email = _currentUser.GetEmail(),
            //        Firstname = _currentUser.GetFirstname(),
            //        Surname = _currentUser.GetSurname()
            //    };

            //    if (!_context.Player.Any())
            //    {
            //        foreach (var role in _context.PlayerRole)
            //        {
            //            player.Roles.Add(new PlayerRolePlayer() { PlayerRoleId = role.Id });
            //        }
            //    }
            //    _context.Player.Add(player);
            //    _context.SaveChanges();
            //}


            return _context.Player.Include(p => p.Roles).ThenInclude(role => role.PlayerRole).First<Player>(x => x.AuthString == authId);
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
            //var player = GetPlayer();
            return _currentUser.IsAuthenticated();
        }

        public async Task CreateNewPlayer(CancellationToken cancellationToken)
        {
            var authId = GetAuthId();

            if (!string.IsNullOrEmpty(authId) && !_context.Player.Any(x => x.AuthString == authId))
            {
                var player = new Player()
                {
                    DisplayName = _currentUser.GetName(),
                    AuthString = authId,
                    Email = _currentUser.GetEmail(),
                    Firstname = _currentUser.GetFirstname(),
                    Surname = _currentUser.GetSurname()
                };

                if (!_context.Player.Any())
                {
                    foreach (var role in _context.PlayerRole)
                    {
                        player.Roles.Add(new PlayerRolePlayer() { PlayerRoleId = role.Id });
                    }
                }
                await _context.Player.AddAsync(player, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            //return _context.Player.Include(p => p.Roles).ThenInclude(role => role.PlayerRole).First<Player>(x => x.AuthString == authId);
        }
    };
}