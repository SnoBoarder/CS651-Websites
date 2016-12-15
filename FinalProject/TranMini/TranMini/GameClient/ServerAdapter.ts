/// <reference path="../Scripts/endgate-0.2.0.d.ts" />
/// <reference path="../Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="PayloadDecompressor.ts" />
/// <reference path="ServerConnectionManager.ts" />

module TranMini.Server {
    export class ServerAdapter {
        public static NEGOTIATE_RETRIES: number = 3;
        //public static RETRY_DELAY: eg.TimeSpan = eg.TimeSpan.FromSeconds(1);

        public OnPayload: eg.EventHandler1<IPayloadData>;
        public OnForcedDisconnect: eg.EventHandler;
        public OnControlTransferred: eg.EventHandler;
        public OnPingRequest: eg.EventHandler;
        //public OnMessageReceived: EventHandler1<ShootR.ChatMessage>;

        private _payloadDecompressor: PayloadDecompressor;
        private _connectionManager: ServerConnectionManager;

        constructor(public Connection: SignalR.Hub.Connection, public Proxy: SignalR.Hub.Proxy, authCookieName: string) {

            //var savedProxyInvoke = this.Proxy.invoke;

            this.OnPayload = new eg.EventHandler1<IPayloadData>();
            this.OnForcedDisconnect = new eg.EventHandler();
            this.OnControlTransferred = new eg.EventHandler();
            this.OnPingRequest = new eg.EventHandler();

            this._connectionManager = new ServerConnectionManager(authCookieName);

            //(<any>this.Proxy.invoke) = () => {
            //    if ((<any>this.Connection).state === $.signalR.connectionState.connected) {
            //        return savedProxyInvoke.apply(this.Proxy, arguments);
            //    }
            //};

            this.OnForcedDisconnect.Bind(() => {
                // You have been disconnected for being Idle too long.  Refresh the page to play again.
                this.Stop();
            });

            this.OnControlTransferred.Bind(() => {
                // You have been disconnected!  The control for your square has been transferred to your other login.
                this.Stop();
            });
        }

        // This optional function html-encodes messages for display in the page.
        public htmlEncode(value: string): string {
            var encodedValue = $('<div />').text(value).html();
            return encodedValue;
        }

        public Negotiate(): JQueryPromise<IClientInitialization> {
            var userInformation: IUserInformation = this._connectionManager.PrepareRegistration();
            var result: JQueryDeferred<IClientInitialization> = $.Deferred();

            this.Wire();

            this.Connection.start().done(() => {
                this.TryInitialize(userInformation, (initialization: IClientInitialization) => {
                    initialization.UserInformation = userInformation;
                    this._payloadDecompressor = new PayloadDecompressor(initialization.CompressionContracts);

                    result.resolve(initialization);

                    this.Proxy.invoke("readyForPayloads");
                });
            });

            return result.promise();
        }

        public Stop(): void {
            this.Connection.stop();
        }

        private TryInitialize(userInformation: IUserInformation, onComplete: (initialization: IClientInitialization) => void, count: number = 0): void {
            this.Proxy.invoke("initializeClient", userInformation.RegistrationID).done((initialization: IClientInitialization) => {
                if (!initialization) {
                    if (count >= ServerAdapter.NEGOTIATE_RETRIES) {
                        console.log("Could not negotiate with server, refreshing the page.");
                        window.location.reload();
                    } else {
                        //setTimeout(() => {
                        //    this.TryInitialize(userInformation, onComplete, count + 1);
                        //}, ServerAdapter.RETRY_DELAY.Milliseconds);
                    }
                } else {
                    onComplete(initialization);
                }
            });
        }

        private Wire(): void {
            this.Proxy.on("d", (payload: any) => {
                this.OnPayload.Trigger(this._payloadDecompressor.Decompress(payload));
            });

            this.Proxy.on("disconnect", () => {
                this.OnForcedDisconnect.Trigger();
            });

            this.Proxy.on("controlTransferred", () => {
                this.OnControlTransferred.Trigger();
            });

            this.Proxy.on("pingBack", () => {
                this.OnPingRequest.Trigger();
            });

            this.Proxy.on("chatMessage", (from: string, message: string, type: number) => {
                //this.OnMessageReceived.Trigger(new ShootR.ChatMessage(from, message, type));
            });
        }
    }
}
