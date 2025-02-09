using Features.UiLayersModule.Behavior;
using Features.UiLayersModule.Core.Controller;
using Features.UiLayersModule.Core.Data;
using UnityEngine;
using Zenject;

namespace Features.UiLayersModule.Binding
{
    public class UiLayersInstaller : MonoInstaller<UiLayersInstaller>
    {
        [SerializeField] private UiLayersConfig _config;

        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<UiLayersBehavior>().AsSingle().WithArguments(_config);

            // main controllers
            Container.BindInterfacesTo<UiLayersController>().AsSingle();
        }
    }
}