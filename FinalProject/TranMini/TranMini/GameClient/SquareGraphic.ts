/// <reference path="Square.ts" />
/// <reference path="../Scripts/phaser.d.ts" />

module TranMini {

    export class SquareGraphic {
        private _square: Phaser.Group;
        private _body: Phaser.Sprite;
        private _playerName: Phaser.Text;
        private _description: Phaser.Text;

        private _scoring: Phaser.Group;
        private _currentScoreText: Phaser.Text;
        private _highScoreText: Phaser.Text;

        private _width: number;
        private _height: number;

        private _y: number;

        private _jumping: boolean = false;

        private _currentScore: number;
        private _highScore: number;

        constructor(private game: Phaser.Game, payload: Server.ISquareData) {
            this._currentScore = 0;
            this._highScore = 0;

            this._width = payload.Width;
            this._height = payload.Height;

            this._y = payload.Y;

            this.CreateSquare(payload);
            this.CreateScoring(payload);
        }

        private CreateSquare(payload: Server.ISquareData): void {
            this._square = this.game.add.group();

            var graphics = this.game.add.graphics(0, 0);

            graphics.beginFill(payload.UserControlled ? 0xCEAB34 : 0xD039D8, 1);
            graphics.drawRect(0, 0, this._width, this._height);
            graphics.endFill();

            this._body = this.game.add.sprite(0, 0, graphics.generateTexture());

            var style = { font: "bold 10px Arial", fill: "#fff", boundsAlignH: "center", boundsAlignV: "middle" };

            this._playerName = this.game.add.text(0, 0, payload.Name, style);
            this._playerName.setTextBounds(0, 0, this._width, this._height);

            this._square.add(this._body);
            this._square.add(this._playerName);

            if (payload.UserControlled) {
                this._description = this.game.add.text(0, 0, "(YOU)", style);
                this._description.setTextBounds(0, 0, this._width, this._height / 2);
                this._square.add(this._description);
            }

            this._square.x = payload.X;
            this._square.y = payload.Y;

            graphics.destroy();
        }

        private CreateScoring(payload: Server.ISquareData): void {
            var currentScoreStyle = { font: "bold 18px Arial", fill: "#ffff00", boundsAlignH: "center", boundsAlignV: "middle" };
            var highScoreStyle = { font: "bold 14px Arial", fill: "#fff", boundsAlignH: "center", boundsAlignV: "middle" };

            this._currentScore = payload.CurrentScore;
            this._highScore = payload.HighScore;

            this._highScoreText = this.game.add.text(0, 0, String(this._highScore), highScoreStyle);
            this._highScoreText.setTextBounds(0, 30, this._width, 0);
            this._currentScoreText = this.game.add.text(0, 0, String(this._currentScore), currentScoreStyle);
            this._currentScoreText.setTextBounds(0, 0, this._width, 0);

            this._scoring = this.game.add.group();
            this._scoring.add(this._highScoreText);
            this._scoring.add(this._currentScoreText);

            this._scoring.x = payload.X;
            this._scoring.y = 150;
        }

        public LoadPayload(payload: Server.ISquareData): void {
            this.HandleScoring(payload);

            if (!this._jumping && payload.Jump > 0) {
                this._jumping = true;
                this.Jump(payload.Jump);
            }

            if (this._jumping && payload.Jump == 0) {
                this._jumping = false;
            }

            if (payload.X != this._square.x) {
                this.MoveSquare(payload.X);
            }
        }

        private HandleScoring(payload: Server.ISquareData): void {
            if (this._currentScore > payload.CurrentScore) {
                // we failed! trigger
                var tween = this.game.add.tween(this._square).to({ x: payload.X - 10 }, 50, Phaser.Easing.Bounce.InOut, false, 0, 4, true);
                tween.start();
            }

            this._currentScore = payload.CurrentScore;
            this._highScore = payload.HighScore;

            this._currentScoreText.text = String(this._currentScore);
            this._highScoreText.text = String(this._highScore);
        }

        private MoveSquare(x: number) {
            // TODO: Consider animating
            this._square.x = x;
            this._scoring.x = x;
        }

        private Jump(duration: number) {
            var tweenA = this.game.add.tween(this._square).to({ y: this._y - 50 }, duration / 2, Phaser.Easing.Exponential.Out);
            var tweenB = this.game.add.tween(this._square).to({ y: this._y }, duration / 2, Phaser.Easing.Bounce.Out);

            tweenA.chain(tweenB);

            tweenA.start();
        }

        public Hide(): void {
            this._square.visible = false;
        }

        public Dispose(): void {
            this._square.removeAll();
            this._body.kill();
            this._playerName.kill();
            if (this._description != null) {
                this._description.kill();
            }

            this._scoring.removeAll();
            this._currentScoreText.kill();
            this._highScoreText.kill();
        }
    }

}