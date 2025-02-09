using Features.UiViewTetrisGameScreen.Behavior;
using Features.UiViewTetrisGameScreenModule.Core;
using UnityEngine;
using Zenject;

namespace Features.UiViewTetrisGameScreen.Binding
{
    public class UiViewTetrisGameScreenInstall : MonoInstaller<UiViewTetrisGameScreenInstall>
    {
        [SerializeField] private UiViewTetrisGameScreenConfig _config;

        public override void InstallBindings()
        {
            // behaviors
            Container.BindInterfacesTo<UiViewTetrisGameScreenBehavior>().AsSingle();

            // main controllers
            Container.BindInterfacesTo<UiViewTetrisGameScreenController>().AsSingle().WithArguments(_config);
        }
    }
}