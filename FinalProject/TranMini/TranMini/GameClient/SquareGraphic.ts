/// <reference path="Square.ts" />
/// <reference path="../Scripts/phaser.d.ts" />

module TranMini {

    export class SquareGraphic {
        public group: Phaser.Group;

        // get real x y coord
        private _x: number = 200;
        private _y: number = 300;

        constructor(private game: Phaser.Game, name: string, userControlled: boolean) {
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

        public MoveSquare(x: number, y: number) {
            //this.Body.x = x;
            //this.Body.y = y;
        }

        public Jump(duration: number) {
            var tweenA = this.game.add.tween(this.group).to({ y: this._y - 100 }, duration / 2, Phaser.Easing.Exponential.Out, true);
            var tweenB = this.game.add.tween(this.group).to({ y: this._y }, duration / 2, Phaser.Easing.Bounce.Out);

            tweenA.chain(tweenB);
        }

        //public Status(text: string, size: number, color: eg.Graphics.Color, fadeDuration?: eg.TimeSpan, reverseDirection?: boolean): void {
        //    this._statusGraphic.Status(text, size, color, fadeDuration, reverseDirection);
        //}

        public Hide(): void {
            this.group.visible = false;
            //this.Body.visible = false;
        }

        //public Update(gameTime: eg.GameTime): void {
        //    this._statusGraphic.Update(gameTime);
        //}
    }

}