/// <reference path="Enemy.ts" />
var TranMini;
(function (TranMini) {
    var EnemyManager = (function () {
        function EnemyManager(_game) {
            this._game = _game;
            this._enemies = {};
        }
        EnemyManager.prototype.LoadPayload = function (payload) {
            var enemyPayload = payload.Enemies;
            var enemy;
            for (var i = 0; i < enemyPayload.length; i++) {
                enemy = enemyPayload[i];
                if (!this._enemies[enemy.ID]) {
                    this._enemies[enemy.ID] = new TranMini.Enemy(this._game, enemy);
                }
                else {
                    this._enemies[enemy.ID].LoadPayload(enemy);
                }
            }
        };
        return EnemyManager;
    }());
    TranMini.EnemyManager = EnemyManager;
})(TranMini || (TranMini = {}));
//# sourceMappingURL=EnemyManager.js.map