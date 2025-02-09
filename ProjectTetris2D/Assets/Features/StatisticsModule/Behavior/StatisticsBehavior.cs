using Cysharp.Threading.Tasks;
using Features.AppLifetimeModule.Core;
using Features.JsonModule.Core;
using Features.StatisticsModule.Core;
using Features.TetrisModule.Core.Controller;
using Zenject;

namespace Features.StatisticsModule.Behavior
{
    public class StatisticsBehavior : IInitializable
    {
        [Inject] private IAppLifetimeController AppLifetimeController { get; }
        [Inject] private IJsonController JsonController { get; }
        [Inject] private IStatisticsSetController StatisticsController { get; }
        [Inject] private ITetrisGridController TetrisGridController { get; }

        public void Initialize() => InitializeAsync().Forget();

        private async UniTaskVoid InitializeAsync()
        {
            await LoadStatisticsAsync();
            SubscribeToUpdates();
        }

        private async UniTask LoadStatisticsAsync()
        {
            var fileName = "statistics.json";
            var stats = await JsonController.TryLoad<GameStats>(fileName);
            StatisticsController.SetGameStats(stats);
        }

        private void SubscribeToUpdates()
        {
            AppLifetimeController.AppStateChangedEvent += () =>
            {
                if (AppLifetimeController.AppState == AppState.GameOver)
                    StatisticsController.UpdateGameStats();
                
                if (AppLifetimeController.AppState == AppState.Pause)
                    StatisticsController.UpdateGameStats();
            };

            TetrisGridController.GridClearedEvent += StatisticsController.NewGameSession;
            TetrisGridController.GridLinesBurntEvent += StatisticsController.AddBurntLines;
            TetrisGridController.TetrominoSuccessSpawnedEvent += StatisticsController.AddTetrominoCount;
        }
    }
}