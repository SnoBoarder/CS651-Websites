/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="Square.ts" />

module TranMini.Server {

    export class PayloadDecompressor {
        public PayloadContract: any;
        public CollidableContract: any;
        public SquareContract: any;
        public EnemyContract: any;

        constructor(contracts: any) {
            this.PayloadContract = contracts.PayloadContract;
            this.SquareContract = contracts.SquareContract;
            this.CollidableContract = contracts.CollidableContract;
            this.EnemyContract = contracts.EnemyContract;
        }

        private DecompressCollidable(obj: any[]): ICollidableData {
            return {
                Collided: !!obj[this.CollidableContract.Collided],
                ID: obj[this.CollidableContract.ID],
                Disposed: !!obj[this.CollidableContract.Disposed],
                X: obj[this.CollidableContract.X],
                Y: obj[this.CollidableContract.Y],
                Width: obj[this.CollidableContract.Width],
                Height: obj[this.CollidableContract.Height]
            };
        }

        private DecompressSquare(square: any): ISquareData {
            var result: ISquareData = <ISquareData>this.DecompressCollidable(square);

            result.Jump = square[this.SquareContract.Jump];
            result.UserControlled = square[this.SquareContract.UserControlled];
            result.Name = square[this.SquareContract.Name];
            result.CurrentScore = square[this.SquareContract.CurrentScore];
            result.HighScore = square[this.SquareContract.HighScore];

            return result;
        }

        private DecompressEnemy(enemy: any): IEnemyData {
            var result: IEnemyData = <IEnemyData>this.DecompressCollidable(enemy);

            result.Name = enemy[this.EnemyContract.Name];
            result.Speed = enemy[this.EnemyContract.Speed];

            return result;
        }

        public DecompressPayload(data: any): IPayloadData {
            return {
                Squares: data[this.PayloadContract.Squares],
                Enemies: data[this.PayloadContract.Enemies],
                Kills: data[this.PayloadContract.Kills],
                Deaths: data[this.PayloadContract.Deaths]
            };
        }

        public Decompress(data: any): IPayloadData {
            var payload: IPayloadData = this.DecompressPayload(data);
            var i: number = 0;

            for (i = 0; i < payload.Squares.length; i++) {
                payload.Squares[i] = this.DecompressSquare(payload.Squares[i]);
            }

            for (i = 0; i < payload.Enemies.length; i++) {
                payload.Enemies[i] = this.DecompressEnemy(payload.Enemies[i]);
            }

            return payload;
        }
    }

}