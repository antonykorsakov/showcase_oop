using Features.LogModule.Core;
using Features.StatisticsModule.Behavior;
using Features.StatisticsModule.Core;
using Zenject;

namespace Features.StatisticsModule.Binding
{
    public class StatisticsInstall : MonoInstaller<StatisticsInstall>
    {
        private void Awake() => LogConfig.DisableLogging(typeof(StatisticsInstall));

        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<StatisticsBehavior>().AsSingle();

            // main controllers
            Container.BindInterfacesTo<StatisticsController>().AsSingle();
        }
    }
}