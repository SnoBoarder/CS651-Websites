/// <reference path="IConfigurationDefinitions.ts" />
/// <reference path="LatencyResolver.ts" />
/// <reference path="Game.ts" />
/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />

module TranMini {

    export class ConfigurationManager {
        public gameConfig: Server.IGameConfiguration;
        public mapConfig: Server.IMapConfiguration;
        public screenConfig: Server.IScreenConfiguration;
        public squareConfig: Server.ISquareConfiguration;
        public enemyConfig: Server.IEnemyConfiguration;

        constructor(configuration: Server.IConfigurationManager) {
            $.extend(this, configuration);
            LatencyResolver.REQUEST_PING_EVERY = configuration.gameConfig.REQUEST_PING_EVERY;
        }
    }
}