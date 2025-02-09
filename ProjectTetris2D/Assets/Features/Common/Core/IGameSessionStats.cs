namespace Features.Common.Core
{
    public interface IGameSessionStats
    {
        public int BurntLinesCount { get; }
        public int SpawnedTetrominoCount { get; }
        public int GameDurationInMS { get; }
    }
}