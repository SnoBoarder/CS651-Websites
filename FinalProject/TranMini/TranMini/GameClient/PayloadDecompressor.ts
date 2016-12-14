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
            //this.CollidableContract = contracts.CollidableContract;
            //this.EnemyContract = contracts.EnemyContract;
        }

        //private DecompressCollidable(obj: any[]): ICollidableData {
        //    return {
        //        Collided: !!obj[this.CollidableContract.Collided],
        //        CollidedAt: new eg.Vector2d(obj[this.CollidableContract.CollidedAtX], obj[this.CollidableContract.CollidedAtY]),
        //        MovementController: {
        //            Forces: new eg.Vector2d(obj[this.CollidableContract.ForcesX], obj[this.CollidableContract.ForcesY]),
        //            Mass: obj[this.CollidableContract.Mass],
        //            Position: new eg.Vector2d(obj[this.CollidableContract.PositionX], obj[this.CollidableContract.PositionY]),
        //            Rotation: obj[this.CollidableContract.Rotation] * .0174532925,
        //            Velocity: new eg.Vector2d(obj[this.CollidableContract.VelocityX], obj[this.CollidableContract.VelocityY])
        //        },
        //        LifeController: {
        //            Alive: obj[this.CollidableContract.Alive],
        //            Health: obj[this.CollidableContract.Health]
        //        },
        //        ID: obj[this.CollidableContract.ID],
        //        Disposed: !!obj[this.CollidableContract.Disposed]
        //    };
        //}

        //private DecompressShip(ship: any): ISquareData {
        //    var result: ISquareData = <ISquareData>this.DecompressCollidable(ship);

        //    result.MovementController.Position = result.MovementController.Position.Add(Ship.SIZE.Multiply(.5));

        //    result.MovementController.Moving = {
        //        RotatingLeft: !!ship[this.SquareContract.RotatingLeft],
        //        RotatingRight: !!ship[this.SquareContract.RotatingRight],
        //        Forward: !!ship[this.SquareContract.Forward],
        //        Backward: !!ship[this.SquareContract.Backward]
        //    };

        //    result.Name = ship[this.SquareContract.Name];

        //    return result;
        //}

        //private DecompressBullet(bullet: any): IBulletData {
        //    var result: IBulletData = <IBulletData>this.DecompressCollidable(bullet);

        //    result.DamageDealt = bullet[this.BulletContract.DamageDealt];

        //    return result;
        //}

        public DecompressPayload(data: any): IPayloadData {
            return {
                Squares: data[this.PayloadContract.Squares],
                Kills: data[this.PayloadContract.Kills],
                Deaths: data[this.PayloadContract.Deaths]
                //SqauresInWorld: data[this.PayloadContract.SqauresInWorld],
                //EnemiesInWorld: data[this.PayloadContract.EnemiesInWorld],
                //LastCommandProcessed: data[this.PayloadContract.LastCommandProcessed]
            };
        }

        public Decompress(data: any): IPayloadData {
            var payload: IPayloadData = this.DecompressPayload(data);
            var i: number = 0;

            //for (i = 0; i < payload.Squares.length; i++) {
            //    payload.Squares[i] = this.DecompressSquare(payload.Squares[i]);
            //}

            //for (i = 0; i < payload.Bullets.length; i++) {
            //    payload.Bullets[i] = this.DecompressBullet(payload.Bullets[i]);
            //}

            return payload;
        }
    }

}