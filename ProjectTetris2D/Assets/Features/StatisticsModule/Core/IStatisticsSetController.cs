namespace Features.StatisticsModule.Core
{
    // GameStats -> GameSessionStats
    public interface IStatisticsSetController : IStatisticsController
    {
        // GameStats
        void SetGameStats(GameStats value);
        void UpdateGameStats();

        // GameSessionStats
        void NewGameSession();
        void AddBurntLines(int value);
        void AddTetrominoCount();
        void SetGameDuration(int durationInMilliseconds);
    }
}