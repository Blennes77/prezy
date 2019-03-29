using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace prezi.Hubs
{

    public class PreziHub : Hub
    {
        // Méthode appelée 'invoke' depuis le client
        public async Task SendMessage(string user, string message)
        {
            // Méthode d'appel d'une fonction côté client : ReceiveMessage
            // Le fait d'avoir un Clients.All indique que l'on envoi un message à tous les clients
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
