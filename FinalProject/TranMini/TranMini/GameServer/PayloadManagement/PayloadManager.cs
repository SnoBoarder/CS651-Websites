using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TranMini.GameServer
{
	public class PayloadManager
	{
		public PayloadCompressor Compressor = new PayloadCompressor();

		public ConcurrentDictionary<string, object[]> GetGamePayloads(ICollection<User> userList, int playerCount, World world)
		{
			ConcurrentDictionary<string, object[]> payloads = new ConcurrentDictionary<string, object[]>();

			Parallel.ForEach(userList, user =>
			{
				if (user.ReadyForPayloads && user.Connected)
				{
					string connectionID = user.ConnectionID;

					var payload = new Payload();

					if (true)
					{
						List<Collidable> onScreen = world.objects;
						foreach (Collidable obj in onScreen)
						{
							if (obj is Square)
							{
								payload.Squares.Add(Compressor.Compress(((Square)obj)));
							}
							else if (obj is Enemy)
							{
								payload.Enemies.Add(Compressor.Compress((Enemy)obj));
							}
						}
					}

					// This is used to send down "death" data a single time to the client and not send it repeatedly
					if (user.DeathOccured)
					{
						// We've acknowledged the death
						user.DeathOccured = false;
					}

					if (user.Connected)
					{
						payloads.TryAdd(connectionID, Compressor.Compress(payload));
					}
				}
			});

			return payloads;
		}
	}
}