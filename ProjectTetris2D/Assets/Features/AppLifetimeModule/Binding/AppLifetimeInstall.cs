using Features.AppLifetimeModule.Behavior;
using Features.AppLifetimeModule.Core;
using Zenject;

namespace Features.AppLifetimeModule.Binding
{
    public sealed class AppLifetimeInstall : MonoInstaller<AppLifetimeInstall>
    {
        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<AppLifetimeBehavior>().AsSingle();

            // main controllers
            Container.BindInterfacesTo<AppLifetimeController>().AsSingle();
        }
    }
}