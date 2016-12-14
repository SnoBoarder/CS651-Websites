using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer.CompressionContracts
{
	public class PayloadCompressionContract
	{
		public short Squares = 0;
		public short Kills = 1;
		public short Deaths = 2;
	}
}