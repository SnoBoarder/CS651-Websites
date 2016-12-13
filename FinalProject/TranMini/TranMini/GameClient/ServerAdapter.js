/// <reference path="../Scripts/typings/signalr/signalr.d.ts" />
var TranMini;
(function (TranMini) {
    var Server;
    (function (Server) {
        var ServerAdapter = (function () {
            function ServerAdapter(Connection, Proxy, authCookieName) {
                this.Connection = Connection;
                this.Proxy = Proxy;
                // Get the user name and store it to prepend to messages.
                //$('#displayname').val(prompt('Enter your name:', ''));
                console.log("server adapter!");
                // Create a function that the hub can call back to display messages.
                Proxy.on("addNewMessageToPage", function (name, message) {
                    // Add the message to the page.
                    $('#discussion').append('<li><strong>' + name
                        + '</strong>: ' + message + '</li>');
                });
                Connection.start().done(function () {
                    $('#sendmessage').click(function () {
                        // Call the Send method on the hub.
                        Proxy.invoke("send", $('#displayname').val(), $('#message').val());
                        // Clear text box and reset focus for next comment.
                        $('#message').val('').focus();
                    });
                });
            }
            // This optional function html-encodes messages for display in the page.
            ServerAdapter.prototype.htmlEncode = function (value) {
                var encodedValue = $('<div />').text(value).html();
                return encodedValue;
            };
            return ServerAdapter;
        }());
        Server.ServerAdapter = ServerAdapter;
    })(Server = TranMini.Server || (TranMini.Server = {}));
})(TranMini || (TranMini = {}));
//# sourceMappingURL=ServerAdapter.js.map