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
		public EnemyCompressionContract EnemyCompressionContract = new EnemyCompressionContract();
		public CollidableCompressionContract CollidableCompressionContract = new CollidableCompressionContract();

		private void SetCollidableContractMembers(object[] result, Collidable obj)
		{
			result[CollidableCompressionContract.Collided] = Convert.ToInt32(obj.Collided);
			result[CollidableCompressionContract.ID] = obj.ID;
			result[CollidableCompressionContract.Disposed] = Convert.ToInt32(obj.Disposed);
			result[CollidableCompressionContract.X] = obj.Position.X;
			result[CollidableCompressionContract.Y] = obj.Position.Y;
			result[CollidableCompressionContract.Width] = obj.Width;
			result[CollidableCompressionContract.Height] = obj.Height;
		}

		public object[] Compress(Square square)
		{
			object[] result = new object[9];

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

		public object[] Compress(Enemy enemy)
		{
			object[] result = new object[8];

			SetCollidableContractMembers(result, enemy);

			result[EnemyCompressionContract.Name] = enemy.Name;

			return result;
		}

		public object[] Compress(Payload payload)
		{
			object[] result = new object[4];
			//if (payload.KilledByName != null)
			//{
			//	result = new object[14];
			//	result[PayloadCompressionContract.KilledByName] = payload.KilledByName;
			//	result[PayloadCompressionContract.KilledByPhoto] = payload.KilledByPhoto;
			//}

			result[PayloadCompressionContract.Squares] = payload.Squares;
			result[PayloadCompressionContract.Enemies] = payload.Enemies;
			result[PayloadCompressionContract.Kills] = payload.Kills;
			result[PayloadCompressionContract.Deaths] = payload.Deaths;

			return result;
		}
	}
}