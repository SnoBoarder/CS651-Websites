using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer
{
	public class PayloadCache
	{
		ConcurrentDictionary<string, ConcurrentDictionary<int, bool>> _lastCache;
		ConcurrentDictionary<string, ConcurrentDictionary<int, bool>> _currentCache;

		public PayloadCache()
		{
			// Initiate base cache containers
			_currentCache = new ConcurrentDictionary<string, ConcurrentDictionary<int, bool>>();
			_lastCache = new ConcurrentDictionary<string, ConcurrentDictionary<int, bool>>();
		}

		public void StartNextPayloadCache()
		{
			_lastCache = _currentCache;
			_currentCache = new ConcurrentDictionary<string, ConcurrentDictionary<int, bool>>();
		}

		public void CreateCacheFor(string connectionID)
		{
			_currentCache.TryAdd(connectionID, new ConcurrentDictionary<int, bool>());
		}

		public void Cache(string connectionID, Collidable obj)
		{
			_currentCache[connectionID].TryAdd(obj.ServerID(), true);
		}

		public bool ExistedLastPayload(string connectionID, Collidable obj)
		{
			return _lastCache.ContainsKey(connectionID) && _lastCache[connectionID].ContainsKey(obj.ServerID());
		}
	}
}