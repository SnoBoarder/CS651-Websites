﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer
{
	public class RuntimeConfiguration
	{
		public RuntimeConfiguration()
		{
			MaxServerUsers = 15;
		}

		public int MaxServerUsers { get; set; }
	}
}