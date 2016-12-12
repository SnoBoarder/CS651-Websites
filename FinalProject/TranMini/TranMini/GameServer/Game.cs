using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranMini.GameServer.Util;

namespace TranMini.GameServer
{
	public class Game
	{
		private readonly static Lazy<Game> _instance = new Lazy<Game>(() => new Game());

		public static Game Instance
		{
			get
			{
				return _instance.Value;
			}
		}

		private HighFrequencyTimer _gameLoop;
		private GameTime _gameTime;
		private World _world;
		private object _locker;

		private long _drawCount = 0;
		private long _actualFPS = 0;

		public GameConfigurationManager Configuration { get; set; }
		public UserHandler UserHandler { get; private set; }
		public ConnectionManager ConnectionManager { get; private set; }
		public GameHandler GameHandler { get; set; }

		// private constructor since this should be a singleton
		private Game()
		{
			_gameLoop = new HighFrequencyTimer(1000 / Configuration.gameConfig.UPDATE_INTERVAL, id => Update(id), () => { }, () => { }, (fps) =>
			{
				_actualFPS = fps;
			});

			// TODO: make game time

			_world = new World();
			GameHandler = new GameHandler(_world);

			//_payloadManager = new PayloadManager();

			// RegistraionHandler

			UserHandler = new UserHandler(GameHandler);
			ConnectionManager = new ConnectionManager(UserHandler, _locker);
		}

		public static IHubContext GetContext()
		{
			return GlobalHost.ConnectionManager.GetHubContext<GameHub>();
		}

		private long Update(long id)
		{
			lock (_locker)
			{
				DateTime utcNow = DateTime.UtcNow;

				try
				{
		//			if ((utcNow - _lastSpawn).TotalSeconds >= 1 && _spawned < AIShipsToSpawn)
		//			{
		//				_spawned += SpawnsPerInterval;
		//				SpawnAIShips(SpawnsPerInterval);
		//				_lastSpawn = utcNow;
		//			}

					_gameTime.Update(utcNow);

					GameHandler.Update(_gameTime);

		//			_space.Update();

		//			if (_actualFPS <= _drawFPS || (++_drawCount) % DRAW_AFTER == 0)
		//			{
		//				Draw();
		//				_drawCount = 0;
		//			}
				}
				catch (Exception e)
				{
		//			ErrorLog.Instance.Log(e);
				}

				return id;
			}
		}

		/// <summary>
		/// Sends down batches of data to the clients in order to update their screens
		/// </summary>
		//private void Draw()
		//{
		//	_space.CheckIncreaseMapSize(UserHandler.TotalActiveUsers);
		//	UserHandler.Update();

		//	ConcurrentDictionary<string, object[]> payloads = _payloadManager.GetGamePayloads(UserHandler.GetUsers(), UserHandler.TotalActiveUsers, GameHandler.BulletManager.Bullets.Count, _space);
		//	IHubContext context = GetContext();

		//	foreach (string connectionID in payloads.Keys)
		//	{
		//		UserHandler.GetUser(connectionID).PushToClient(payloads[connectionID], context);
		//	}
		//}

		/// <summary>
		/// Retrieves the game's configuration
		/// </summary>
		/// <returns>The game's configuration</returns>
		//public object initializeClient(string connectionId, RegisteredClient rc)
		//{
		//	if (!UserHandler.UserExistsAndReady(connectionId))
		//	{
		//		try
		//		{
		//			lock (_locker)
		//			{
		//				User user = UserHandler.FindUserByIdentity(rc.Identity);
		//				Ship ship;

		//				if (user == null)
		//				{
		//					if (UserHandler.TotalActiveUsers >= RuntimeConfiguration.MaxServerUsers)
		//					{
		//						return new
		//						{
		//							ServerFull = true
		//						};
		//					}
		//					else
		//					{
		//						ship = new Ship(RespawnManager.GetRandomStartPosition(), GameHandler.BulletManager);
		//						ship.Name = rc.DisplayName;
		//						user = new User(connectionId, ship, rc) { Controller = false };
		//						UserHandler.AddUser(user);
		//					}
		//				}
		//				else
		//				{
		//					string previousConnectionID = user.ConnectionID;
		//					UserHandler.ReassignUser(connectionId, user);
		//					ship = user.MyShip;

		//					if (user.Connected) // Check if it's a duplicate login
		//					{
		//						GetContext().Clients.Client(previousConnectionID).controlTransferred();
		//						user.NotificationManager.Notify("Transfering control to this browser.  You were already logged in.");
		//					}
		//					else
		//					{
		//						ship.Disposed = false;
		//						ship.LifeController.HealFull();
		//						user.Connected = true;
		//					}

		//					user.IdleManager.RecordActivity();
		//					user.IdleManager.Idle = false;
		//				}

		//				GameHandler.AddShipToGame(ship);
		//			}

		//			return new
		//			{
		//				Configuration = Configuration,
		//				ServerFull = false,
		//				CompressionContracts = new
		//				{
		//					PayloadContract = _payloadManager.Compressor.PayloadCompressionContract,
		//					CollidableContract = _payloadManager.Compressor.CollidableCompressionContract,
		//					ShipContract = _payloadManager.Compressor.ShipCompressionContract,
		//					BulletContract = _payloadManager.Compressor.BulletCompressionContract,
		//					LeaderboardEntryContract = _payloadManager.Compressor.LeaderboardEntryCompressionContract,
		//					PowerupContract = _payloadManager.Compressor.PowerupCompressionContract
		//				},
		//				ShipID = UserHandler.GetUserShip(connectionId).ID,
		//				ShipName = UserHandler.GetUserShip(connectionId).Name
		//			};
		//		}
		//		catch (Exception e)
		//		{
		//			ErrorLog.Instance.Log(e);
		//		}
		//	}

		//	return null;
		//}

		/// <summary>
		/// Retrieves the game's configuration
		/// </summary>
		/// <returns>The game's configuration</returns>
		//public object initializeController(string connectionId, RegisteredClient rc)
		//{
		//	if (!UserHandler.UserExistsAndReady(connectionId))
		//	{
		//		try
		//		{
		//			User main = UserHandler.FindUserByIdentity(rc.Identity);

		//			if (main != null)
		//			{
		//				User controllerUser = new User(connectionId, rc) { Controller = true };

		//				controllerUser.MyShip = main.MyShip;

		//				UserHandler.AddUser(controllerUser);
		//				main.RemoteControllers.Add(controllerUser);

		//				main.NotificationManager.Notify("Controller attached.");

		//				return new
		//				{
		//					Configuration = Configuration,
		//					CompressionContracts = new
		//					{
		//						PayloadContract = _payloadManager.Compressor.PayloadCompressionContract,
		//						CollidableContract = _payloadManager.Compressor.CollidableCompressionContract,
		//						ShipContract = _payloadManager.Compressor.ShipCompressionContract,
		//						BulletContract = _payloadManager.Compressor.BulletCompressionContract,
		//						LeaderboardEntryCompressionContract = _payloadManager.Compressor.LeaderboardEntryCompressionContract
		//					}
		//				};
		//			}
		//			else
		//			{
		//				return new
		//				{
		//					FailureMessage = "Could not find logged in user to control."
		//				};
		//			}
		//		}
		//		catch (Exception e)
		//		{
		//			ErrorLog.Instance.Log(e);
		//		}
		//	}

		//	return null;
		//}
	}
}