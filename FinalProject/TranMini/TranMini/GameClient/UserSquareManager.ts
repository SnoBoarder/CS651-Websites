/// <reference path="SquareManager.ts" />
/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="ServerAdapter.ts" />
/// <reference path="LatencyResolver.ts" />
/// <reference path="Square.ts" />

module TranMini {

    export class UserSquareManager {

        public LatencyResolver: LatencyResolver;

        private _proxy: SignalR.Hub.Proxy;
        private _lastSync: Date;

        constructor(public ControlledSquareId: number, private _squareManager: SquareManager, serverAdapter: Server.ServerAdapter) {
            this._proxy = serverAdapter.Proxy;
            this._lastSync = new Date();
            this.LatencyResolver = new LatencyResolver(serverAdapter);
        }

        // send a jump message to the server for this user
        public Jump(): void {
            this.Invoke("registerJump", this.LatencyResolver.TryRequestPing());
        }

        private Invoke(method: string, pingBack: boolean): void {
            var square: Square = this._squareManager.GetSquare(this.ControlledSquareId);

            this._proxy.invoke(method, pingBack);
        }

        private NewMovementCommand(direction: string, startMoving: boolean): ISquareCommand {
            var command: ISquareCommand = {
                Command: direction,
                Start: startMoving,
                IsAbility: false
            };

            return command;
        }
    }

    interface ISquareCommand {
        Command: string;
        Start: boolean;
        IsAbility: boolean;
    }

}