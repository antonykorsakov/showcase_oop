using UnityEngine;

namespace Features.TetrisModule.Core.Controller
{
    public sealed class TetrisManager : ITetrisManager
    {
        private readonly ITetrisGridController _tetrisGridController;
        private readonly ITetrisItemsController _tetrisItemsController;
        private readonly ITetrisSpeedController _tetrisSpeedController;

        public TetrisManager(
            ITetrisGridController tetrisGridController,
            ITetrisItemsController tetrisItemsController,
            ITetrisSpeedController tetrisSpeedController)
        {
            _tetrisGridController = tetrisGridController;
            _tetrisItemsController = tetrisItemsController;
            _tetrisSpeedController = tetrisSpeedController;

            _tetrisSpeedController.SetAction(MoveDownOrSpawn);
        }

        public void GestureHandling(Vector2 value)
        {
            var tetromino = _tetrisItemsController.CurrentTetromino;
            switch (value.x)
            {
                case > 0:
                    _tetrisGridController.MoveRightTetromino(tetromino);
                    break;

                case < 0:
                    _tetrisGridController.MoveLeftTetromino(tetromino);
                    break;
            }

            switch (value.y)
            {
                case > 0:
                    _tetrisGridController.Rotate(tetromino, true);
                    break;

                case < 0:
                    _tetrisSpeedController.TurnOnAccelerate();
                    break;
            }
        }

        public void ReStartGame()
        {
            _tetrisGridController.Clear();
            SpawnTetromino();
        }

        private void MoveDownOrSpawn()
        {
            var tetromino = _tetrisItemsController.CurrentTetromino;
            var hasMoveDown = _tetrisGridController.MoveDownTetromino(tetromino);
            if (!hasMoveDown)
                SpawnTetromino();
        }

        private void SpawnTetromino()
        {
            _tetrisGridController.ClearFullLines();
            _tetrisSpeedController.TurnOffAccelerate();
            _tetrisItemsController.ChangeTetromino();
            var tetromino = _tetrisItemsController.CurrentTetromino;
            _tetrisGridController.SpawnTetromino(tetromino);
        }
    }
}