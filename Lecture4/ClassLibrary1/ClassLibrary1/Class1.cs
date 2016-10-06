using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ClassLibrary1
{
	public class Class1 : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
		}

		private void OnPreRequestHandlerExecute(object sender, EventArgs e)
		{
			// i want this to execute before the pre request handler is called
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
