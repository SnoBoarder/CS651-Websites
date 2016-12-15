using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer
{
	public class User
	{
		public delegate void IdleEventHandler(User u);
		public event IdleEventHandler OnIdle;

		public bool Connected { get; set; }
		public RegisteredClient RegistrationTicket { get; set; }
		public List<User> RemoteControllers { get; set; }
		//public IdleManager IdleManager { get; private set; }
		public string ConnectionID { get; set; }
		public Square MySquare { get; set; }
		public bool Controller { get; set; }
		public bool ReadyForPayloads { get; set; }
		public int CurrentLeaderboardPosition { get; set; }
		public bool DeathOccured { get; set; }

		public User(string connectionID, RegisteredClient rc)
			: this(connectionID, null, rc)
		{
		}

		public User(string connectionID, Square square, RegisteredClient rc)
		{
			RegistrationTicket = rc;
			ConnectionID = connectionID;
			MySquare = square;

			MySquare.OnSuperFail += OnSuperFail;

			ReadyForPayloads = false;
			//Viewport = new Size(0, 0); // Initialize the viewport to 0 by 0
			RemoteControllers = new List<User>();
			//IdleManager = new IdleManager(square, NotificationManager);
			Connected = true;

			if (square != null)
			{
				square.Host = this;
			}
		}

		private void OnSuperFail()
		{
			OnIdle?.Invoke(this);
		}

		public virtual void PushToClient(object[] payload, IHubContext context)
		{
			context.Clients.Client(ConnectionID).d(payload);
		}

		//private Size _viewport;
		//public Size Viewport
		//{
		//	get
		//	{
		//		return _viewport;
		//	}
		//	set
		//	{
		//		if (value.Width > MAX_SCREEN_WIDTH)
		//		{
		//			value.Width = MAX_SCREEN_WIDTH;
		//		}
		//		if (value.Height > MAX_SCREEN_HEIGHT)
		//		{
		//			value.Height = MAX_SCREEN_HEIGHT;
		//		}

		//		_viewport = value;
		//	}
		//}

		public void Update()
		{
			if (MySquare != null)
			{
				//IdleManager.Update();
			}
		}
	}
}