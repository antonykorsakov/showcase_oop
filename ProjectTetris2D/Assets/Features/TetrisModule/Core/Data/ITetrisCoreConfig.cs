using System.Collections.Generic;

namespace Features.TetrisModule.Core.Data
{
    public interface ITetrisCoreConfig
    {
        int Width { get; }
        int Height { get; }
        float FastFallDelay { get; }
        int SpeedLevelsCount { get; }
        int StartTetrominoLevel { get; }
        int ShapesDifficultyLevelsCount { get; }

        float GetStandardFallDelay(int index);
        IReadOnlyList<CellState> GetTetrominoTypesByDifficulty(int index);
    }
}