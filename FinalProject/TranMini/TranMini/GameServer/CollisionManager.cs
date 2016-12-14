using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranMini.GameServer.Util;

namespace TranMini.GameServer
{
	public class CollisionManager
	{
		private List<Collidable> _objects;

		private World _world;

		public CollisionManager(World world)
		{
			_world = world;
			_objects = new List<Collidable>();
		}

		public void Update(GameTime gameTime)
		{
		}

		public void Monitor(Collidable obj)
		{
			_world.Insert(obj);
			_objects.Add(obj);
		}

		public void UnMonitor(Collidable obj)
		{
			_world.Remove(obj);
			_objects.Remove(obj);
		}
	}
}