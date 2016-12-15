/// <reference path="Enemy.ts" />
/// <reference path="../Scripts/phaser.d.ts" />
var TranMini;
(function (TranMini) {
    var EnemyGraphic = (function () {
        function EnemyGraphic(game, payload) {
            this.game = game;
            this._width = payload.Width;
            this._height = payload.Height;
            this._y = payload.Y;
            var graphics = game.add.graphics(0, 0);
            graphics.beginFill(0xFF00FF, 1);
            graphics.drawRect(0, 0, this._width, this._height);
            graphics.endFill();
            this._body = game.add.sprite(0, 0, graphics.generateTexture());
            this._body.x = payload.X;
            this._body.y = payload.Y;
            graphics.destroy();
        }
        EnemyGraphic.prototype.LoadPayload = function (payload) {
            if (payload.X != this._body.x) {
                this._body.x = payload.X;
            }
        };
        EnemyGraphic.prototype.MoveSquare = function (x) {
            // TODO: Consider animating
            this._body.x = x;
        };
        EnemyGraphic.prototype.Hide = function () {
            this._body.visible = false;
        };
        return EnemyGraphic;
    }());
    TranMini.EnemyGraphic = EnemyGraphic;
})(TranMini || (TranMini = {}));
