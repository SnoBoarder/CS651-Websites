/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="SquareGraphic.ts" />
// <reference path="Animations/ShipAnimationHandler.ts" />

module TranMini {

    export class Square {
        public ID: number;
        public Graphic: SquareGraphic;

        private _destroyed: boolean;

        constructor(game: Phaser.Game, payload: Server.ISquareData) {
            this._destroyed = false;
            this.Graphic = new SquareGraphic(game, payload);
        }

        public LoadPayload(payload: Server.ISquareData): void {
            this.ID = payload.ID;

            this.Graphic.LoadPayload(payload);
        }

        public Destroy(): void {
            if (!this._destroyed) {
                this._destroyed = true;

                this.Graphic.Dispose();
                this.Graphic = null;
            }
        }
    }
}