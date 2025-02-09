using Features.LocalizationModule.Behavior;
using Zenject;

namespace Features.LocalizationModule.Binding
{
    public class LocalizationInstaller : MonoInstaller<LocalizationInstaller>
    {
        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<LocalizationBehavior>().AsSingle();
        }
    }
}