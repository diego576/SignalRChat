using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using SignalRChat.AzureServer;
using Microsoft.AspNet.SignalR;

namespace SignalRChat.AzureServer
{
public class Global : HttpApplication
{
    public static Dictionary<String, String> Users = new Dictionary<string, string>();
        
    void Application_Start(object sender, EventArgs e)
    {
        HubConfiguration hubConfiguration = new HubConfiguration();
        hubConfiguration.EnableJavaScriptProxies = false;

        RouteTable.Routes.MapHubs("/chatserver", hubConfiguration);
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }
}
}
