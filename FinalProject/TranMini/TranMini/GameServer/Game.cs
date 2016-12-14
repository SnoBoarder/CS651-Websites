using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
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

		private PayloadManager _payloadManager;

		private HighFrequencyTimer _gameLoop;
		private GameTime _gameTime;
		private World _world;
		private object _locker;

		private long _drawCount = 0;
		private long _actualFPS = 0;
		private long _drawFPS = 0;
		private int DRAW_AFTER;

		public RuntimeConfiguration RuntimeConfiguration { get; set; }
		public GameConfigurationManager Configuration { get; set; }
		public UserHandler UserHandler { get; private set; }
		public ConnectionManager ConnectionManager { get; private set; }
		public GameHandler GameHandler { get; set; }
		public RegistrationHandler RegistrationHandler { get; private set; }

		// private constructor since this should be a singleton
		private Game()
		{
			_locker = new object();

			Configuration = new GameConfigurationManager();
			RegistrationHandler = new RegistrationHandler();
			RuntimeConfiguration = new RuntimeConfiguration();

			DRAW_AFTER = Configuration.gameConfig.DRAW_INTERVAL / Configuration.gameConfig.UPDATE_INTERVAL;
			_drawFPS = 1000 / Configuration.gameConfig.DRAW_INTERVAL;

			_gameLoop = new HighFrequencyTimer(1000 / Configuration.gameConfig.UPDATE_INTERVAL, id => Update(id), () => { }, () => { }, (fps) =>
			{
				_actualFPS = fps;
			});

			_gameTime = new GameTime();

			_world = new World();
			GameHandler = new GameHandler(_world);

			_payloadManager = new PayloadManager();

			// RegistraionHandler

			UserHandler = new UserHandler(GameHandler);
			ConnectionManager = new ConnectionManager(UserHandler, _locker);

			_gameLoop.Start();
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

					if (_actualFPS <= _drawFPS || (++_drawCount) % DRAW_AFTER == 0)
					{
						Draw();
						_drawCount = 0;
					}
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
		private void Draw()
		{
			//_world.CheckIncreaseMapSize(UserHandler.TotalActiveUsers);
			UserHandler.Update();

			ConcurrentDictionary<string, object[]> payloads = _payloadManager.GetGamePayloads(UserHandler.GetUsers(), UserHandler.TotalActiveUsers, _world);
			IHubContext context = GetContext();

			foreach (string connectionID in payloads.Keys)
			{
				UserHandler.GetUser(connectionID).PushToClient(payloads[connectionID], context);
			}
		}

		/// <summary>
		/// Retrieves the game's configuration
		/// </summary>
		/// <returns>The game's configuration</returns>
		public object initializeClient(string connectionId, RegisteredClient rc)
		{
			if (UserHandler.UserExistsAndReady(connectionId))
				return null; // user already exists

			// user does not exist. setup client
			try
			{
				lock (_locker)
				{
					User user = UserHandler.FindUserByIdentity(rc.Identity);
					Square square;

					if (user == null)
					{
						// user doesn't exist

						if (UserHandler.TotalActiveUsers >= RuntimeConfiguration.MaxServerUsers)
						{
							// server is full!
							return new
							{
								ServerFull = true
							};
						}
						else
						{
							// server is not full! generate a square for user
							square = new Square();
							square.Name = rc.DisplayName;
							user = new User(connectionId, square, rc) { Controller = false };
							UserHandler.AddUser(user);
						}
					}
					else
					{
						// user exists!
						string previousConnectionID = user.ConnectionID;
						UserHandler.ReassignUser(connectionId, user);
						square = user.MySquare;

						if (user.Connected)
						{
							// user already logged in
							GetContext().Clients.Client(previousConnectionID).controlTransferred();
							//user.NotificationManager.Notify("Transfering control to this browser.  You were already logged in.");
						}
						else
						{
							// user was not connected
							square.Disposed = false;
							user.Connected = true;
						}

						//user.IdleManager.RecordActivity();
						//user.IdleManager.Idle = false;
					}

					GameHandler.AddSquareToGame(square);
				}

				return new
				{
					Configuration = Configuration,
					ServerFull = false,
					SquareID = UserHandler.GetUserSquare(connectionId).ID,
					SquareName = UserHandler.GetUserSquare(connectionId).Name,
					CompressionContracts = new
					{
						PayloadContract = _payloadManager.Compressor.PayloadCompressionContract,
						SquareContract = _payloadManager.Compressor.SquareCompressionContract
						//CollidableContract = _payloadManager.Compressor.CollidableCompressionContract,
						//BulletContract = _payloadManager.Compressor.BulletCompressionContract,
					}
				};
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//ErrorLog.Instance.Log(e);
			}

			return null;
		}

		/// <summary>
		/// Retrieves the game's configuration
		/// </summary>
		/// <returns>The game's configuration</returns>
		public object initializeController(string connectionId, RegisteredClient rc)
		{
			if (UserHandler.UserExistsAndReady(connectionId))
				return null;

			try
			{
				User main = UserHandler.FindUserByIdentity(rc.Identity);

				if (main != null)
				{
					User controllerUser = new User(connectionId, rc) { Controller = true };

					controllerUser.MySquare = main.MySquare;

					UserHandler.AddUser(controllerUser);
					main.RemoteControllers.Add(controllerUser);

					//main.NotificationManager.Notify("Controller attached.");

					return new
					{
						Configuration = Configuration,
					};
				}
				else
				{
					return new
					{
						FailureMessage = "Could not find logged in user to control."
					};
				}
			}
			catch (Exception e)
			{
				//ErrorLog.Instance.Log(e);
			}

			return null;
		}
	}
}