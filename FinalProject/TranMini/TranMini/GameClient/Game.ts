/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="../Scripts/phaser.d.ts" />
/// <reference path="ServerAdapter.ts" />

module ShootR {
    export class Game {

        private game: Phaser.Game;

        constructor() {
            this.game = new Phaser.Game(800, 600, Phaser.AUTO, 'game', { preload: this.preload, create: this.create });
        }

        preload() {
            this.game.load.image('logo', '../Images/phaser-logo-small.png');
        }

        create() {
            var logo = this.game.add.sprite(this.game.world.centerX, this.game.world.centerY, 'logo');
            logo.anchor.setTo(0, 0);
        }
    }

    var game = new Game();
}