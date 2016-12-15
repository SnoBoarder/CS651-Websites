/// <reference path="Square.ts" />
/// <reference path="../Scripts/phaser.d.ts" />

module TranMini {

    export class SquareGraphic {
        public group: Phaser.Group;

        constructor(game: Phaser.Game, name: string, userControlled: boolean) {
            this.group = game.add.group();

            var graphics = game.add.graphics(0, 0);

            if (userControlled) {
                graphics.beginFill(0xFFFFFF, 1);
                graphics.moveTo(25, 0);
                graphics.lineTo(75, 0);
                graphics.lineTo(50, 20);
                graphics.lineTo(25, 0);
                graphics.endFill();
            }

            graphics.beginFill(0xFFFFFF, 1);
            graphics.drawRect(0, 30, 100, 100);
            graphics.endFill();

            var body = game.add.sprite(0, 0, graphics.generateTexture());

            var style = { font: "bold 14px Arial", fill: "#000", boundsAlignH: "center", boundsAlignV: "middle" };

            var text = game.add.text(0, 0, name, style);
            text.setTextBounds(0, 30, 100, 100);

            this.group.add(body);
            this.group.add(text);

            this.group.x = 200;
            this.group.y = 300;

            graphics.destroy();
        }

        public MoveSquare(x: number, y: number) {
            //this.Body.x = x;
            //this.Body.y = y;
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