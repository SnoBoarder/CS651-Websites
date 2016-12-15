/// <reference path="../Scripts/endgate-0.2.0.d.ts" />
/// <reference path="IPayloadDefinitions.ts" />
/// <reference path="EnemyGraphic.ts" />
var TranMini;
(function (TranMini) {
    var Enemy = (function () {
        function Enemy(_game, payload) {
            this._game = _game;
            // Going to use the rectangle to "hold" all the other graphics
            this.Graphic = new TranMini.EnemyGraphic(this._game, payload);
            this.OnExplosion = new eg.EventHandler();
            this._spawnedAt = new Date().getTime();
            this._destroyed = false;
            this.LoadPayload(payload);
        }
        Enemy.prototype.Update = function (gameTime) {
            // Bullets been alive too long
            //if ((new Date().getTime() - this._spawnedAt) >= Bullet.BULLET_DIE_AFTER.Milliseconds) {
            //    this.Destroy(false);
            //}
        };
        Enemy.prototype.LoadPayload = function (payload) {
            this.ID = payload.ID;
            // Ensure that our position matches the movement controllers position
            this.Graphic.LoadPayload(payload);
        };
        Enemy.prototype.Destroy = function (explode) {
            if (explode === void 0) { explode = true; }
            if (!this._destroyed) {
                this._destroyed = true;
                //this.MovementController.Dispose();
                if (!explode) {
                }
                else {
                    // We rely on the completion of the explosion to finish disposing the bounds and graphic
                    this.OnExplosion.Trigger();
                }
            }
        };
        Enemy.SIZE = new eg.Size2d(13);
        Enemy.BULLET_DIE_AFTER = eg.TimeSpan.FromSeconds(2);
        return Enemy;
    }());
    TranMini.Enemy = Enemy;
})(TranMini || (TranMini = {}));
//# sourceMappingURL=Enemy.js.map