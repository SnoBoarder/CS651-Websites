/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="SquareGraphic.ts" />
// <reference path="Animations/ShipAnimationHandler.ts" />
var TranMini;
(function (TranMini) {
    var Square = (function () {
        function Square(game, payload) {
            this._destroyed = false;
            this.Graphic = new TranMini.SquareGraphic(game, payload);
        }
        Square.prototype.LoadPayload = function (payload) {
            this.ID = payload.ID;
            this.Graphic.LoadPayload(payload);
        };
        Square.prototype.Destroy = function () {
            if (!this._destroyed) {
                this._destroyed = true;
                this.Graphic.Dispose();
                this.Graphic = null;
            }
        };
        return Square;
    }());
    TranMini.Square = Square;
})(TranMini || (TranMini = {}));
