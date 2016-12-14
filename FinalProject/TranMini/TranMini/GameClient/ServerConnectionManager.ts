/// <reference path="../Scripts/typings/jquery.cookie/jquery.cookie.d.ts" />
/// <reference path="IUserInformation.ts" />

module TranMini.Server {

    export class ServerConnectionManager {
        constructor(private _authCookieName: string) { }

        public PrepareRegistration(): IUserInformation {
            var stateCookie: JQueryCookieStatic = $.cookie(this._authCookieName);
            var state: any = stateCookie ? JSON.parse((<any>stateCookie)) : {};
            var registrationID: string = state.RegistrationID;

            if (registrationID) {
                delete state.RegistrationID;

                // Re-update the registration cookie
                $.cookie(this._authCookieName, JSON.stringify(state), { path: '/', expires: 30 });

                return {
                    Name: state.DisplayName,
                    RegistrationID: registrationID
                };
            } else {
                throw new Error("Registration ID not available.");
            }
        }
    }

}