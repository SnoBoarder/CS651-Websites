/// <reference path="../Scripts/endgate-0.2.0.d.ts" />
/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="EnemyGraphic.ts" />

module TranMini {

    export class Enemy {
        public static SIZE: eg.Size2d = new eg.Size2d(13);
        public static BULLET_DIE_AFTER: eg.TimeSpan = eg.TimeSpan.FromSeconds(2);

        public ID: number;
        public Graphic: EnemyGraphic;

        private _spawnedAt: number;
        private _destroyed: boolean;

        constructor(private _game: Phaser.Game, payload: Server.IEnemyData) {
            // Going to use the rectangle to "hold" all the other graphics
            this.Graphic = new EnemyGraphic(this._game, payload);

            this.OnExplosion = new eg.EventHandler();

            this._spawnedAt = new Date().getTime();
            this._destroyed = false;

            this.LoadPayload(payload);
        }

        public OnExplosion: eg.EventHandler;

        public Update(gameTime: eg.GameTime): void {
        }

        public LoadPayload(payload: Server.IEnemyData): void {
            this.ID = payload.ID;

            // Ensure that our position matches the movement controllers position
            this.Graphic.LoadPayload(payload);
        }

        public Destroy(): void {
            if (!this._destroyed) {
                this._destroyed = true;
            }
        }
    }
}