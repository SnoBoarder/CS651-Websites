using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSite7Module.Startup))]
namespace WebSite7Module
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
