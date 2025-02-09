using Features.AppLifetimeModule.Core;
using Features.StatisticsModule.Core;
using Features.TetrisModule.Core.Controller;
using Features.UiViewTetrisGameScreenModule.Core;
using Zenject;

namespace Features.UiViewTetrisGameScreen.Behavior
{
    public class UiViewTetrisGameScreenBehavior : IInitializable
    {
        [Inject] private IAppLifetimeController AppLifetimeController { get; }
        [Inject] private IStatisticsController StatisticsController { get; }
        [Inject] private ITetrisItemsController TetrisItemsController { get; }
        [Inject] private ITetrisSpeedController TetrisSpeedController { get; }
        [Inject] private IUiViewTetrisGameScreenController UiViewTetrisGameScreenController { get; }

        public void Initialize()
        {
            AppLifetimeController.AppStateChangedEvent += () =>
            {
                switch (AppLifetimeController.AppState)
                {
                    case AppState.OutOfGame:
                        Disable();
                        break;

                    case AppState.PrepareGame:
                        Enable();
                        break;
                }
            };
        }

        private void Enable()
        {
            StatisticsController.StatsUpdatedEvent += UpdateScore;
            TetrisItemsController.NextTetrominoDataChangedEvent += UpdateNextTetromino;
            TetrisSpeedController.Level.ValueChangedEvent += UpdateSpeedLevel;
            TetrisItemsController.DifficultyLevel.ValueChangedEvent += UpdateDifficultyLevel;

            UpdateHiScore();
            UpdateScore();
            UpdateNextTetromino();
            UpdateSpeedLevel();
            UpdateDifficultyLevel();
        }

        private void Disable()
        {
            StatisticsController.StatsUpdatedEvent -= UpdateScore;
            TetrisItemsController.NextTetrominoDataChangedEvent -= UpdateNextTetromino;
            TetrisSpeedController.Level.ValueChangedEvent -= UpdateSpeedLevel;
            TetrisItemsController.DifficultyLevel.ValueChangedEvent -= UpdateDifficultyLevel;
        }

        private void UpdateHiScore()
        {
            var gameStats = StatisticsController.GetGameStats();
            UiViewTetrisGameScreenController.SetHiScore(gameStats.HiScore);
        }

        private void UpdateScore()
        {
            var sessionStats = StatisticsController.GetGameSessionStats();
            UiViewTetrisGameScreenController.SetScore(sessionStats.BurntLinesCount);
        }

        private void UpdateNextTetromino()
        {
            var nextShapeData = TetrisItemsController.NextTetrominoData.GetShape(0);
            UiViewTetrisGameScreenController.SetNextTetromino(nextShapeData);
        }

        private void UpdateSpeedLevel()
        {
            var speedLevel = TetrisSpeedController.Level.Value;
            UiViewTetrisGameScreenController.SetSpeedLevel(speedLevel);
        }

        private void UpdateDifficultyLevel()
        {
            var complexityLevel = TetrisItemsController.DifficultyLevel.Value;
            UiViewTetrisGameScreenController.SetComplexityLevel(complexityLevel);
        }
    }
}