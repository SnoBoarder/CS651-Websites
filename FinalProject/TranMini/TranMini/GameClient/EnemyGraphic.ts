/// <reference path="Enemy.ts" />
/// <reference path="../Scripts/phaser.d.ts" />

module TranMini {

    export class EnemyGraphic {
        private _body: Phaser.Sprite;

        private _width: number;
        private _height: number;

        private _y: number;

        constructor(private game: Phaser.Game, payload: Server.IEnemyData) {
            this._width = payload.Width;
            this._height = payload.Height;

            this._y = payload.Y;

            var graphics = game.add.graphics(0, 0);
            graphics.beginFill(0xFF00FF, 1);
            graphics.drawRect(0, 0, this._width, this._height);
            graphics.endFill();
            this._body = game.add.sprite(0, 0, graphics.generateTexture());

            this._body.x = payload.X;
            this._body.y = payload.Y;

            graphics.destroy();
        }

        public LoadPayload(payload: Server.IEnemyData): void {

            if (payload.X != this._body.x) {
                this._body.x = payload.X;
            }
        }

        private MoveSquare(x: number) {
            // TODO: Consider animating
            this._body.x = x;
        }

        public Hide(): void {
            this._body.visible = false;
        }
    }

}