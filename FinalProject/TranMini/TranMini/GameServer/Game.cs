using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranMini.GameServer
{
    public class Game
    {
        private readonly static Lazy<Game> _instance = new Lazy<Game>(() => new Game());

        public static Game Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        //public UserHandler UserHandler { get; private set; }

        private Game()
        {
            // private constructor since this should be a singleton

        }
    }
}