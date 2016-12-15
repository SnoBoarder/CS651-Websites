/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="ServerAdapter.ts" />
$(function () {
    var connection = $.connection.game;
    var serverAdapter = new TranMini.Server.ServerAdapter($.connection.hub, connection, "tranmini.state");
    var game;
    serverAdapter.Negotiate().done(function (initializationData) {
        game = new TranMini.Game(serverAdapter, initializationData);
    });
});
//# sourceMappingURL=Main.js.map