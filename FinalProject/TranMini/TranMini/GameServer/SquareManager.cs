using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TranMini.GameServer.Util;

namespace TranMini.GameServer
{
	public class SquareManager
	{
		private GameHandler _gameHandler;

		//private RespawnManager _respawnManager;

		public ConcurrentDictionary<string, Square> Squares { get; set; }

		public SquareManager(GameHandler gameHandler)
		{
			_gameHandler = gameHandler;

			Squares = new ConcurrentDictionary<string, Square>();
		}

		public void Add(Square s)
		{
			// Only enable respawn if it hasn't been enabled yet
			//if (!s.RespawnEnabled)
			//{
			//	s.RespawnEnabled = true;
			//	s.OnDeath += new DeathEventHandler(_respawnManager.StartRespawnCountdown);
			//}

			Squares.TryAdd(s.Host.ConnectionID, s);
		}

		public void Remove(string connectionIDKey)
		{
			Square s;
			Squares.TryRemove(connectionIDKey, out s);
		}

		public void Update(GameTime gameTime)
		{
			List<string> keysToRemove = new List<string>(Squares.Count);
			Parallel.ForEach(Squares, currentSquare =>
			{
				if (!currentSquare.Value.Disposed)
				{
					// not disposed. update square!
					currentSquare.Value.Update(gameTime);
				}
				else
				{
					// item has been disposed, remove it from the list
					keysToRemove.Add(currentSquare.Key);
				}

			});

			for (int i = keysToRemove.Count - 1; i >= 0; i--)
			{
				Remove(keysToRemove[i]);
			}
		}
	}
}