/// <reference path="IConfigurationDefinitions.ts" />
/// <reference path="LatencyResolver.ts" />
/// <reference path="Game.ts" />
/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
// <reference path="Ship.ts" />
// <reference path="UserShipManager.ts" />

module TranMini {

    export class ConfigurationManager {
        public gameConfig: Server.IGameConfiguration;
        public mapConfig: Server.IMapConfiguration;
        public screenConfig: Server.IScreenConfiguration;
        public squareConfig: Server.ISquareConfiguration;
        public enemyConfig: Server.IEnemyConfiguration;

        constructor(configuration: Server.IConfigurationManager) {
            // Update the prototypes from the config
            //Ship.SIZE = new eg.Size2d(configuration.shipConfig.WIDTH, configuration.shipConfig.HEIGHT);

            //Map.SIZE = new eg.Size2d(configuration.mapConfig.WIDTH, configuration.mapConfig.HEIGHT);
            //Map.BARRIER_DEPRECATION = configuration.mapConfig.BARRIER_DEPRECATION;

            //GameScreen.MAX_SCREEN_HEIGHT = configuration.screenConfig.MAX_SCREEN_HEIGHT;
            //GameScreen.MAX_SCREEN_WIDTH = configuration.screenConfig.MAX_SCREEN_WIDTH;
            //GameScreen.MIN_SCREEN_HEIGHT = configuration.screenConfig.MIN_SCREEN_HEIGHT;
            //GameScreen.MIN_SCREEN_WIDTH = configuration.screenConfig.MIN_SCREEN_WIDTH;
            //GameScreen.SCREEN_BUFFER_AREA = configuration.screenConfig.SCREEN_BUFFER_AREA;

            $.extend(this, configuration);
            LatencyResolver.REQUEST_PING_EVERY = configuration.gameConfig.REQUEST_PING_EVERY;
        }
    }
}