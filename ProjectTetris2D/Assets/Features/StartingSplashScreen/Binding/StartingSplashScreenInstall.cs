using Features.StartingSplashScreen.Behavior;
using Features.StartingSplashScreen.Core;
using UnityEngine;
using Zenject;

namespace Features.StartingSplashScreen.Binding
{
    public class StartingSplashScreenInstall : MonoInstaller<StartingSplashScreenInstall>
    {
        [SerializeField] private StartingSplashScreenConfig _config;

        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<StartingSplashScreenBehavior>().AsSingle().WithArguments(_config);

            // main controllers
            Container.BindInterfacesTo<StartingSplashScreenController>().AsSingle().WithArguments(_config);
        }
    }
}