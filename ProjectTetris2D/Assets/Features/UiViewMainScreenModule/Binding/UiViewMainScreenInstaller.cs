using Features.UiViewMainScreenModule.Behavior;
using Features.UiViewMainScreenModule.Core.Controller;
using Features.UiViewMainScreenModule.Core.Data;
using UnityEngine;
using Zenject;

namespace Features.UiViewMainScreenModule.Binding
{
    public class UiViewMainScreenInstaller : MonoInstaller<UiViewMainScreenInstaller>
    {
        [SerializeField] private UiViewMainScreenConfig _config;

        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<UiViewMainScreenBehavior>().AsSingle();

            // main controllers
            Container.BindInterfacesTo<UiViewMainScreenController>().AsSingle().WithArguments(_config);
        }
    }
}