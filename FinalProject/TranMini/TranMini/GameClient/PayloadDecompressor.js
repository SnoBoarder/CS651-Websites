/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="Square.ts" />
var TranMini;
(function (TranMini) {
    var Server;
    (function (Server) {
        var PayloadDecompressor = (function () {
            function PayloadDecompressor(contracts) {
                this.PayloadContract = contracts.PayloadContract;
                this.SquareContract = contracts.SquareContract;
                this.CollidableContract = contracts.CollidableContract;
                //this.EnemyContract = contracts.EnemyContract;
            }
            PayloadDecompressor.prototype.DecompressCollidable = function (obj) {
                return {
                    Collided: !!obj[this.CollidableContract.Collided],
                    ID: obj[this.CollidableContract.ID],
                    Disposed: !!obj[this.CollidableContract.Disposed]
                };
            };
            PayloadDecompressor.prototype.DecompressSquare = function (square) {
                var result = this.DecompressCollidable(square);
                //result.MovementController.Position = result.MovementController.Position.Add(Ship.SIZE.Multiply(.5));
                //result.MovementController.Moving = {
                //    RotatingLeft: !!square[this.SquareContract.RotatingLeft],
                //    RotatingRight: !!square[this.SquareContract.RotatingRight],
                //    Forward: !!square[this.SquareContract.Forward],
                //    Backward: !!square[this.SquareContract.Backward]
                //};
                result.Jump = square[this.SquareContract.Jump];
                result.UserControlled = square[this.SquareContract.UserControlled];
                result.Name = square[this.SquareContract.Name];
                return result;
            };
            //private DecompressBullet(bullet: any): IBulletData {
            //    var result: IBulletData = <IBulletData>this.DecompressCollidable(bullet);
            //    result.DamageDealt = bullet[this.BulletContract.DamageDealt];
            //    return result;
            //}
            PayloadDecompressor.prototype.DecompressPayload = function (data) {
                return {
                    Squares: data[this.PayloadContract.Squares],
                    Kills: data[this.PayloadContract.Kills],
                    Deaths: data[this.PayloadContract.Deaths]
                };
            };
            PayloadDecompressor.prototype.Decompress = function (data) {
                var payload = this.DecompressPayload(data);
                var i = 0;
                for (i = 0; i < payload.Squares.length; i++) {
                    payload.Squares[i] = this.DecompressSquare(payload.Squares[i]);
                }
                //for (i = 0; i < payload.Bullets.length; i++) {
                //    payload.Bullets[i] = this.DecompressBullet(payload.Bullets[i]);
                //}
                return payload;
            };
            return PayloadDecompressor;
        }());
        Server.PayloadDecompressor = PayloadDecompressor;
    })(Server = TranMini.Server || (TranMini.Server = {}));
})(TranMini || (TranMini = {}));
