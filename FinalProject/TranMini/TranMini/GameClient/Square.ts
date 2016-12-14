/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="SquareGraphic.ts" />
// <reference path="Animations/ShipAnimationHandler.ts" />

module TranMini {

    export class Square {
        //public static SIZE: eg.Size2d = new eg.Size2d(75);

        public ID: number;
        public Graphic: SquareGraphic;
        //public MovementController: ShipMovementController;
        //public AbilityHandler: ShipAbilityHandler;
        //public AnimationHandler: ShipAnimationHandler;
        //public LifeController: ShipLifeController;
        //public LevelManager: ShipLevelManager;

        private _destroyed: boolean;

        constructor(game: Phaser.Game, payload: Server.ISquareData) {
            this._destroyed = false;

            this.Graphic = new SquareGraphic(game, payload.Name, payload.UserControlled); // TODO: Add position information if needed

            //this.Graphic = new ShipGraphic(payload.Name, payload.UserControlled, this.LevelManager, this.LifeController, payload.MovementController.Position, payload.MovementController.Rotation, Ship.SIZE, contentManager);

            // Going to use the rectangle to "hold" all the other graphics
            //super(this.Graphic.GetDrawBounds());

            //this.LoadPayload(payload, true);

            //this.Graphic.RotateShip(this.MovementController.Rotation);
        }

        //public Update(gameTime: eg.GameTime): void {
        //    this.AbilityHandler.Update(gameTime);
        //    this.MovementController.Update(gameTime);
        //    this.AnimationHandler.Update(gameTime);

        //    // Updates rotation
        //    this.Graphic.RotateShip(this.MovementController.Rotation);
        //    this.Graphic.Update(gameTime);
        //}

        public LoadPayload(payload: Server.ISquareData): void {
            this.ID = payload.ID;
        }

        public Destroy(): void {
            if (!this._destroyed) {
                this._destroyed = true;

                //this.Graphic.Dispose();
                //this.Dispose();
            }
        }
    }
}