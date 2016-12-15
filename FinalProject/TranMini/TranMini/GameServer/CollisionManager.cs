using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TranMini.GameServer.Util;

namespace TranMini.GameServer
{
	public class CollisionManager
	{
		private List<Collidable> _objects;
		private List<Collidable> _invincibles;

		private World _world;

		public CollisionManager(World world)
		{
			_world = world;
			_objects = new List<Collidable>();
			_invincibles = new List<Collidable>();
		}

		public void Update(GameTime gameTime)
		{
			bool[] objectsToKeepAround = new bool[_objects.Count];

			// traverse every object and check collision with only the enemy
			Parallel.For(0, _objects.Count, i =>
			{
				Collidable thisObject = _objects[i];
				if (thisObject.Disposed)
				{
					objectsToKeepAround[i] = false;
					return;
				}

				// Retrieve objects that it could be colliding with
				List<Collidable> potentials = _invincibles;

				for (int j = 0; j < potentials.Count; j++)
				{
					Collidable thisPotential = potentials[j];

					if (thisObject.IsCollidingWith(thisPotential))
					{
						thisObject.HandleCollisionWith(thisPotential);
						thisPotential.HandleCollisionWith(thisObject);

						if (thisObject.Disposed)
						{
							objectsToKeepAround[i] = false;
							return;
						}
					}
				}

				objectsToKeepAround[i] = true;
			});

			for (int i = objectsToKeepAround.Length - 1; i >= 0; i--)
			{
				if (!objectsToKeepAround[i])
				{
					_objects[i] = _objects[_objects.Count - 1];
					_objects.RemoveAt(_objects.Count - 1);
				}
			}
		}

		public void Monitor(Collidable obj, bool invincible = false)
		{
			_world.Insert(obj);

			if (invincible)
				_invincibles.Add(obj);
			else
				_objects.Add(obj);
		}

		public void UnMonitor(Collidable obj)
		{
			_world.Remove(obj);

			if (_objects.IndexOf(obj) != -1)
				_objects.Remove(obj);
			else if (_invincibles.IndexOf(obj) != -1)
				_invincibles.Remove(obj);
		}
	}
}