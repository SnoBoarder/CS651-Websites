using System;
using System.Web;

namespace ClassLibrary1
{
	/// <summary>
	/// Take this DLL, plug it into IIS somehow and associate it with a resource type
	/// and anytime a client asks for a resource type this code will fire.
	/// </summary>
	public class Class1 : IHttpHandler
	{
		public bool IsReusable
		{
			get { return true; }
		}

		/// <summary>
		/// Interecption point where you can add your code
		/// </summary>
		/// <param name="context"></param>
		public void ProcessRequest(HttpContext context)
		{
			//context.Response.Write("HelloWorld");

			DateTime dt;
			string useUtc = context.Request.QueryString["utc"]; // use universal time or not
			if (!string.IsNullOrEmpty(useUtc) && useUtc.Equals("true"))
			{
				dt = DateTime.UtcNow;
			}
			else
			{
				dt = DateTime.Now;
			}

			context.Response.Write(string.Format("<h1>{0}</h1>", dt.ToLongTimeString()));
		}
	}
}
