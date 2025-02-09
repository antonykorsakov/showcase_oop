using System.Collections.Generic;
using Features.TetrisModule.Core.TetrominoType;
using UnityEngine;

namespace Features.TetrisModule.Core.Data
{
    /// <summary>
    /// A detailed description of the shape, including its current position in the grid.
    /// </summary>
    public sealed class Tetromino
    {
        private readonly TetrominoData _tetrominoData;

        private RectInt _positionArea;
        private int _rotateState;

        public Tetromino(TetrominoData tetrominoData)
        {
            _tetrominoData = tetrominoData;
            _positionArea = new RectInt(0, 0, Shape.Width, Shape.Height);
        }

        public RectInt PositionArea => _positionArea;
        public CellState Type => _tetrominoData.Type;
        public ShapeData Shape => _tetrominoData.GetShape(_rotateState);

        public void Move(int dx, int dy)
        {
            _positionArea.x += dx;
            _positionArea.y += dy;
        }

        public void Rotate(bool clockwise)
        {
            if (_tetrominoData.ShapesCount < 2)
                return;

            var newRotateState = GetNewRotateState(clockwise);
            var newShape = _tetrominoData.GetShape(newRotateState);

            var xMin = _positionArea.xMin + (Shape.Width - newShape.Width) / 2;
            var yMin = _positionArea.yMin + (Shape.Height - newShape.Height) / 2;

            _positionArea = new RectInt(xMin, yMin, newShape.Width, newShape.Height);
            _rotateState = newRotateState;
        }

        public IEnumerable<Vector2Int> CellsPositions()
        {
            foreach (var position in Shape.CellsPositions)
                yield return (position + PositionArea.min);
        }

        private int GetNewRotateState(bool clockwise)
        {
            int offset = clockwise ? 1 : -1;
            int arrayLength = _tetrominoData.ShapesCount;

            int newState = (_rotateState + offset + arrayLength) % arrayLength;
            return newState;
        }
    }
}