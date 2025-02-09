using Features.JsonModule.Core;
using Features.StatisticsModule.Core;
using Zenject;

namespace Features.JsonModule.Behavior
{
    public sealed class JsonModuleBehavior : IInitializable
    {
        private readonly IJsonModuleConfig _config;

        public JsonModuleBehavior(IJsonModuleConfig config)
        {
            _config = config;
        }

        [Inject] private IJsonController JsonController { get; }
        [Inject] private IStatisticsController StatisticsController { get; }

        public void Initialize()
        {
            StatisticsController.StatsUpdatedEvent += Save;
        }

        private void Save()
        {
            var fileName = _config.StatisticsFileName;
            JsonController.TrySave(fileName, StatisticsController.GetGameStats);
        }
    }
}