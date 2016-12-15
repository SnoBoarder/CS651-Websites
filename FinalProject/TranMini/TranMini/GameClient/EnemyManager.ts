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

                    //this._enemies[enemy.ID].OnDisposed.Bind((enemy) => {
                    //    delete this._enemies[(<Enemy>enemy).ID];
                    //});
                } else {
                    this._enemies[enemy.ID].LoadPayload(enemy);
                }

                //if (enemy.Disposed) {
                //    if (enemy.Collided) {
                //        this._enemies[enemy.ID].MovementController.Position = enemy.CollidedAt;
                //    }

                //    this._enemies[enemy.ID].Destroy(enemy.Collided);
                //}
            }
        }

        //public Update(gameTime: eg.GameTime): void {
        //    // Update positions first
        //    for (var id in this._enemies) {
        //        this._enemies[id].Update(gameTime);
        //    }

        //    for (var id in this._enemies) {
        //        // Check for "in-bounds" to see what bullets we should destroy
        //        //if (!this._enemies[id].Bounds.IntersectsRectangle(this._viewport)) {
        //        //    this._enemies[id].Destroy(false);
        //        //}
        //    }
        //}
    }

}