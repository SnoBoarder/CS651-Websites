declare module TranMini.Server {

    //export interface IMovementControllerData {
    //    Forces: eg.Vector2d;
    //    Mass: number;
    //    Rotation: number;
    //    Position: eg.Vector2d;
    //    Velocity: eg.Vector2d;
    //}

    export interface ICollidableData {
        Collided: boolean;
        //CollidedAt: eg.Vector2d;
        //MovementController: IMovementControllerData;
        //LifeController: ILifeControllerData;
        ID: number;
        Disposed: boolean;
    }

    export interface ISquareData extends ICollidableData {
        Name: string;
        Jump: number;
        UserControlled?: boolean;
    }

    export interface IBulletData extends ICollidableData {
        DamageDealt: number;
    }

    export interface IPayloadData {
        Squares: Array<ISquareData>;
        //Bullets: Array<IBulletData>;
        Kills: number;
        Deaths: number;
        //SquaresInWorld: number;
        //EnemiesInWorld: number;
    }
}
