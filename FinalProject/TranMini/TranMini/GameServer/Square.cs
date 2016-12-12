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
		public const int WIDTH = 10;
		public const int HEIGHT = WIDTH;

		private static int _squareGUID = 0;

		//public event DeathEventHandler OnDeath;

		//private ConcurrentQueue<Action> _enqueuedCommands; // maybe?

		public User Host { get; set; }

		public Square() : base(WIDTH, HEIGHT)
		{
			ID = Interlocked.Increment(ref _squareGUID);

			// handle death handler
			//OnDeath += new DeathEventHandler((sender, e) => StatRecorder.ShipDeath(sender, e));

			//_enqueuedCommands = new ConcurrentQueue<Action>();

		}

		public virtual void Update(GameTime gameTime)
		{
			base.Update();

		//	Action command;

		//	while (_enqueuedCommands.Count > 0)
		//	{
		//		if (_enqueuedCommands.TryDequeue(out command) && !this.Disposed)
		//		{
		//			command();
		//		}
		//	}
		}
	}
}