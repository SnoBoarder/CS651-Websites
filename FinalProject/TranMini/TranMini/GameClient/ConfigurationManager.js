/// <reference path="IConfigurationDefinitions.ts" />
/// <reference path="LatencyResolver.ts" />
/// <reference path="Game.ts" />
/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
var TranMini;
(function (TranMini) {
    var ConfigurationManager = (function () {
        function ConfigurationManager(configuration) {
            $.extend(this, configuration);
            TranMini.LatencyResolver.REQUEST_PING_EVERY = configuration.gameConfig.REQUEST_PING_EVERY;
        }
        return ConfigurationManager;
    }());
    TranMini.ConfigurationManager = ConfigurationManager;
})(TranMini || (TranMini = {}));
//# sourceMappingURL=ConfigurationManager.js.map