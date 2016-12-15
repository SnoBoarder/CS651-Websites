/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="SquareGraphic.ts" />
// <reference path="Animations/ShipAnimationHandler.ts" />
var TranMini;
(function (TranMini) {
    var Square = (function () {
        function Square(game, payload) {
            this._destroyed = false;
            this.Graphic = new TranMini.SquareGraphic(game, payload); // TODO: Add position information if needed
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
        Square.prototype.LoadPayload = function (payload) {
            this.ID = payload.ID;
            this.Graphic.LoadPayload(payload);
        };
        Square.prototype.Destroy = function () {
            if (!this._destroyed) {
                this._destroyed = true;
            }
        };
        return Square;
    }());
    TranMini.Square = Square;
})(TranMini || (TranMini = {}));
//# sourceMappingURL=Square.js.map