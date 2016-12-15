using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TranMini.GameServer.Util;

namespace TranMini.GameServer
{
	public class EnemyManager
	{
		private object _locker = new object();

		public List<Enemy> Enemies { get; set; }

		public EnemyManager()
		{
			Enemies = new List<Enemy>();
		}

		public void Add(Enemy enemy)
		{
			lock (_locker)
			{
				Enemies.Add(enemy);
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach (Enemy enemy in Enemies)
			{
				if (!enemy.Disposed)
				{
					enemy.Update(gameTime);
				}
			}
		}
	}
}