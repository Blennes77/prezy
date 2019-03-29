using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace prezy
{
    public class PrezyHub : Hub
    {
        public Task JoinGroup(string _groupName)
        {
            return Groups.Add(Context.ConnectionId, _groupName);
        }
        public Task LeaveGroup(string _groupName)
        {
            return Groups.Remove(Context.ConnectionId, _groupName);
        }
        public void Action(Message message)
        {
            var btHub = GlobalHost.ConnectionManager.GetHubContext<PrezyHub>();

            btHub.Clients.Group(message.Room).action(message);
        }
        public void Send(string name, string message)
        {
            //Clients.All.broadcastMessage(name, message);
            Clients.Others.broadcastMessage(name, message);
        }

    }

    public class Message
    {
        public string Type { get; set; }
        public string File { get; set; }
        public string DisplayId { get; set; }
        public string Command { get; set; }
        public string Room { get; set; } // This is the group
    }

}