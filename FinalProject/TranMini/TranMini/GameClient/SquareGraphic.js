/// <reference path="Square.ts" />
/// <reference path="../Scripts/phaser.d.ts" />
var TranMini;
(function (TranMini) {
    var SquareGraphic = (function () {
        function SquareGraphic(game, name, userControlled) {
            this.game = game;
            // get real x y coord
            this._x = 200;
            this._y = 300;
            this.group = game.add.group();
            var graphics = game.add.graphics(0, 0);
            graphics.beginFill(0xFFFFFF, 1);
            graphics.drawRect(0, 0, 100, 100);
            graphics.endFill();
            var body = game.add.sprite(0, 0, graphics.generateTexture());
            var style = { font: "bold 14px Arial", fill: "#000", boundsAlignH: "center", boundsAlignV: "middle" };
            var text = game.add.text(0, 0, name, style);
            text.setTextBounds(0, 0, 100, 100);
            this.group.add(body);
            this.group.add(text);
            if (userControlled) {
                var description = game.add.text(0, 0, "(YOU)", style);
                description.setTextBounds(0, 0, 100, 50);
                this.group.add(description);
            }
            this.group.x = this._x;
            this.group.y = this._y;
            graphics.destroy();
        }
        SquareGraphic.prototype.MoveSquare = function (x, y) {
            //this.Body.x = x;
            //this.Body.y = y;
        };
        SquareGraphic.prototype.Jump = function (duration) {
            var tweenA = this.game.add.tween(this.group).to({ y: this._y - 100 }, duration / 2, Phaser.Easing.Exponential.Out, true);
            var tweenB = this.game.add.tween(this.group).to({ y: this._y }, duration / 2, Phaser.Easing.Bounce.Out);
            tweenA.chain(tweenB);
        };
        //public Status(text: string, size: number, color: eg.Graphics.Color, fadeDuration?: eg.TimeSpan, reverseDirection?: boolean): void {
        //    this._statusGraphic.Status(text, size, color, fadeDuration, reverseDirection);
        //}
        SquareGraphic.prototype.Hide = function () {
            this.group.visible = false;
            //this.Body.visible = false;
        };
        return SquareGraphic;
    }());
    TranMini.SquareGraphic = SquareGraphic;
})(TranMini || (TranMini = {}));
