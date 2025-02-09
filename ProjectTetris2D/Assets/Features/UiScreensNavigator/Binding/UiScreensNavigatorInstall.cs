using Features.UiScreensNavigator.Behavior;
using Features.UiScreensNavigator.Core;
using Zenject;

namespace Features.UiScreensNavigator.Binding
{
    public class UiScreensNavigatorInstall : MonoInstaller<UiScreensNavigatorInstall>
    {
        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<UiScreensNavigatorBehavior>().AsSingle();

            // main controllers
            Container.BindInterfacesTo<UiScreensNavigatorController>().AsSingle();
        }
    }
}