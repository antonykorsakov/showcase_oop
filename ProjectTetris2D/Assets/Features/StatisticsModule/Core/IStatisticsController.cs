using System;

namespace Features.StatisticsModule.Core
{
    public interface IStatisticsController
    {
        event Action GameStatsPreparedEvent;
        event Action GameSessionStatsPreparedEvent;
        event Action StatsUpdatedEvent;

        GameStats GetGameStats();
        GameSessionStats GetGameSessionStats();
    }
}