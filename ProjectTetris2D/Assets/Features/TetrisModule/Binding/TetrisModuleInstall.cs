using Features.TetrisModule.Behavior;
using Features.TetrisModule.Core.Controller;
using Features.TetrisModule.Core.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Features.TetrisModule.Binding
{
    public sealed class TetrisModuleInstall : MonoInstaller<TetrisModuleInstall>
    {
        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private TetrisCoreConfig _config;

        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<TetrisModuleBehavior>().AsSingle().WithArguments(_inputActionAsset);

            // main controllers
            Container.BindInterfacesTo<TetrisManager>().AsSingle();
            Container.BindInterfacesTo<TetrisGridController>().AsSingle().WithArguments(_config);
            Container.BindInterfacesTo<TetrisItemsController>().AsSingle().WithArguments(_config);
            Container.BindInterfacesTo<TetrisSpeedController>().AsSingle().WithArguments(_config);
        }
    }
}