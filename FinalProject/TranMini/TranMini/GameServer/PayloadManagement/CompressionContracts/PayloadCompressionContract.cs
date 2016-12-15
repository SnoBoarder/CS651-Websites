using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer.CompressionContracts
{
	public class PayloadCompressionContract
	{
		public short Squares = 0;
		public short Enemies = 1;
		public short Kills = 2;
		public short Deaths = 3;
	}
}