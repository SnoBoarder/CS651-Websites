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
		public const int SPEED = 5;

		private static int _enemyGUID = 0;

		public delegate void BounceEventHandler();
		public event BounceEventHandler OnBounce;

		public string Name { get; set; }

		public int Speed { get; private set; }


		public Enemy() : base(WIDTH, HEIGHT)
		{
			ID = Interlocked.Increment(ref _enemyGUID);// Reverse bullet GUID's to go below 0
			Name = NAME_PREFIX + ID;

			Speed = SPEED;
		}

		public void Update(GameTime gameTime)
		{
			base.Update();

			Position.X+= Speed;

			SetPosition(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y));
			if (Position.X + Width > Game.Instance.ScreenConfiguration.SCREEN_WIDTH || Position.X < 0)
			{
				OnBounce?.Invoke();

				Speed *= -1;
			}
		}
	}
}