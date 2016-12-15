/// <reference path="SquareManager.ts" />
/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="ServerAdapter.ts" />
/// <reference path="LatencyResolver.ts" />
/// <reference path="Square.ts" />
var TranMini;
(function (TranMini) {
    var UserSquareManager = (function () {
        function UserSquareManager(ControlledSquareId, _squareManager, serverAdapter) {
            this.ControlledSquareId = ControlledSquareId;
            this._squareManager = _squareManager;
            this._proxy = serverAdapter.Proxy;
            this._lastSync = new Date();
            this.LatencyResolver = new TranMini.LatencyResolver(serverAdapter);
        }
        // send a jump message to the server for this user
        UserSquareManager.prototype.Jump = function () {
            this.Invoke("registerJump", this.LatencyResolver.TryRequestPing());
        };
        UserSquareManager.prototype.Invoke = function (method, pingBack) {
            var square = this._squareManager.GetSquare(this.ControlledSquareId);
            this._proxy.invoke(method, pingBack);
        };
        UserSquareManager.prototype.NewMovementCommand = function (direction, startMoving) {
            var command = {
                Command: direction,
                Start: startMoving,
                IsAbility: false
            };
            return command;
        };
        return UserSquareManager;
    }());
    TranMini.UserSquareManager = UserSquareManager;
})(TranMini || (TranMini = {}));
//# sourceMappingURL=UserSquareManager.js.map