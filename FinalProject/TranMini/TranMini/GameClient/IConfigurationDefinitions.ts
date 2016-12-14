module TranMini.Server {

    export interface ISquareConfiguration {
        WIDTH: number;
        HEIGHT: number;
    }

    export interface IEnemyConfiguration {
        WIDTH: number;
        HEIGHT: number;
    }

    export interface IGameConfiguration {
        DRAW_INTERVAL: number;
        UPDATE_INTERVAL: number;
        REQUEST_PING_EVERY: number;
    }

    export interface IMapConfiguration {
        WIDTH: number;
        HEIGHT: number;
    }

    export interface IScreenConfiguration {
        SCREEN_BUFFER_AREA: number;
        MAX_SCREEN_WIDTH: number;
        MAX_SCREEN_HEIGHT: number;
        MIN_SCREEN_WIDTH: number;
        MIN_SCREEN_HEIGHT: number;
    }

    export interface IConfigurationManager {
        gameConfig: IGameConfiguration;
        //squareConfig: ISquareConfiguration;
        //mapConfig: IMapConfiguration;
        //screenConfig: IScreenConfiguration;
        //enemyConfig: IEnemyConfiguration;
    }
}
