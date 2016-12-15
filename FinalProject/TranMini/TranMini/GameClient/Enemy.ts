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

            // Bullets been alive too long
            //if ((new Date().getTime() - this._spawnedAt) >= Bullet.BULLET_DIE_AFTER.Milliseconds) {
            //    this.Destroy(false);
            //}
        }

        public LoadPayload(payload: Server.IEnemyData): void {
            this.ID = payload.ID;

            // Ensure that our position matches the movement controllers position
            this.Graphic.LoadPayload(payload);
        }

        public Destroy(explode: boolean = true): void {
            if (!this._destroyed) {
                this._destroyed = true;

                //this.MovementController.Dispose();

                if (!explode) {
                    //this.Graphic.Dispose();
                    //this.Dispose();
                } else {
                    // We rely on the completion of the explosion to finish disposing the bounds and graphic
                    this.OnExplosion.Trigger();
                }
            }
        }
    }

}