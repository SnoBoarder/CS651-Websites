﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

using ReactChat.Models;
using ReactChat.App_Start;

namespace ReactChat
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			RouteTable.Routes.MapHttpRoute(
						name: "DefaultApi",
						routeTemplate: "api/{controller}/{id}",
						defaults: new { id = System.Web.Http.RouteParameter.Optional }
						);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["ChatStore"] = new ChatStore();
            ChatManager chatManager = new ChatManager(Session["ChatStore"] as ChatStore);
            GlobalConfiguration.Configuration.DependencyResolver = new WebApiDependencyResolver(chatManager);
        }
    }
}
