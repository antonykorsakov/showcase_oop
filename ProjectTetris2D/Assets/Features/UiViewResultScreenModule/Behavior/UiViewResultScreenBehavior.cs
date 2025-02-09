using Features.StatisticsModule.Core;
using Features.UiViewResultScreenModule.Core;
using Zenject;

namespace Features.UiViewResultScreenModule.Behavior
{
    public class UiViewResultScreenBehavior : IInitializable
    {
        [Inject] private IStatisticsController StatisticsController { get; }
        [Inject] private IUiViewResultScreenController UiViewResultScreenController { get; }

        public void Initialize()
        {
            StatisticsController.StatsUpdatedEvent += SetStats;
            SetStats();
        }

        private void SetStats()
        {
            var sessionStats = StatisticsController.GetGameSessionStats();
            UiViewResultScreenController.SetStats(sessionStats);
        }
    }
}