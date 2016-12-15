declare module TranMini.Server {

    export interface ICollidableData {
        Collided: boolean;
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
    }
}
