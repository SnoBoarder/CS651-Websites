/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="ServerAdapter.ts" />

$(function () {
    var connection = (<any>$.connection).game;
    var serverAdapter: TranMini.Server.ServerAdapter = new TranMini.Server.ServerAdapter($.connection.hub, connection, "tranmini.state");
    var game: TranMini.Game;

    serverAdapter.Negotiate().done((initializationData: TranMini.Server.IClientInitialization) => {
        game = new TranMini.Game(serverAdapter, initializationData);
    });
});
