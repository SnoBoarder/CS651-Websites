using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranMini.GameServer.CompressionContracts;

namespace TranMini.GameServer
{
	public class PayloadCompressor
	{
		public PayloadCompressionContract PayloadCompressionContract = new PayloadCompressionContract();
		public SquareCompressionContract SquareCompressionContract = new SquareCompressionContract();
		public CollidableCompressionContract CollidableCompressionContract = new CollidableCompressionContract();

		private void SetCollidableContractMembers(object[] result, Collidable obj)
		{
			result[CollidableCompressionContract.Collided] = Convert.ToInt32(obj.Collided);
			result[CollidableCompressionContract.ID] = obj.ID;
			result[CollidableCompressionContract.Disposed] = Convert.ToInt32(obj.Disposed);
		}

		public object[] Compress(Square square)
		{
			object[] result = new object[5];

			SetCollidableContractMembers(result, square);

			int jumpDuration = 0;
			if (square.JumpDuration > 0)
			{
				jumpDuration = square.JumpDuration;
				square.JumpDuration = 0;
			}

			result[SquareCompressionContract.Jump] = jumpDuration;
			result[SquareCompressionContract.Name] = square.Name;

			return result;
		}

		public object[] Compress(Payload payload)
		{
			object[] result = new object[3];
			//if (payload.KilledByName != null)
			//{
			//	result = new object[14];
			//	result[PayloadCompressionContract.KilledByName] = payload.KilledByName;
			//	result[PayloadCompressionContract.KilledByPhoto] = payload.KilledByPhoto;
			//}

			result[PayloadCompressionContract.Squares] = payload.Squares;
			result[PayloadCompressionContract.Kills] = payload.Kills;
			result[PayloadCompressionContract.Deaths] = payload.Deaths;

			return result;
		}
	}
}