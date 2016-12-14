/// <reference path="../Scripts/typings/jquery.cookie/jquery.cookie.d.ts" />
/// <reference path="IUserInformation.ts" />
var TranMini;
(function (TranMini) {
    var Server;
    (function (Server) {
        var ServerConnectionManager = (function () {
            function ServerConnectionManager(_authCookieName) {
                this._authCookieName = _authCookieName;
            }
            ServerConnectionManager.prototype.PrepareRegistration = function () {
                var stateCookie = $.cookie(this._authCookieName);
                var state = stateCookie ? JSON.parse(stateCookie) : {};
                var registrationID = state.RegistrationID;
                if (registrationID) {
                    delete state.RegistrationID;
                    // Re-update the registration cookie
                    $.cookie(this._authCookieName, JSON.stringify(state), { path: '/', expires: 30 });
                    return {
                        Name: state.DisplayName,
                        RegistrationID: registrationID
                    };
                }
                else {
                    throw new Error("Registration ID not available.");
                }
            };
            return ServerConnectionManager;
        }());
        Server.ServerConnectionManager = ServerConnectionManager;
    })(Server = TranMini.Server || (TranMini.Server = {}));
})(TranMini || (TranMini = {}));
