using System;
using Features.LogModule.Core;
using UnityEngine;

namespace Features.StatisticsModule.Core
{
    public class StatisticsController : IStatisticsSetController
    {
        private GameStats _gameStats;
        private GameSessionStats _currentGameSessionStats;

        public event Action GameStatsPreparedEvent;
        public event Action GameSessionStatsPreparedEvent;
        public event Action StatsUpdatedEvent;

        public GameStats GetGameStats() => _gameStats;
        public GameSessionStats GetGameSessionStats() => _currentGameSessionStats;

        public void SetGameStats(GameStats value)
        {
            if (value == null)
            {
                _gameStats = new();
                Log.Print("GameStats: set new();");
            }
            else
            {
                _gameStats = value;
                Log.Print("GameStats: set old value;");
            }

            GameStatsPreparedEvent?.Invoke();
        }

        public void UpdateGameStats()
        {
            var maxScore = Mathf.Max(_gameStats.HiScore, _currentGameSessionStats.BurntLinesCount);
            _gameStats.HiScore = maxScore;
            CallStatisticsUpdated();
        }

        public void NewGameSession()
        {
            _currentGameSessionStats = new();
            GameSessionStatsPreparedEvent?.Invoke();
        }

        public void AddBurntLines(int value)
        {
            if (value <= 0)
                return;

            _currentGameSessionStats.BurntLinesCount += value;
            CallStatisticsUpdated();
        }

        public void AddTetrominoCount()
        {
            _currentGameSessionStats.SpawnedTetrominoCount++;
            CallStatisticsUpdated();
        }

        public void SetGameDuration(int durationInMilliseconds)
        {
            _currentGameSessionStats.GameDurationInMS = durationInMilliseconds;
            CallStatisticsUpdated();
        }

        private void CallStatisticsUpdated()
        {
            Log.Print($"Updated stats; {_currentGameSessionStats}");
            StatsUpdatedEvent?.Invoke();
        }
    }
}