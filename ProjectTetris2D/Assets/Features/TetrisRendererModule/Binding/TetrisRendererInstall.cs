using Features.TetrisRendererModule.Behavior;
using Features.TetrisRendererModule.Core;
using UnityEngine;
using Zenject;

namespace Features.TetrisRendererModule.Binding
{
    public class TetrisRendererInstall : MonoInstaller<TetrisRendererInstall>
    {
        [SerializeField] private TetrisRendererConfig _config;

        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<TetrisRendererBehavior>().AsSingle();

            // main controllers
            Container.BindInterfacesTo<TetrisGridRendererController>().AsSingle().WithArguments(_config);
        }
    }
}