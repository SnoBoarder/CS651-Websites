using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer
{
	public class Payload
	{
		public Payload()
		{
			Squares = new List<object>();
			Enemies = new List<object>();
		}

		public List<object> Squares { get; set; }
		public List<object> Enemies { get; set; }
		public int Kills { get; set; }
		public int Deaths { get; set; }
	}
}