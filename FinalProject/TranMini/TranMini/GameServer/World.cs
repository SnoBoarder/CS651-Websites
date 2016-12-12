using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer
{
	public class World
	{
		private List<Collidable> _objects;

		public World()
		{
			_objects = new List<Collidable>();
		}
	}
}