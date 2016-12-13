/// <reference path="../Scripts/typings/signalr/signalr.d.ts" />

module TranMini.Server {
    export class ServerAdapter {
        constructor(public Connection: SignalR.Hub.Connection, public Proxy: SignalR.Hub.Proxy, authCookieName: string) {
            // Get the user name and store it to prepend to messages.
            //$('#displayname').val(prompt('Enter your name:', ''));
            console.log("server adapter!");

            // Create a function that the hub can call back to display messages.
            Proxy.on("addNewMessageToPage", function (name, message) {
                // Add the message to the page.
                $('#discussion').append('<li><strong>' + name
                    + '</strong>: ' + message + '</li>');
            });

            Connection.start().done(()=> {
                $('#sendmessage').click(()=> {
                    // Call the Send method on the hub.
                    Proxy.invoke("send", $('#displayname').val(), $('#message').val());
                    // Clear text box and reset focus for next comment.
                    $('#message').val('').focus();
                });
            });
        }

        // This optional function html-encodes messages for display in the page.
        public htmlEncode(value: string): string {
            var encodedValue = $('<div />').text(value).html();
            return encodedValue;
        }
    }
}
