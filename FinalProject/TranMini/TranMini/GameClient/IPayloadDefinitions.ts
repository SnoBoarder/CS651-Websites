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
        X: number;
        Y: number;
        Width: number;
        Height: number;
    }

    export interface ISquareData extends ICollidableData {
        Name: string;
        Jump: number;
        UserControlled?: boolean;
        CurrentScore: number;
        HighScore: number;
    }

    export interface IEnemyData extends ICollidableData {
        Name: string;
        Speed: number;
    }

    export interface IPayloadData {
        Squares: Array<ISquareData>;
        Enemies: Array<IEnemyData>;
        Kills: number;
        Deaths: number;
        //SquaresInWorld: number;
        //EnemiesInWorld: number;
    }
}
