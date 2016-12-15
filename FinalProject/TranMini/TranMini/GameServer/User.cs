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
			RemoteControllers = new List<User>();
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

		public void Update()
		{
		}
	}
}