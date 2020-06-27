using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SPMS.Web.Models;
using SPMS.Web.Service;
using SPMS.Web.TagHelper;

namespace SPMS.Web.Areas.player.Hubs
{
    public class AuthoringHub : Hub
    {
        private readonly SpmsContext _db;
        private readonly ILogger<AuthoringHub> _logger;
        private readonly IUserService _userService;

        public AuthoringHub(SpmsContext context, ILogger<AuthoringHub> logger, IUserService userService)
        {
            _db = context;
            _logger = logger;
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            var name = await _userService.GetEmailAsync();
            _logger.LogInformation($"Connections name {name}");

            var userCount =  _db.Player
                .Include(u => u.Connections)
                .Count(u => u.Email == name);
            _logger.LogInformation($"Number of matching results {userCount} for {name}");
            var user = _db.Player
                .Include(u => u.Connections)
                .SingleOrDefault(u => u.Email == name);

            _logger.LogInformation($"User found {user.Id} - {user.DisplayName} - {user.Email} - {user.AuthString}");

            user.Connections.Add(new PlayerConnection()
            {
                ConnectionId = Context.ConnectionId,
                UserAgent = Context.GetHttpContext().Request.Headers["User-Agent"],
                Connected = true
            });
            _logger.LogInformation($"Add connection {Context.ConnectionId} - {Context.GetHttpContext().Request.Headers["User-Agent"]}");
            await _db.SaveChangesAsync();
            await base.OnConnectedAsync();

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {

            var connection = await _db.PlayerConnection.FindAsync(Context.ConnectionId);
            if (connection != null)
            {
                connection.Connected = false;
                await _db.SaveChangesAsync();
                //await Groups.RemoveFromGroupAsync(Context.ConnectionId, "group1");
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(string groupName)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);

            var playerConnection = await _db.PlayerConnection.Include(x => x.Player)
                .FirstOrDefaultAsync(x => x.ConnectionId == Context.ConnectionId);

            _logger.LogInformation($"Finding player associated with Connection {Context.ConnectionId} - User Id {playerConnection.PlayerId}");

            await Clients.Group(groupName).SendAsync("AddPlayer", playerConnection.PlayerId.ToString());
        }

        public async Task LeaveGroup(string groupName)
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, groupName);
            var user =
                await _db.Player.Include(p => p.Connections).FirstOrDefaultAsync(p =>
                    p.Connections.Any(x => x.ConnectionId == Context.ConnectionId));

            await Clients.Group(groupName).SendAsync("RemovePlayer", user.Id.ToString());
        }


        public async Task SendMessage(string msg, string groupName)
        {
            _logger.LogInformation($"Sending to group {groupName}");
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveText", msg);
        }
    }
}
