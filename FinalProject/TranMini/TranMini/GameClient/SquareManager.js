/// <reference path="../Scripts/phaser.d.ts" />
/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="Square.ts" />
/// <reference path="UserSquareManager.ts" />
var TranMini;
(function (TranMini) {
    var SquareManager = (function () {
        function SquareManager(_game) {
            this._game = _game;
            this._squares = {};
        }
        SquareManager.prototype.Initialize = function (userShipManager) {
            this.UserSquareManager = userShipManager;
        };
        SquareManager.prototype.GetSquare = function (id) {
            return this._squares[id];
        };
        SquareManager.prototype.RemoveShip = function (squareID) {
            delete this._squares[squareID];
        };
        SquareManager.prototype.LoadPayload = function (payload) {
            var squarePayload = payload.Squares;
            var square;
            for (var i = 0; i < squarePayload.length; i++) {
                square = squarePayload[i];
                if (!this._squares[square.ID]) {
                    square.UserControlled = (square.ID === this.UserSquareManager.ControlledSquareId);
                    this._squares[square.ID] = new TranMini.Square(this._game, square);
                }
                else {
                    this._squares[square.ID].LoadPayload(square);
                }
                if (square.Disposed) {
                    this._squares[square.ID].Destroy();
                    delete this._squares[square.ID];
                }
            }
        };
        // dispatch jump message for the user
        SquareManager.prototype.Jump = function () {
            this.UserSquareManager.Jump();
        };
        return SquareManager;
    }());
    TranMini.SquareManager = SquareManager;
})(TranMini || (TranMini = {}));
//# sourceMappingURL=SquareManager.js.map