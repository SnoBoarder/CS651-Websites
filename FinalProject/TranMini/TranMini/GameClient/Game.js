/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="../Scripts/phaser.d.ts" />
/// <reference path="ServerAdapter.ts" />
var ShootR;
(function (ShootR) {
    var Game = (function () {
        function Game() {
            this.game = new Phaser.Game(800, 600, Phaser.AUTO, 'game', { preload: this.preload, create: this.create });
        }
        Game.prototype.preload = function () {
            this.game.load.image('logo', '../Images/phaser-logo-small.png');
        };
        Game.prototype.create = function () {
            var logo = this.game.add.sprite(this.game.world.centerX, this.game.world.centerY, 'logo');
            logo.anchor.setTo(0, 0);
        };
        return Game;
    }());
    ShootR.Game = Game;
    var game = new Game();
})(ShootR || (ShootR = {}));
//# sourceMappingURL=Game.js.map