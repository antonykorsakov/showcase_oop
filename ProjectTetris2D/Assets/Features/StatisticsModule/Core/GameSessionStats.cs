using System.Text;
using Features.Common.Core;
using Newtonsoft.Json;

namespace Features.StatisticsModule.Core
{
    public sealed class GameSessionStats : IGameSessionStats
    {
        [JsonProperty("burnt_lines_count")] private int _burntLinesCount;
        [JsonProperty("spawned_tetromino_count")] private int _spawnedTetrominoCount;
        [JsonProperty("game_duration")] private int _gameDurationInMS;

        [JsonIgnore] private readonly StringBuilder _sb = new();

        [JsonIgnore]
        public int BurntLinesCount
        {
            get => _burntLinesCount;
            internal set => _burntLinesCount = value;
        }

        [JsonIgnore]
        public int SpawnedTetrominoCount
        {
            get => _spawnedTetrominoCount;
            internal set => _spawnedTetrominoCount = value;
        }

        [JsonIgnore]
        public int GameDurationInMS
        {
            get => _gameDurationInMS;
            internal set => _gameDurationInMS = value;
        }

        public override string ToString()
        {
            _sb.Clear()
                .Append("Stats: ")
                .Append("Lines = ").Append(BurntLinesCount).Append(", ")
                .Append("Tetromino = ").Append(SpawnedTetrominoCount).Append(", ")
                .Append("Game Duration = ").Append(GameDurationInMS)
                .Append(";");

            return _sb.ToString();
        }
    }
}