using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using TranMini.GameServer;
using Microsoft.AspNet.SignalR.Hubs;

namespace TranMini.GameServer
{
	[HubName("game")]
	public class GameHub : Hub
    {
		private readonly Game _game;

		public GameHub() : this(Game.Instance) { }

		public GameHub(Game game)
		{
			_game = game;
		}

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

		#region Connection Methods

		public override Task OnConnected()
		{
			_game.ConnectionManager.OnConnected(Context.ConnectionId);
			return base.OnConnected();
		}

		public override Task OnReconnected()
		{
			_game.ConnectionManager.OnReconnected(Context.ConnectionId);
			return base.OnReconnected();
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			_game.ConnectionManager.OnDisconnected(Context.ConnectionId);
			return base.OnDisconnected(stopCalled);
		}

		#endregion

		#region Client Accessor Methods

		public DateTime ping()
		{
			return DateTime.UtcNow;
		}

		/// <summary>
		/// Retrieves the game's configuration
		/// </summary>
		/// <returns>The game's configuration</returns>
		public object initializeClient(string registrationID)
		{
			if (_game.RegistrationHandler.RegistrationExists(registrationID))
			{
				return _game.initializeClient(Context.ConnectionId, _game.RegistrationHandler.RemoveRegistration(registrationID));
			}

			return null;
		}

		/// <summary>
		/// Retrieves the game's configuration
		/// </summary>
		/// <returns>The game's configuration</returns>
		public object initializeController(string registrationID)
		{
			if (_game.RegistrationHandler.RegistrationExists(registrationID))
			{
				return _game.initializeController(Context.ConnectionId, _game.RegistrationHandler.RemoveRegistration(registrationID));
			}

			return null;
		}

		public void readyForPayloads()
		{
			try
			{
				_game.UserHandler.GetUser(Context.ConnectionId).ReadyForPayloads = true;
			}
			catch (Exception e)
			{
				//ErrorLog.Instance.Log(e);
			}
		}

		public void registerJump(bool pingBack)
		{
			if (_game.UserHandler.UserExistsAndReady(Context.ConnectionId))
			{
				try
				{
					if (pingBack)
					{
						Clients.Caller.pingBack();
					}

					Square square = _game.UserHandler.GetUserSquare(Context.ConnectionId);

					square.Jump();

					//if (square.Controllable.Value)
					//{
					//	square.ActivateAbility(abilityName, at, angle, velocity);
					//}
				}
				catch (Exception e)
				{
					//ErrorLog.Instance.Log(e);
				}
			}
		}

		//public void changeViewport(int viewportWidth, int viewportHeight)
		//{
		//	try
		//	{
		//		if (_game.UserHandler.UserExistsAndReady(Context.ConnectionId))
		//		{
		//			_game.UserHandler.GetUser(Context.ConnectionId).Viewport = new Size(viewportWidth, viewportHeight);
		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		ErrorLog.Instance.Log(e);
		//	}
		//}

		#endregion
	}
}