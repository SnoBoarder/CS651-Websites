/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="ServerAdapter.ts" />
$(function () {
    //http://stackoverflow.com/questions/31160722/typescript-how-to-import-classes-uncaught-referenceerror
    var connection = $.connection.game;
    var serverAdapter = new TranMini.Server.ServerAdapter($.connection.hub, connection, "tranmini.state");
    var game; // = new TranMini.Game("game", serverAdapter);
    serverAdapter.Negotiate().done(function (initializationData) {
        //loadContent.hide();
        //gameContent.show();
        game = new TranMini.Game(serverAdapter, initializationData); //new TranMini.Game(<HTMLCanvasElement>gameCanvas[0], gameScreen, serverAdapter, initializationData);
    });
    //    popUpHolder: JQuery = $("#popUpHolder"),
    //    gameContent: JQuery = $("#gameContent"),
    //    loadContent: JQuery = $("#loadContent"),
    //    game: ShootR.Game,
    //    serverAdapter: ShootR.Server.ServerAdapter = new ShootR.Server.ServerAdapter($.connection.hub, (<any>$.connection).h, "shootr.state"),
    //    gameScreen: ShootR.GameScreen = new ShootR.GameScreen(gameCanvas, popUpHolder, serverAdapter);
    //gameScreen.OnResizeComplete.BindFor(() => {
    //    serverAdapter.Negotiate().done((initializationData: ShootR.Server.IClientInitialization) => {
    //        loadContent.hide();
    //        gameContent.show();
    //        game = new ShootR.Game(<HTMLCanvasElement>gameCanvas[0], gameScreen, serverAdapter, initializationData);
    //        gameScreen.ForceResizeCheck();
    //    });
    //}, 1);
});
