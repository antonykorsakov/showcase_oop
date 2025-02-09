using System;
using System.Runtime.CompilerServices;
using Features.TetrisModule.Core.Data;

[assembly: InternalsVisibleTo("Features.TetrisModule.Tests.EditMode")]

namespace Features.TetrisModule.Core.Controller
{
    public interface ITetrisGridController
    {
        int Width { get; }
        int Height { get; }

        event Action GridClearedEvent;
        event Action<int> GridLinesBurntEvent;
        event Action GridChangedEvent;

        event Action TetrominoSuccessSpawnedEvent;
        event Action TetrominoFailedSpawnedEvent;

        CellState GetCellState(int x, int y);

        internal void Clear();
        internal int ClearFullLines();

        internal bool SpawnTetromino(Tetromino tetromino);
        internal bool Rotate(Tetromino tetromino, bool clockwise);
        internal bool MoveLeftTetromino(Tetromino tetromino);
        internal bool MoveRightTetromino(Tetromino tetromino);
        internal bool MoveDownTetromino(Tetromino tetromino);
    }
}