/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="../Scripts/phaser.d.ts" />
/// <reference path="ServerAdapter.ts" />
/// <reference path="ConfigurationManager.ts" />
/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="SquareManager.ts" />
/// <reference path="UserSquareManager.ts" />
var TranMini;
(function (TranMini) {
    var Game = (function () {
        function Game(serverAdapter, initializationData) {
            var _this = this;
            var create = this.create.bind(this);
            this.game = new Phaser.Game(800, 600, Phaser.AUTO, 'game', { preload: this.preload, create: this.create });
            Game.GameConfiguration = new TranMini.ConfigurationManager(initializationData.Configuration);
            this._squareManager = new TranMini.SquareManager(this.game);
            this._squareManager.Initialize(new TranMini.UserSquareManager(initializationData.SquareID, this._squareManager, serverAdapter));
            serverAdapter.OnPayload.Bind(function (payload) {
                _this._squareManager.LoadPayload(payload);
            });
            $("#game").click(function () {
                _this._squareManager.Jump();
            });
        }
        Game.prototype.preload = function () {
            this.game.load.image('logo', '../Images/phaser-logo-small.png');
        };
        Game.prototype.create = function () {
            var logo = this.game.add.sprite(this.game.world.centerX, 0, 'logo');
            logo.anchor.setTo(.5, 0);
        };
        return Game;
    }());
    TranMini.Game = Game;
})(TranMini || (TranMini = {}));
