using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System;
using Owin;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Hosting;


namespace SignalRChat.Server
{
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting server...");

        string url = "http://localhost:8080";

        using (WebApplication.Start<Startup>(url))
        {
            Console.Clear();
            Console.WriteLine("Server running on {0}", url);

            while (true)
            {
                ConsoleKeyInfo ki = Console.ReadKey(true);
                if (ki.Key == ConsoleKey.X)
                {
                    break;
                }
            }
        }
    }
}

class Startup
{
    public void Configuration(IAppBuilder app)
    {
        app.MapHubs("/chatserver");
    }
}

public class LocalCache
{
    public static Dictionary<String, String> Users = new Dictionary<string, string>();
}

    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        public Task SendMessage(string message)
        {
            String formatedMessage = String.Format("{0}: {1}", LocalCache.Users[Context.ConnectionId], message);

            return Clients.All.addMessage(formatedMessage);
        }

        public Boolean Join(string username)
        {
            LocalCache.Users.Add(Context.ConnectionId, username);

            Clients.Others.addMessage(username + " joined the chat.");

            Console.WriteLine(Context.ConnectionId + " (" + username + ") connected.");

            return true;
        }

        public List<String> GetUsers()
        {
            return LocalCache.Users.Values.ToList<String>();
        }
    }
}
