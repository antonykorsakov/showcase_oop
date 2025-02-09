using System;
using Features.TetrisModule.Core.Data;
using UnityEngine;

namespace Features.TetrisModule.Core.Controller
{
    public sealed class TetrisGridController : ITetrisGridController
    {
        private readonly CellState[,] _grid;
        private readonly int _width;
        private readonly int _height;

        private readonly CellState[] _cacheStates = new CellState[10];

        public TetrisGridController(ITetrisCoreConfig config)
        {
            _width = config.Width;
            _height = config.Height;
            if (_width <= 0 || _height <= 0)
                throw new ArgumentException("Invalid tetris grid size.");

            _grid = new CellState[_width, _height];
        }

        public int Width => _width;
        public int Height => _height;

        public event Action GridClearedEvent;
        public event Action<int> GridLinesBurntEvent;
        public event Action GridChangedEvent;

        public event Action TetrominoSuccessSpawnedEvent;
        public event Action TetrominoFailedSpawnedEvent;

        public CellState GetCellState(int x, int y)
        {
            // check width
            if (x < 0 || x >= _width)
                return CellState.Error;

            // check height
            if (y < 0 || y >= _height)
                return CellState.Error;

            return _grid[x, y];
        }

        void ITetrisGridController.Clear()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _grid[x, y] = CellState.Empty;
                }
            }

            GridClearedEvent?.Invoke();
            GridChangedEvent?.Invoke();
        }

        int ITetrisGridController.ClearFullLines()
        {
            int fullLinesCount = 0;

            for (int y = 0; y < _height; /*nothing*/)
            {
                bool isFullLine = true;
                for (int x = 0; x < _width; x++)
                {
                    if (_grid[x, y] == CellState.Empty)
                    {
                        isFullLine = false;
                        break;
                    }
                }

                if (isFullLine)
                {
                    fullLinesCount++;

                    for (int row = y + 1; row < _height; row++)
                    {
                        for (int col = 0; col < _width; col++)
                        {
                            _grid[col, row - 1] = _grid[col, row];
                        }
                    }

                    for (int col = 0; col < _width; col++)
                    {
                        _grid[col, _height - 1] = CellState.Empty;
                    }
                }
                else
                {
                    y++;
                }
            }

            if (fullLinesCount > 0)
            {
                GridLinesBurntEvent?.Invoke(fullLinesCount);
                GridChangedEvent?.Invoke();
            }

            return fullLinesCount;
        }

        bool ITetrisGridController.SpawnTetromino(Tetromino tetromino)
        {
            int minX = (_width - tetromino.Shape.Width) / 2;
            int minY = _height - tetromino.Shape.Height;
            tetromino.Move(minX, minY);

            bool success = true;
            foreach (var cellPosition in tetromino.CellsPositions())
            {
                var cell = _grid[cellPosition.x, cellPosition.y];
                var isEmpty = cell == CellState.Empty;
                if (!isEmpty)
                    success = false;

                _grid[cellPosition.x, cellPosition.y] = tetromino.Type;
            }

            if (success)
                TetrominoSuccessSpawnedEvent?.Invoke();
            else
                TetrominoFailedSpawnedEvent?.Invoke();

            GridChangedEvent?.Invoke();
            return success;
        }

        bool ITetrisGridController.Rotate(Tetromino tetromino, bool clockwise)
        {
            int count = tetromino.Shape.CellsPositions.Count;
            if (_cacheStates.Length < count)
                Debug.LogError($"need increase cache; length: current = {_cacheStates.Length}, desired = {count};");

            // check next tetromino state
            tetromino.Rotate(clockwise);
            bool canAction = CanAssignNewPos(tetromino.Shape, tetromino.PositionArea, 0, 0);
            tetromino.Rotate(!clockwise);

            if (!canAction)
                return false;

            // reset old cells (and save states to cache) 
            for (var index = 0; index < count; index++)
            {
                var pos = tetromino.Shape.CellsPositions[index];
                _cacheStates[index] = _grid[pos.x + tetromino.PositionArea.xMin, pos.y + tetromino.PositionArea.yMin];
                _grid[pos.x + tetromino.PositionArea.xMin, pos.y + tetromino.PositionArea.yMin] = CellState.Empty;
            }

            // change tetromino
            tetromino.Rotate(clockwise);

            // set new cells (from cache)
            for (var index = 0; index < count; index++)
            {
                var pos = tetromino.Shape.CellsPositions[index];
                _grid[pos.x + tetromino.PositionArea.xMin, pos.y + tetromino.PositionArea.yMin] = _cacheStates[index];
            }

            GridChangedEvent?.Invoke();
            return true;
        }

        bool ITetrisGridController.MoveLeftTetromino(Tetromino tetromino) => MoveTetromino(tetromino, -1, 0);
        bool ITetrisGridController.MoveRightTetromino(Tetromino tetromino) => MoveTetromino(tetromino, 1, 0);
        bool ITetrisGridController.MoveDownTetromino(Tetromino tetromino) => MoveTetromino(tetromino, 0, -1);

        private bool MoveTetromino(Tetromino tetromino, int dx, int dy)
        {
            int count = tetromino.Shape.CellsPositions.Count;
            if (_cacheStates.Length < count)
                Debug.LogError($"need increase cache; length: current = {_cacheStates.Length}, desired = {count};");

            // check next tetromino state
            bool canAction = CanAssignNewPos(tetromino.Shape, tetromino.PositionArea, dx, dy);
            if (!canAction)
                return false;

            // reset old cells (and save states to cache) 
            for (var index = 0; index < count; index++)
            {
                var pos = tetromino.Shape.CellsPositions[index];
                _cacheStates[index] = _grid[pos.x + tetromino.PositionArea.xMin, pos.y + tetromino.PositionArea.yMin];
                _grid[pos.x + tetromino.PositionArea.xMin, pos.y + tetromino.PositionArea.yMin] = CellState.Empty;
            }

            // change tetromino
            tetromino.Move(dx, dy);

            // set new cells (from cache)
            for (var index = 0; index < count; index++)
            {
                var pos = tetromino.Shape.CellsPositions[index];
                _grid[pos.x + tetromino.PositionArea.xMin, pos.y + tetromino.PositionArea.yMin] = _cacheStates[index];
            }

            GridChangedEvent?.Invoke();
            return true;
        }

        private bool CanAssignNewPos(ShapeData shape, RectInt tetrominoRect, int dx, int dy)
        {
            tetrominoRect.x += dx;
            if (tetrominoRect.xMin < 0 || tetrominoRect.xMax > _width)
                return false;

            tetrominoRect.y += dy;
            if (tetrominoRect.yMin < 0 || tetrominoRect.yMax > _height)
                return false;

            // validation
            int count = shape.CellsPositions.Count;
            for (var index = 0; index < count; index++)
            {
                var pos = shape.CellsPositions[index];
                var shapeOffset = new Vector2Int(pos.x + dx, pos.y + dy);

                // check cells of current Tetromino 
                var isCurrentTetromino = shape.Contains(shapeOffset);
                if (isCurrentTetromino)
                    continue;

                // check collision
                var gridPos = new Vector2Int(pos.x + tetrominoRect.xMin, pos.y + tetrominoRect.yMin);
                var state = _grid[gridPos.x, gridPos.y];
                if (state != CellState.Empty)
                    return false;
            }

            return true;
        }
    }
}