using Features.ZenjectExtendModule.Behavior;
using Features.ZenjectExtendModule.Core;
using Zenject;

namespace Features.ZenjectExtendModule.Binding
{
    public class ZenjectExtendInstaller : MonoInstaller<ZenjectExtendInstaller>
    {
        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<ZenjectExtendBehavior>().AsSingle();

            // main controllers
            Container.BindInterfacesTo<ZenjectLastController>().AsSingle();
        }
    }
}