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
            //this._shipInputController = new ShipInputController(input.Keyboard, (direction: string, startMoving: boolean) =>{
            //    var ship = this._shipManager.GetShip(this.ControlledShipId);
            //    if (ship && ship.MovementController.Controllable && ship.LifeController.Alive) {
            //        if (startMoving) {
            //            if (direction === "Boost") {
            //                this.Invoke("registerAbilityStart", this.LatencyResolver.TryRequestPing(), this.NewAbilityCommand(direction, true));
            //                ship.AbilityHandler.Activate(direction);
            //                // Don't want to trigger a server command if we're already moving in the direction
            //            } else if (!ship.MovementController.IsMovingInDirection(direction)) {
            //                this.Invoke("registerMoveStart", this.LatencyResolver.TryRequestPing(), this.NewMovementCommand(direction, true));
            //                ship.MovementController.Move(direction, startMoving);
            //            }
            //        } else {
            //            // Don't want to trigger a server command if we're already moving in the direction
            //            if (ship.MovementController.IsMovingInDirection(direction)) {
            //                this.Invoke("registerMoveStop", this.LatencyResolver.TryRequestPing(), this.NewMovementCommand(direction, false));
            //                ship.MovementController.Move(direction, startMoving);
            //            }
            //        }
            //    }
            //}, (fireMethod: string) => {
            //    var hubMethod: string = fireMethod.substr(0, 1).toUpperCase() + fireMethod.substring(1);
            //    this._proxy.invoke(hubMethod);
            //});
        }
        //public LoadPayload(payload: Server.IPayloadData): void {
        //    var ship: Square = this._shipManager.GetShip(this.ControlledShipId);
        //}
        //public Update(gameTime: eg.GameTime): void {
        //    var ship = this._shipManager.GetShip(this.ControlledShipId);
        //    if (ship) {
        //        if (eg.TimeSpan.DateSpan(this._lastSync, gameTime.Now).Seconds > UserShipManager.SYNC_INTERVAL.Seconds && ship.LifeController.Alive) {
        //            this._lastSync = gameTime.Now;
        //            this._proxy.invoke("syncMovement", { X: Math.round(ship.MovementController.Position.X - ship.Graphic.Size.HalfWidth), Y: Math.round(ship.MovementController.Position.Y - ship.Graphic.Size.HalfHeight) }, Math.roundTo(ship.MovementController.Rotation * 57.2957795, 2), { X: Math.round(ship.MovementController.Velocity.X), Y: Math.round(ship.MovementController.Velocity.Y) });
        //        }
        //        this._userCameraController.Update(gameTime);
        //    }
        //}
        UserSquareManager.prototype.Jump = function () {
            this.Invoke("registerJump", this.LatencyResolver.TryRequestPing());
        };
        UserSquareManager.prototype.Invoke = function (method, pingBack) {
            var square = this._squareManager.GetSquare(this.ControlledSquareId);
            this._proxy.invoke(method, pingBack);
            //this._proxy.invoke(method, command.Command, { X: Math.round(ship.MovementController.Position.X - ship.Graphic.Size.HalfWidth), Y: Math.round(ship.MovementController.Position.Y - ship.Graphic.Size.HalfHeight) }, Math.roundTo(ship.MovementController.Rotation * 57.2957795, 2), { X: Math.round(ship.MovementController.Velocity.X), Y: Math.round(ship.MovementController.Velocity.Y) }, pingBack);
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
