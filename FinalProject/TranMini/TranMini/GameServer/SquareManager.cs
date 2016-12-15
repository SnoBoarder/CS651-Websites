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

		public ConcurrentDictionary<string, Square> Squares { get; set; }

		public SquareManager(GameHandler gameHandler)
		{
			_gameHandler = gameHandler;

			Squares = new ConcurrentDictionary<string, Square>();
		}

		public void OrganizeSquares()
		{
			int totalSquares = Squares.Count;

			// space out the squares equally
			int padding = (Game.Instance.ScreenConfiguration.SCREEN_WIDTH - Square.WIDTH) / (totalSquares + 1);

			int x = padding;
			int y = Game.Instance.ScreenConfiguration.SCREEN_HEIGHT - Square.HEIGHT;
			Parallel.ForEach(Squares, currentSquare =>
			{
				currentSquare.Value.SetPosition(x, y);

				x += padding;
			});
		}

		public void Add(Square s)
		{
			Squares.TryAdd(s.Host.ConnectionID, s);
		}

		public void Remove(string connectionIDKey)
		{
			Square s;
			Squares.TryRemove(connectionIDKey, out s);

			OrganizeSquares();
		}

		public void RewardSquares()
		{
			Parallel.ForEach(Squares, currentSquare =>
			{
				if (currentSquare.Value.JustCollided)
					currentSquare.Value.JustCollided = false;
				else
					currentSquare.Value.CurrentScore++;
			});
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