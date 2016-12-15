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
            this.game = new Phaser.Game(700, 400, Phaser.AUTO, 'game', { preload: this.preload, create: this.create });
            Game.GameConfiguration = new TranMini.ConfigurationManager(initializationData.Configuration);
            this._squareManager = new TranMini.SquareManager(this.game);
            this._squareManager.Initialize(new TranMini.UserSquareManager(initializationData.SquareID, this._squareManager, serverAdapter));
            this._enemyManager = new TranMini.EnemyManager(this.game);
            serverAdapter.OnPayload.Bind(function (payload) {
                _this._squareManager.LoadPayload(payload);
                _this._enemyManager.LoadPayload(payload);
            });
            $("#game").click(function () {
                _this._squareManager.Jump();
            });
        }
        Game.prototype.preload = function () {
        };
        Game.prototype.create = function () {
            this.game.stage.backgroundColor = "#67B2D2";
            var style = { font: "bold 64px Arial", fill: "#fff", boundsAlignH: "center", boundsAlignV: "middle" };
            var title = this.game.add.text(0, 0, "Tran Mini Game", style);
            title.setTextBounds(0, 0, 700, 100);
        };
        return Game;
    }());
    TranMini.Game = Game;
})(TranMini || (TranMini = {}));
