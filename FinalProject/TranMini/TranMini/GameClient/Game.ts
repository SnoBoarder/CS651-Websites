﻿/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="../Scripts/phaser.d.ts" />
/// <reference path="ServerAdapter.ts" />
/// <reference path="ConfigurationManager.ts" />
/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="SquareManager.ts" />
/// <reference path="UserSquareManager.ts" />


module TranMini {
    export class Game {
        public static GameConfiguration: ConfigurationManager;

        private game: Phaser.Game;

        private _squareManager: SquareManager;

        constructor(serverAdapter: Server.ServerAdapter, initializationData: Server.IClientInitialization) {

            var create = this.create.bind(this);

            this.game = new Phaser.Game(800, 600, Phaser.AUTO, 'game', { preload: this.preload, create: this.create });

            Game.GameConfiguration = new ConfigurationManager(initializationData.Configuration);

            this._squareManager = new SquareManager(this.game);
            this._squareManager.Initialize(new UserSquareManager(initializationData.SquareID, this._squareManager, serverAdapter));

            serverAdapter.OnPayload.Bind((payload: Server.IPayloadData) => {
                this._squareManager.LoadPayload(payload);
            });

            $("#game").click(()=> {
                this._squareManager.Jump();
            });
        }

        private preload() {
            this.game.load.image('logo', '../Images/phaser-logo-small.png');
        }

        private create() {

            var logo = this.game.add.sprite(this.game.world.centerX, 0, 'logo');
            logo.anchor.setTo(.5, 0);
        }
    }
}