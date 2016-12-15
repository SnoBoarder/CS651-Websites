using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using TranMini.GameServer.Util;

namespace TranMini.GameServer
{
	public class Square : Collidable
	{
		public const string NAME_PREFIX = "Square";
		public const int WIDTH = 50;
		public const int HEIGHT = WIDTH;
		public const int JUMP_DURATION = 500;

		private static int _squareGUID = 0;

		//public event DeathEventHandler OnDeath;

		//private ConcurrentQueue<Action> _enqueuedCommands; // maybe?

		public string Name { get; set; }
		public User Host { get; set; }

		public int JumpDuration { get; set; }
		public DateTime? JumpedAt { get; private set; }

		public int HighScore { get; private set; }
		public int CurrentScore { get; set; }

		public bool JustCollided { get; set; }

		public Square() : base(WIDTH, HEIGHT)
		{
			ID = Interlocked.Increment(ref _squareGUID);
			Name = NAME_PREFIX + ID;

			// handle death handler
			//OnDeath += new DeathEventHandler((sender, e) => StatRecorder.ShipDeath(sender, e));
		}

		public override void HandleCollisionWith(Collidable c)
		{
			// you collided with the enemy!

			if (JumpedAt.HasValue)
				return; // if you're jumping you're fine

			// you weren't jumping! fail
			JustCollided = true;

			if (CurrentScore > HighScore)
				HighScore = CurrentScore;

			CurrentScore = 0;
		}

		public void Jump()
		{
			if (!JumpedAt.HasValue)
			{
				JumpedAt = GameTime.Now;
				JumpDuration = JUMP_DURATION;
			}
		}

		public virtual void Update(GameTime gameTime)
		{
			base.Update();

			if (JumpedAt.HasValue && (GameTime.Now - JumpedAt) >= TimeSpan.FromMilliseconds(JUMP_DURATION))
			{
				JumpedAt = null;
				JumpDuration = 0;
			}
		}
	}
}