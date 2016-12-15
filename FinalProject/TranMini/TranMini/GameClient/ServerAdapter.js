/// <reference path="../Scripts/endgate-0.2.0.d.ts" />
/// <reference path="../Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="PayloadDecompressor.ts" />
/// <reference path="ServerConnectionManager.ts" />
var TranMini;
(function (TranMini) {
    var Server;
    (function (Server) {
        var ServerAdapter = (function () {
            function ServerAdapter(Connection, Proxy, authCookieName) {
                //var savedProxyInvoke = this.Proxy.invoke;
                var _this = this;
                this.Connection = Connection;
                this.Proxy = Proxy;
                this.OnPayload = new eg.EventHandler1();
                this.OnForcedDisconnect = new eg.EventHandler();
                this.OnControlTransferred = new eg.EventHandler();
                this.OnPingRequest = new eg.EventHandler();
                this._connectionManager = new Server.ServerConnectionManager(authCookieName);
                //(<any>this.Proxy.invoke) = () => {
                //    if ((<any>this.Connection).state === $.signalR.connectionState.connected) {
                //        return savedProxyInvoke.apply(this.Proxy, arguments);
                //    }
                //};
                this.OnForcedDisconnect.Bind(function () {
                    // You have been disconnected for being Idle too long.  Refresh the page to play again.
                    _this.Stop();
                });
                this.OnControlTransferred.Bind(function () {
                    // You have been disconnected!  The control for your square has been transferred to your other login.
                    _this.Stop();
                });
            }
            // This optional function html-encodes messages for display in the page.
            ServerAdapter.prototype.htmlEncode = function (value) {
                var encodedValue = $('<div />').text(value).html();
                return encodedValue;
            };
            ServerAdapter.prototype.Negotiate = function () {
                var _this = this;
                var userInformation = this._connectionManager.PrepareRegistration();
                var result = $.Deferred();
                this.Wire();
                this.Connection.start().done(function () {
                    _this.TryInitialize(userInformation, function (initialization) {
                        initialization.UserInformation = userInformation;
                        _this._payloadDecompressor = new Server.PayloadDecompressor(initialization.CompressionContracts);
                        result.resolve(initialization);
                        _this.Proxy.invoke("readyForPayloads");
                    });
                });
                return result.promise();
            };
            ServerAdapter.prototype.Stop = function () {
                this.Connection.stop();
            };
            ServerAdapter.prototype.TryInitialize = function (userInformation, onComplete, count) {
                if (count === void 0) { count = 0; }
                this.Proxy.invoke("initializeClient", userInformation.RegistrationID).done(function (initialization) {
                    if (!initialization) {
                        if (count >= ServerAdapter.NEGOTIATE_RETRIES) {
                            console.log("Could not negotiate with server, refreshing the page.");
                            window.location.reload();
                        }
                        else {
                        }
                    }
                    else {
                        onComplete(initialization);
                    }
                });
            };
            ServerAdapter.prototype.Wire = function () {
                var _this = this;
                this.Proxy.on("d", function (payload) {
                    _this.OnPayload.Trigger(_this._payloadDecompressor.Decompress(payload));
                });
                this.Proxy.on("disconnect", function () {
                    _this.OnForcedDisconnect.Trigger();
                });
                this.Proxy.on("controlTransferred", function () {
                    _this.OnControlTransferred.Trigger();
                });
                this.Proxy.on("pingBack", function () {
                    _this.OnPingRequest.Trigger();
                });
                this.Proxy.on("chatMessage", function (from, message, type) {
                    //this.OnMessageReceived.Trigger(new ShootR.ChatMessage(from, message, type));
                });
            };
            ServerAdapter.NEGOTIATE_RETRIES = 3;
            return ServerAdapter;
        }());
        Server.ServerAdapter = ServerAdapter;
    })(Server = TranMini.Server || (TranMini.Server = {}));
})(TranMini || (TranMini = {}));
//# sourceMappingURL=ServerAdapter.js.map