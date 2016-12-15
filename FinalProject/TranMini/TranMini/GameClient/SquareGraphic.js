/// <reference path="Square.ts" />
/// <reference path="../Scripts/phaser.d.ts" />
var TranMini;
(function (TranMini) {
    var SquareGraphic = (function () {
        function SquareGraphic(game, payload) {
            this.game = game;
            this._jumping = false;
            this._width = payload.Width;
            this._height = payload.Height;
            this._y = payload.Y;
            this.group = game.add.group();
            var graphics = game.add.graphics(0, 0);
            graphics.beginFill(payload.UserControlled ? 0xEEEEEE : 0xFFFFFF, 1);
            graphics.drawRect(0, 0, this._width, this._height);
            graphics.endFill();
            var body = game.add.sprite(0, 0, graphics.generateTexture());
            var style = { font: "bold 10px Arial", fill: "#000", boundsAlignH: "center", boundsAlignV: "middle" };
            var text = game.add.text(0, 0, payload.Name, style);
            text.setTextBounds(0, 0, this._width, this._height);
            this.group.add(body);
            this.group.add(text);
            if (payload.UserControlled) {
                var description = game.add.text(0, 0, "(YOU)", style);
                description.setTextBounds(0, 0, this._width, this._height / 2);
                this.group.add(description);
            }
            this.group.x = payload.X;
            this.group.y = payload.Y;
            graphics.destroy();
        }
        SquareGraphic.prototype.LoadPayload = function (payload) {
            if (!this._jumping && payload.Jump > 0) {
                this._jumping = true;
                this.Jump(payload.Jump);
            }
            if (this._jumping && payload.Jump == 0) {
                this._jumping = false;
            }
            if (payload.X != this.group.x) {
                this.MoveSquare(payload.X);
            }
        };
        SquareGraphic.prototype.MoveSquare = function (x) {
            // TODO: Consider animating
            this.group.x = x;
        };
        SquareGraphic.prototype.Jump = function (duration) {
            var tweenA = this.game.add.tween(this.group).to({ y: this._y - 100 }, duration / 2, Phaser.Easing.Exponential.Out);
            var tweenB = this.game.add.tween(this.group).to({ y: this._y }, duration / 2, Phaser.Easing.Bounce.Out);
            tweenA.chain(tweenB);
            tweenA.start();
        };
        SquareGraphic.prototype.Hide = function () {
            this.group.visible = false;
        };
        return SquareGraphic;
    }());
    TranMini.SquareGraphic = SquareGraphic;
})(TranMini || (TranMini = {}));
//# sourceMappingURL=SquareGraphic.js.map