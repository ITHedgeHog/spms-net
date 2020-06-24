using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Web.Areas.player.Hubs
{
    public class AuthoringHub : Hub
    {
        static HashSet<string> CurrentConnections = new HashSet<string>();


        public override async Task OnConnectedAsync()
        {
            
            await Groups.AddToGroupAsync(Context.ConnectionId, "group1");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "group1");
            await base.OnDisconnectedAsync(exception);
        }



        public async Task SendMessage(string msg)
        {
            //await Clients.All.SendAsync("RecieveMessage", user, msg);

            await Clients.OthersInGroup("group1").SendAsync("ReceiveText", msg);
        }
    }
}
