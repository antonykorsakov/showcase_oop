using System.Collections.Generic;
using UnityEngine;

namespace Features.TetrisModule.Core.Data
{
    [CreateAssetMenu(fileName = nameof(TetrisCoreConfig),
        menuName = "Configs/" + nameof(TetrisCoreConfig), order = 'T')]
    public sealed class TetrisCoreConfig : ScriptableObject, ITetrisCoreConfig
    {
        [SerializeField] private int _width = 10;
        [SerializeField] private int _height = 20;

        [SerializeField, Range(0f, 0.5f)] private float _fastFallDelay = 0.005f;

        [SerializeField, Range(0f, 0.5f)] private float[] _standardFallDelay =
        {
            0.3f,
            0.2f,
            0.1f,
            0.065f,
            0.033f,
            0.02f,
            0.015f,
            0.01f,
            0.005f,
        };

        [SerializeField] private int _startTetrominoLevel = 1;

        [SerializeField] private TetrominoCollection[] _shapesByDifficulty =
        {
            new(CellState.I, CellState.O, CellState.T),
            new(CellState.I, CellState.O, CellState.T, CellState.L, CellState.S),
            new(CellState.I, CellState.O, CellState.T, CellState.L, CellState.S, CellState.J, CellState.Z),
            new(CellState.L, CellState.S, CellState.J, CellState.Z, CellState.Tb, CellState.Sb),
            new(CellState.Tb, CellState.Sb, CellState.Zb, CellState.W, CellState.C, CellState.Tank)
        };

        public int Width => _width;
        public int Height => _height;
        public float FastFallDelay => _fastFallDelay;
        public int SpeedLevelsCount => _standardFallDelay.Length;
        public int StartTetrominoLevel => _startTetrominoLevel;
        public int ShapesDifficultyLevelsCount => _shapesByDifficulty.Length;

        public float GetStandardFallDelay(int index)
        {
            index = Mathf.Clamp(index, 0, _standardFallDelay.Length - 1);
            return _standardFallDelay[index];
        }

        public IReadOnlyList<CellState> GetTetrominoTypesByDifficulty(int index)
        {
            index = Mathf.Clamp(index, 0, _shapesByDifficulty.Length - 1);
            return _shapesByDifficulty[index].Types;
        }
    }
}