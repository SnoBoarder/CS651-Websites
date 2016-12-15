/// <reference path="../Scripts/phaser.d.ts" />
/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="Square.ts" />
/// <reference path="UserSquareManager.ts" />

module TranMini {

    export class SquareManager {
        public UserSquareManager: UserSquareManager;

        private _squares: { [id: number]: Square };

        constructor(private _game: Phaser.Game) {
            this._squares = {};
        }

        public Initialize(userShipManager: UserSquareManager): void {
            this.UserSquareManager = userShipManager;
        }

        public GetSquare(id: number): Square {
            return this._squares[id];
        }

        public RemoveShip(squareID: number): void {
            delete this._squares[squareID];
        }

        public LoadPayload(payload: Server.IPayloadData): void {
            var squarePayload: Array<Server.ISquareData> = payload.Squares;
            var square: Server.ISquareData;

            for (var i = 0; i < squarePayload.length; i++) {
                square = squarePayload[i];

                if (!this._squares[square.ID]) {
                    square.UserControlled = (square.ID === this.UserSquareManager.ControlledSquareId);

                    this._squares[square.ID] = new Square(this._game, square);
                } else {
                    this._squares[square.ID].LoadPayload(square);
                }

                if (square.Disposed) {
                    this._squares[square.ID].Destroy();
                    delete this._squares[square.ID];
                }
            }
        }

        public Jump(): void {
            this.UserSquareManager.Jump();
        }
    }
}