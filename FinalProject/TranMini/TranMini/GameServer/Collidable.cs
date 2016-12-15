using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web;

namespace TranMini.GameServer
{
	public class Collidable
	{
		public bool Disposed { get; set; }
		public int ID { get; set; }
		public bool Collided { get; set; }
		public Vector2 Position { get; }
		public DateTime LastUpdated { get; set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		// updated internally inside Interlocked
		private static int _itemCount = 0;

		private Rectangle _bounds;
		private int _serverID = 0;

		public Collidable(int w, int h)
		{
			ID = -1;
			Width = w;
			Height = h;
			Position = new Vector2();
			_bounds = new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Width, Height);

			_serverID = Interlocked.Increment(ref _itemCount);
		}

		public void SetPosition(int x, int y)
		{
			Position.X = x;
			_bounds.X = x;

			if (Position.Y == 0)
			{
				Position.Y = y; // only set if y has not been set yet
				_bounds.Y = y;
			}
		}

		public int ServerID()
		{
			return _serverID;
		}

		/// <summary>
		/// Does not null out the object.  Simply used to represent when an object is no longer needed in the game world.
		/// </summary>
		public void Dispose()
		{
			Disposed = true;
		}

		///// <summary>
		///// Called when there is a collision with another object "<paramref name="c"/>."
		///// </summary>
		///// <param name="c">The object that I colided with</param>
		public virtual void HandleCollisionWith(Collidable c)
		{
		}

		public virtual Rectangle GetBounds()
		{
			return _bounds;
		}

		///// <summary>
		///// Calculates whether me and the collidable object <paramref name="c"/> are colliding.
		///// </summary>
		///// <param name="c">The object to check the collision against.</param>
		///// <returns>Whether or not I am colliding with <paramref name="c"/>.</returns>
		public virtual bool IsCollidingWith(Collidable c)
		{
			return _bounds.IntersectsWith(c.GetBounds());
		}

		public virtual void Update()
		{
			LastUpdated = DateTime.UtcNow;
		}
	}
}