using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.TetrisModule.Core.Data
{
    [Serializable]
    public class TetrominoCollection
    {
        [SerializeField] private CellState[] _types;

        public TetrominoCollection(params CellState[] types)
        {
            _types = types;
        }

        public IReadOnlyList<CellState> Types => _types;
    }
}