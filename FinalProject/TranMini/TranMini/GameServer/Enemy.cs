using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using TranMini.GameServer.Util;

namespace TranMini.GameServer
{
	public class Enemy : Collidable
	{
		public const string NAME_PREFIX = "Enemy";
		public const int WIDTH = 25;
		public const int HEIGHT = WIDTH;

		private static int _enemyGUID = 0;

		public string Name { get; set; }

		public Enemy() : base(WIDTH, HEIGHT)
		{
			ID = Interlocked.Increment(ref _enemyGUID);// Reverse bullet GUID's to go below 0
			Name = NAME_PREFIX + ID;
		}

		private int speed = 5;

		public void Update(GameTime gameTime)
		{
			base.Update();

			Position.X+= speed;

			_bounds.X = (int)Position.X;
			if (Position.X + Width > Game.Instance.ScreenConfiguration.SCREEN_WIDTH || Position.X < 0)
				speed *= -1;
		}
	}
}