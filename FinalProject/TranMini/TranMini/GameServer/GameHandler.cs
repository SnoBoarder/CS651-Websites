using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranMini.GameServer.Util;

namespace TranMini.GameServer
{
	public class GameHandler
	{
		private SquareManager _squareManager;
		private CollisionManager _collisionManager;
		private World _world;

		public EnemyManager enemyManager { get; set; }

		public GameHandler(World world)
		{
			_world = world;

			_squareManager = new SquareManager(this);
			_collisionManager = new CollisionManager(_world);

			enemyManager = new EnemyManager();
		}

		public void Update(GameTime gameTime)
		{
			_squareManager.Update(gameTime);
			enemyManager.Update(gameTime);

			_collisionManager.Update(gameTime);
		}

		public void AddSquareToGame(Square square)
		{
			if (square != null)
			{
				_squareManager.Add(square);
				_collisionManager.Monitor(square);

				_squareManager.OrganizeSquares();
			}
		}
	}
}