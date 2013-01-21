using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SignalRChat.AzureServer
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        public Task SendMessage(string message)
        {
            String formatedMessage = String.Format("{0}: {1}", Global.Users[Context.ConnectionId], message);

            return Clients.All.addMessage(formatedMessage);
        }

        public Boolean Join(string username)
        {
            Global.Users.Add(Context.ConnectionId, username);

            Clients.Others.addMessage(username + " joined the chat.");

            Console.WriteLine(Context.ConnectionId + " (" + username + ") connected.");

            return true;
        }

        public List<String> GetUsers()
        {
            return Global.Users.Values.ToList<String>();
        }
    }
}