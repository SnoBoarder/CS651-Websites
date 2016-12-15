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

        //constructor(private _viewport: eg.Bounds.BoundingRectangle, private _scene: eg.Rendering.Scene2d, private _collisionManager: eg.Collision.CollisionManager, private _contentManager: eg.Content.ContentManager) {
        //    this._ships = {};
        //}

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
        //            this._collisionManager.Monitor(this._ships[ship.ID]);
        //            this._scene.Add(this._ships[ship.ID].Graphic);

                    //this._squares[square.ID].OnDisposed.Bind((ship) => {
                    //    delete this._squares[(<Square>square).ID];
                    //});
                } else {
                    this._squares[square.ID].LoadPayload(square);
                }

                if (square.Disposed) {
                    this._squares[square.ID].Destroy();
                }
            }

            //this.UserShipManager.LoadPayload(payload);
        }

        public Jump(): void {
            this.UserSquareManager.Jump();
        }

        //public Update(gameTime: eg.GameTime): void {
        //    // Update positions first
        //    for (var id in this._ships) {
        //        this._ships[id].Update(gameTime);
        //    }

        //    this.UserShipManager.Update(gameTime);

        //    // Check for "in-bounds" to see what ships we should destroy
        //    for (var id in this._ships) {
        //        if (!this._ships[id].Bounds.IntersectsRectangle(this._viewport)) {
        //            this._ships[id].Destroy();
        //        }
        //    }
        //}
    }
}