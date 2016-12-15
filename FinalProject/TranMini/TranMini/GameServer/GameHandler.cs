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

		bool init = false;

		public void Update(GameTime gameTime)
		{
			// make sure to update the squares and enemies before collision manager
			_squareManager.Update(gameTime);
			enemyManager.Update(gameTime);

			if (_squareManager.Squares.Count > 0 && !init)
			{
				init = true;
				AddEnemyToGame();
			}

			// collision manager should only check the enemies against the squares
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

		private void AddEnemyToGame()
		{
			Enemy enemy = new Enemy();
			enemy.SetPosition(0, Game.Instance.ScreenConfiguration.SCREEN_HEIGHT - Enemy.HEIGHT);

			enemy.OnBounce += _squareManager.RewardSquares;

			enemyManager.Add(enemy);
			_collisionManager.Monitor(enemy, true);
		}
	}
}