using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReactJSChat.Startup))]
namespace ReactJSChat
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
			//ConfigureAuth(app);
		}
	}
}
