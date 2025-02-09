using Features.UiViewResultScreenModule.Behavior;
using Features.UiViewResultScreenModule.Core;
using Zenject;

namespace Features.UiViewResultScreenModule.Binding
{
    public class UiViewResultScreenInstall : MonoInstaller<UiViewResultScreenInstall>
    {
        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<UiViewResultScreenBehavior>().AsSingle();

            // main controllers
            Container.BindInterfacesTo<UiViewResultScreenController>().AsSingle();
        }
    }
}