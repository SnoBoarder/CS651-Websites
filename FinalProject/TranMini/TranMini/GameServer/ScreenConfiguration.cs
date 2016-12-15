using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer
{
	public class ScreenConfiguration
	{
		public ScreenConfiguration()
		{
			SCREEN_WIDTH = 700;
			SCREEN_HEIGHT = 400;
		}

		public int SCREEN_WIDTH { get; set; }
		public int SCREEN_HEIGHT { get; set; }
	}
}