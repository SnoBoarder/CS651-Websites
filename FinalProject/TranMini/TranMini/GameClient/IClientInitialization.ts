/// <reference path="IUserInformation.ts" />
/// <reference path="IConfigurationDefinitions.ts" />

module TranMini.Server {
    export interface IClientInitialization {
        Configuration: IConfigurationManager;
        ServerFull: boolean;
        CompressionContracts: any;
        SquareID: number;
        SquareName: string;
        UserInformation: IUserInformation;
    }

}
