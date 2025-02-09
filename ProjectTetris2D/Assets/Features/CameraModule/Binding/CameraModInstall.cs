using Features.CameraModule.Behavior;
using Features.CameraModule.Core.Controller;
using Features.CameraModule.Core.Data;
using UnityEngine;
using Zenject;

namespace Features.CameraModule.Binding
{
    public sealed class CameraModInstall : MonoInstaller<CameraModInstall>
    {
        [SerializeField] private CameraConfig _config;

        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<GameCameraBehavior>().AsSingle().WithArguments(_config);

            // main controllers
            Container.BindInterfacesTo<CameraStackController>().AsSingle();
        }
    }
}