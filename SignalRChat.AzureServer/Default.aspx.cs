using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRChat.AzureServer
{
    public partial class _Default : Page
    {
    protected void Page_Load(object sender, EventArgs e)
    {
        string currentUrl = String.Empty;

        if (Request.ServerVariables["HTTPS"].ToString() == "")
        {
            currentUrl = Request.ServerVariables["SERVER_PROTOCOL"].ToString().ToLower().Substring(0, 4).ToString() + "://" + Request.ServerVariables["SERVER_NAME"].ToString() + ":" + Request.ServerVariables["SERVER_PORT"].ToString();
        }
        else
        {
            currentUrl = Request.ServerVariables["SERVER_PROTOCOL"].ToString().ToLower().Substring(0, 5).ToString() + "://" + Request.ServerVariables["SERVER_NAME"].ToString() + ":" + Request.ServerVariables["SERVER_PORT"].ToString();
        }

        Response.Write("Server is running on " + currentUrl);
    }
    }
}