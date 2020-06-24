using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.EntityFrameworkCore;
using SPMS.Web.Models;

namespace SPMS.Web.Areas.player.Hubs
{
    public class AuthoringHub : Hub
    {
        private readonly SpmsContext _context;

        public AuthoringHub(SpmsContext context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;

            var user = _context.Player
                .Include(u => u.Connections)
                .SingleOrDefault(u => u.DisplayName == name);

            if (user == null)
            {
                await base.OnConnectedAsync();
                return;

                // This should never happen.
                //user = new Player
                //{
                //    DisplayName = name,
                //    Connections = new List<PlayerConnection>()
                //};
                //await _context.Player.AddAsync(user);
            }

            user.Connections.Add(new PlayerConnection()
            {
                ConnectionId = Context.ConnectionId,
                UserAgent = Context.GetHttpContext().Request.Headers["User-Agent"],
                Connected = true
            });
            await _context.SaveChangesAsync();
            await base.OnConnectedAsync();

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {

            var connection = await _context.PlayerConnection.FindAsync(Context.ConnectionId);
            if (connection != null)
            {
                connection.Connected = false;
                await _context.SaveChangesAsync();
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "group1");
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGroup(string groupName)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, groupName);
        }


        public async Task SendMessage(string msg)
        {
            //await Clients.All.SendAsync("RecieveMessage", user, msg);

            await Clients.OthersInGroup("group1").SendAsync("ReceiveText", msg);
        }
    }
}
