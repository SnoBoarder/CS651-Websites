using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApplication
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
			var loggingMiddleWare = new Func<AppFunc, AppFunc>(LoggingMiddleWare);
			var authenticationMiddleWare = new Func<AppFunc, AppFunc>(AuthenticationMiddleWare);
			var myMiddleWare = new Func<AppFunc, AppFunc>(MyMiddleWare);

			app.Use(loggingMiddleWare);
			app.Use(authenticationMiddleWare);
			app.Use(myMiddleWare);
		}

		public AppFunc LoggingMiddleWare(AppFunc next)
		{
			AppFunc appFunc = async (IDictionary<string, object> environment) =>
			{
				// Call the next Middleware in the chain:
				await next.Invoke(environment);

				// Do the logging on the way out:
				IOwinContext context = new OwinContext(environment);
				Console.WriteLine("URI: {0} Status Code: {1}",
				context.Request.Uri, context.Response.StatusCode);
			};

			return appFunc;
		}

		public AppFunc AuthenticationMiddleWare(AppFunc next)
		{
			AppFunc appFunc = async (IDictionary<string, object> environment) =>
			{
				IOwinContext context = new OwinContext(environment);

				// In the real world we would do REAL auth processing here...

				var isAuthorized = context.Request.QueryString.Value == "dino";
				if (!isAuthorized)
				{
					context.Response.StatusCode = 401;
					context.Response.ReasonPhrase = "Not Authorized";

					// Send back a really silly error page:
					await context.Response.WriteAsync(string.Format("<h1>Error {0}-{1}",
						context.Response.StatusCode,
						context.Response.ReasonPhrase));
				}
				else
				{
					// _next is only invoked is authentication succeeds:
					context.Response.StatusCode = 200;
					context.Response.ReasonPhrase = "OK";
					await next.Invoke(environment);
				}
			};

			return appFunc;
		}

		public AppFunc MyMiddleWare(AppFunc next)
		{
			AppFunc appFunc = async (IDictionary<string, object> environment) =>
			{
				// Do something with the incoming result:
				var response = environment["owin.ResponseBody"] as Stream;
				using (var writer = new StreamWriter(response))
				{
					await writer.WriteAsync("<h1>Hello Dino!</h1>");
				}

				// Call the next Middleware in the chain:
				await next.Invoke(environment);
			};

			return appFunc;
		}
	}
}
