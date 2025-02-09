using System.Collections.Generic;
using UnityEngine;

namespace Features.TetrisModule.Core.Data
{
    /// <summary>
    /// A universal description of a shape for more convenient interaction with controlling scripts.
    /// </summary>
    public sealed class ShapeData
    {
        private readonly List<Vector2Int> _cellsPositions = new();

        public ShapeData(byte[,] shape)
        {
            Width = shape.GetLength(0);
            Height = shape.GetLength(1);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    bool isChangeable = shape[x, y] == 1;
                    if (!isChangeable)
                        continue;

                    var point = new Vector2Int(x, y);
                    _cellsPositions.Add(point);
                }
            }
        }

        public int Width { get; }
        public int Height { get; }
        public IReadOnlyList<Vector2Int> CellsPositions => _cellsPositions;

        public bool Contains(Vector2Int point) => _cellsPositions.Contains(point);
    }
}