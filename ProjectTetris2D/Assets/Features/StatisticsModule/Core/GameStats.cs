using System.Text;
using Features.Common.Core;
using Newtonsoft.Json;

namespace Features.StatisticsModule.Core
{
    public class GameStats : IGameStats
    {
        [JsonProperty("hi_score")] private int _hiScore;
        [JsonProperty("game_session_stats")] private GameSessionStats[] _gameSessionStats;

        [JsonIgnore] private readonly StringBuilder _sb = new();

        [JsonIgnore]
        public int HiScore
        {
            get => _hiScore;
            internal set => _hiScore = value;
        }
    }
}