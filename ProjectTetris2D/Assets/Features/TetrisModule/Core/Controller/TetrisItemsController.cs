using System;
using System.Collections.Generic;
using Features.TetrisModule.Core.TetrominoType;
using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.Controller
{
    public sealed class TetrisItemsController : ITetrisItemsController
    {
        private readonly ITetrisCoreConfig _config;

        public TetrisItemsController(ITetrisCoreConfig config)
        {
            _config = config;

            DifficultyLevel.ValueChangedEvent += SetTetrominoCollection;
            DifficultyLevel.Set(config.ShapesDifficultyLevelsCount, config.StartTetrominoLevel);
        }

        public IReadOnlyList<CellState> FallingTetrominoTypes { get; private set; }

        public Tetromino CurrentTetromino { get; private set; }
        public TetrominoData NextTetrominoData { get; private set; }

        public GameParameterController DifficultyLevel { get; } = new("TetrominoComplexityLevel");
        public GameParameterController ShapeType { get; } = new("TetrominoShapeType");

        public event Action NextTetrominoDataChangedEvent;

        public void PrepareFirstTetromino()
        {
            GenerateNextTetromino();
        }

        void ITetrisItemsController.ChangeTetromino()
        {
            CurrentTetromino = new Tetromino(NextTetrominoData);
            GenerateNextTetromino();
        }

        private void SetTetrominoCollection()
        {
            FallingTetrominoTypes = _config.GetTetrominoTypesByDifficulty(DifficultyLevel.Value);
            ShapeType.Set(FallingTetrominoTypes.Count, 0);
        }

        private void GenerateNextTetromino()
        {
            ShapeType.SetRandomValue();

            var type = FallingTetrominoTypes[ShapeType.Value];
            NextTetrominoData = type.CreateTetrominoData();

            NextTetrominoDataChangedEvent?.Invoke();
        }
    }
}