using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinExampleConsoleApplication
{
	using Microsoft.Owin;
	using Microsoft.Owin.Hosting;
	using Owin;
	using System.IO;
	// use an alias for the OWIN AppFunc:
	using AppFunc = Func<IDictionary<string, object>, Task>;

	class Program
	{

		static void Main(string[] args)
		{
			WebApp.Start<Startup>("http://localhost:8080");
			Console.WriteLine("Server Started; Press enter to Quit");
			Console.ReadLine();
		}
	}

	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var middleware = new Func<AppFunc, AppFunc>(MyMiddleWare);
			var otherMiddleware = new Func<AppFunc, AppFunc>(MyOtherMiddleware);

			app.Use(middleware);
			app.Use(otherMiddleware);
		}

		public AppFunc MyMiddleWare(AppFunc next)
		{
			AppFunc appFunc = async (IDictionary<string, object> environment) =>
			{
				// Do something with the incoming result:
				var response = environment["owin.ResponseBody"] as Stream;
				using (var writer = new StreamWriter(response))
				{
					await writer.WriteAsync("<h1>Hello from My First Middleware</h1>");
				}

				// Call the next Middleware in the chain:
				await next.Invoke(environment);
			};

			return appFunc;
		}

		public AppFunc MyOtherMiddleware(AppFunc next)
		{
			AppFunc appFunc = async (IDictionary<string, object> environment) =>
			{
				// Do something with the incoming request:
				var response = environment["owin.ResponseBody"] as Stream;
				using (var writer = new StreamWriter(response))
				{
					await writer.WriteAsync("<h1>Hello from My Second Middleware</h1>");
				}

				// Call the next Middleware in the chain:
				await next.Invoke(environment);


			};

			return appFunc;
		}
	}
}
