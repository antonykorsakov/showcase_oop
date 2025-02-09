using System;
using System.Collections.Generic;
using Features.TetrisModule.Core.Data;
using Features.TetrisModule.Core.TetrominoType;

namespace Features.TetrisModule.Core.Controller
{
    public interface ITetrisItemsController
    {
        IReadOnlyList<CellState> FallingTetrominoTypes { get; }

        Tetromino CurrentTetromino { get; }
        TetrominoData NextTetrominoData { get; }

        GameParameterController DifficultyLevel { get; }
        GameParameterController ShapeType { get; }

        event Action NextTetrominoDataChangedEvent;

        void PrepareFirstTetromino();
        internal void ChangeTetromino();
    }
}