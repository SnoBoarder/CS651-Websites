using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer
{
	public class GameConfigurationManager
	{
		public GameConfiguration gameConfig { get; set; }

		public GameConfigurationManager()
		{
			gameConfig = new GameConfiguration();
		}
}