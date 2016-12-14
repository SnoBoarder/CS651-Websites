using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer
{
	public class World
	{
		private List<Collidable> _objects;

		private object _insertLock = new object();

		public World()
		{
			_objects = new List<Collidable>();
		}

		public List<Collidable> objects {
			get
			{
				return _objects;
			}
		}

		public void Insert(Collidable obj)
		{
			lock (_insertLock)
			{
				_objects.Add(obj);
			}
		}

		public void Remove(Collidable obj)
		{
			_objects.Remove(obj);
		}
	}
}