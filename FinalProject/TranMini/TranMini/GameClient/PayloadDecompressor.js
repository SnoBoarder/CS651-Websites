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
                this.EnemyContract = contracts.EnemyContract;
            }
            PayloadDecompressor.prototype.DecompressCollidable = function (obj) {
                return {
                    Collided: !!obj[this.CollidableContract.Collided],
                    ID: obj[this.CollidableContract.ID],
                    Disposed: !!obj[this.CollidableContract.Disposed],
                    X: obj[this.CollidableContract.X],
                    Y: obj[this.CollidableContract.Y],
                    Width: obj[this.CollidableContract.Width],
                    Height: obj[this.CollidableContract.Height]
                };
            };
            PayloadDecompressor.prototype.DecompressSquare = function (square) {
                var result = this.DecompressCollidable(square);
                result.Jump = square[this.SquareContract.Jump];
                result.UserControlled = square[this.SquareContract.UserControlled];
                result.Name = square[this.SquareContract.Name];
                result.CurrentScore = square[this.SquareContract.CurrentScore];
                result.HighScore = square[this.SquareContract.HighScore];
                return result;
            };
            PayloadDecompressor.prototype.DecompressEnemy = function (enemy) {
                var result = this.DecompressCollidable(enemy);
                result.Name = enemy[this.EnemyContract.Name];
                result.Speed = enemy[this.EnemyContract.Speed];
                return result;
            };
            PayloadDecompressor.prototype.DecompressPayload = function (data) {
                return {
                    Squares: data[this.PayloadContract.Squares],
                    Enemies: data[this.PayloadContract.Enemies],
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
                for (i = 0; i < payload.Enemies.length; i++) {
                    payload.Enemies[i] = this.DecompressEnemy(payload.Enemies[i]);
                }
                return payload;
            };
            return PayloadDecompressor;
        }());
        Server.PayloadDecompressor = PayloadDecompressor;
    })(Server = TranMini.Server || (TranMini.Server = {}));
})(TranMini || (TranMini = {}));
