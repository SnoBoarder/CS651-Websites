using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranMini.Authentication;
using TranMini.GameServer;
using Newtonsoft.Json;

namespace TranMini.Controllers
{
	public class HomeController : Controller
	{
		static long GuestID = 0;

		public ActionResult Index()
		{
			RegisteredClient rc = new RegisteredClient();

			rc.RegistrationID = null;
			rc.DisplayName = "Player " + GuestID++;
			rc.Identity = "Player" + Guid.NewGuid().ToString();

			GameServer.Game.Instance.RegistrationHandler.Register(rc);

			TranMiniAuthenticationProvider.SetState(rc, Response);

			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}
	}
}