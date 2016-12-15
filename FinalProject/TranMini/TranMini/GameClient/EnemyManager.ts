/// <reference path="Enemy.ts" />

module TranMini {

    export class EnemyManager {
        private _enemies: { [id: number]: Enemy };

        constructor(private _game: Phaser.Game) {
            this._enemies = {};
        }

        public LoadPayload(payload: Server.IPayloadData): void {
            var enemyPayload: Array<Server.IEnemyData> = payload.Enemies;
            var enemy: Server.IEnemyData;

            for (var i = 0; i < enemyPayload.length; i++) {
                enemy = enemyPayload[i];

                if (!this._enemies[enemy.ID]) {
                    this._enemies[enemy.ID] = new Enemy(this._game, enemy);
                } else {
                    this._enemies[enemy.ID].LoadPayload(enemy);
                }
            }
        }
    }

}