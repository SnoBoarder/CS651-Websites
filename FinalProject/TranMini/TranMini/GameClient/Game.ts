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
        private _enemyManager: EnemyManager;

        constructor(serverAdapter: Server.ServerAdapter, initializationData: Server.IClientInitialization) {

            this.game = new Phaser.Game(700, 400, Phaser.AUTO, 'game', { preload: this.preload, create: this.create });

            Game.GameConfiguration = new ConfigurationManager(initializationData.Configuration);

            this._squareManager = new SquareManager(this.game);
            this._squareManager.Initialize(new UserSquareManager(initializationData.SquareID, this._squareManager, serverAdapter));

            this._enemyManager = new EnemyManager(this.game);

            // handles all payloads
            serverAdapter.OnPayload.Bind((payload: Server.IPayloadData) => {
                this._squareManager.LoadPayload(payload);
                this._enemyManager.LoadPayload(payload);
            });

            // input handler
            $("#game").click(()=> {
                this._squareManager.Jump();
            });
        }

        preload() {
        }

        create() {
            this.game.stage.backgroundColor = "#67B2D2";

            var style = { font: "bold 64px Arial", fill: "#fff", boundsAlignH: "center", boundsAlignV: "middle" };

            var title = this.game.add.text(0, 0, "Tran Mini Game", style);
            title.setTextBounds(0, 0, 700, 100);
        }
    }
}