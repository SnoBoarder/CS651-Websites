using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using TranMini.GameServer;

namespace TranMini.Authentication
{
	public class TranMiniAuthenticationProvider : CookieAuthenticationProvider
	{
		public static void SetState(RegisteredClient rc, HttpResponseBase response)
		{
			// Save the cookie state
			Byte[] identity = Encoding.UTF8.GetBytes(rc.Identity);
			Byte[] encrypted = MachineKey.Protect(identity, "TranMini.Identity");
			var temp = new RegisteredClient(rc.RegistrationID, HttpServerUtility.UrlTokenEncode(encrypted), rc.DisplayName);
			var state = JsonConvert.SerializeObject(temp);

			response.Cookies.Add(new HttpCookie("tranmini.state", state));
		}
	}
}