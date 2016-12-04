using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using System.Web.Configuration;

using ReactChat.App_Start;
using ReactChat.Controllers;

namespace ReactChat.App_Start
{
    public class WebApiDependencyResolver : IDependencyResolver
    {
        private ChatManager _manager;

        public WebApiDependencyResolver(ChatManager chatManager)
        {
            _manager = chatManager;
        }

        public Object GetService(Type serviceType)
        {
            return serviceType == typeof(ChatController) ? new ChatController(_manager) : null;
        }

        public IEnumerable<Object> GetServices(Type serviceType)
        {
            return new List<Object>();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {

        }
    }
}