using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer
{
	public class UserHandler
	{
		private GameHandler _gameHandler;
		private ConcurrentDictionary<string, User> _userList;

		public int TotalActiveUsers { get; set; }

		public UserHandler(GameHandler gameHandler)
		{
			_userList = new ConcurrentDictionary<string, User>();
			_gameHandler = gameHandler;
			TotalActiveUsers = 0;
		}

		public bool UserExistsAndReady(string connectionId)
		{
			return _userList.ContainsKey(connectionId) && _userList[connectionId].MySquare != null;
		}

		public void RemoveUser(string connectionId)
		{
			User u;
			_userList.TryRemove(connectionId, out u);
			if (!u.Controller && u.MySquare != null)
			{
				u.MySquare.Dispose();
				u.MySquare.Host = null; // Remove linking from the square
			}
		}

		public void ReassignUser(string connectionId, User user)
		{
			_userList.TryRemove(user.ConnectionID, out user);
			user.ConnectionID = connectionId;
			_userList.TryAdd(connectionId, user);
		}

		public void DisconnectUser(User user)
		{
			RemoveUser(user.ConnectionID);
			foreach (User u in user.RemoteControllers)
			{
				DisconnectUser(u);
			}

			if (user.Connected)
			{
				Game.GetContext().Clients.Client(user.ConnectionID).disconnect();
			}

			user.Connected = false;
		}

		public void AddUser(User user)
		{
			_userList.TryAdd(user.ConnectionID, user);
			user.OnIdle += DisconnectUser;
			//user.IdleManager.OnIdle += _gameHandler.RemoveShipFromGame;
			//user.IdleManager.OnIdleTimeout += DisconnectUser;
			//user.IdleManager.OnComeBack += _gameHandler.AddShipToGame;

			//if (!user.Controller)
			//{
			//	user.MyShip.OnFire += _gameHandler.AddBulletToGame;
			//}
		}

		public User GetUser(string connectionId)
		{
			return _userList[connectionId];
		}

		public Square GetUserSquare(string connectionId)
		{
			return _userList[connectionId].MySquare;
		}

		public User FindUserByIdentity(string identity)
		{
			return (from user in _userList.Values
					where user.RegistrationTicket.Identity == identity && !user.Controller
					select user).FirstOrDefault();
		}

		public List<User> GetUsers()
		{
			return _userList.Values.ToList();
		}

		//public List<User> GetActiveUsers()
		//{
		//	List<User> activeUsers = (from user in _userList.Values
		//							  where !user.Controller && user.Connected && !user.IdleManager.Idle
		//							  select user).ToList();

		//	TotalActiveUsers = activeUsers.Count;

		//	return activeUsers;
		//}

		public ICollection<string> GetUserConnectionIds()
		{
			return _userList.Keys;
		}

		public void Update()
		{
			foreach (User user in _userList.Values)
			{
				user.Update();
			}
		}
	}
}