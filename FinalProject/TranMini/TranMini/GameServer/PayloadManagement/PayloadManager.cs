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
		public const int SCREEN_BUFFER_AREA = 200; // Send X extra pixels down to the client to allow for latency between client and server

		public PayloadCompressor Compressor = new PayloadCompressor();

		public ConcurrentDictionary<string, object[]> GetGamePayloads(ICollection<User> userList, int playerCount, World world)
		{
			ConcurrentDictionary<string, object[]> payloads = new ConcurrentDictionary<string, object[]>();

			Parallel.ForEach(userList, user =>
			{
				if (user.ReadyForPayloads && user.Connected)
				{
					//Vector2 screenOffset = new Vector2((user.Viewport.Width / 2) + Ship.WIDTH / 2, (user.Viewport.Height / 2) + Ship.HEIGHT / 2);
					string connectionID = user.ConnectionID;

					var payload = new Payload();//GetInitializedPayload(playerCount, bulletCount, user);

					if (true)//(!user.IdleManager.Idle)
					{
						//Vector2 screenPosition = user.MySquare.MovementController.Position - screenOffset;
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
					//else // User is Idle, push down "MyShip"
					//{
					//	payload.Squares.Add(Compressor.Compress(user.MySquare));
					//}

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

			// Remove all disposed objects from the map
			//space.Clean();

			return payloads;
		}

		public Payload GetInitializedPayload(int playerCount, int bulletCount, User user)
		{
			return new Payload()
			{
				//Kills = user.MySquare.StatRecorder.Kills,
				//Deaths = user.MySquare.StatRecorder.Deaths,
			};
		}
	}
}