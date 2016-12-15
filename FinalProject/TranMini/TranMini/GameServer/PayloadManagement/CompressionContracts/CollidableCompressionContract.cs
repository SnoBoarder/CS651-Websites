using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer.CompressionContracts
{
	public class CollidableCompressionContract
	{
		// Number members
		public short Collided = 0;
		public short ID = 1;
		public short Disposed = 2;
		public short X = 3;
		public short Y = 4;
		public short Width = 5;
		public short Height = 6;
	}
}