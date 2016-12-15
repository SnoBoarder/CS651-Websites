using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer.CompressionContracts
{
	public class SquareCompressionContract
	{
		// setting after last CollidableCompressionContract item
		public short Jump = 7;
		public short Name = 8;
		public short CurrentScore = 9;
		public short HighScore = 10; 
	}
}