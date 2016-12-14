using System;
using System.Configuration;
using System.Security.Claims;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using TranMini.Authentication;

[assembly: OwinStartup(typeof(TranMini.Startup.Startup))]

namespace TranMini.Startup
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Any connection or hub wire up and configuration should go here
			app.MapSignalR();

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = "TranMini",
				Provider = new TranMiniAuthenticationProvider()
			});
		}
	}
}
