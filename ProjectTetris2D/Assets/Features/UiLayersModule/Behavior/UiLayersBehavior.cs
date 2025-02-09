using System.Threading;
using Cysharp.Threading.Tasks;
using Features.AppLifetimeModule.Core;
using Features.Common.Core;
using Features.LogModule.Core;
using Features.UiLayersModule.Core.Controller;
using Features.UiLayersModule.Core.Data;
using Zenject;

namespace Features.UiLayersModule.Behavior
{
    public class UiLayersBehavior : IInitializable
    {
        private static readonly CancellationToken NoneCancel = CancellationToken.None;
        private readonly IUiLayersConfig _config;

        public UiLayersBehavior(IUiLayersConfig config)
        {
            _config = config;
        }

        [Inject] private IAppLifetimeController AppLifetimeController { get; }
        [Inject] private IUiLayersController UiLayersController { get; }
        [Inject] private IUiScreenController[] UiScreenControllers { get; }

        public void Initialize()
        {
            AppLifetimeController.AppStateChangedEvent += CreateUi;
        }

        private void CreateUi()
        {
            if (AppLifetimeController.AppState == AppState.AppInitializing)
                return;

            AppLifetimeController.AppStateChangedEvent -= CreateUi;
            CreateUiAsync().Forget();
        }

        private async UniTaskVoid CreateUiAsync()
        {
            var uiLayersContainer = await FactoryAsync.InstantiateAsync(_config.UiLayersContainer, null, NoneCancel);
            UiLayersController.SetupContainer(uiLayersContainer);

            var screenTasks = new UniTask<UiViewScreen>[_config.Screens.Length];

            for (var index = 0; index < _config.Screens.Length; index++)
            {
                var screenSource = _config.Screens[index];
                var canvas = uiLayersContainer.Canvas.transform;
                var instantiateAsync = FactoryAsync.InstantiateAsync(screenSource, canvas, NoneCancel);

                screenTasks[index] = instantiateAsync;
            }

            var screens = await UniTask.WhenAll(screenTasks);

            for (var index = 0; index < UiScreenControllers.Length; index++)
            {
                // correct order
                screens[index].transform.SetSiblingIndex(index);

                // send all screens to every ScreenController
                UiScreenControllers[index].SetScreen(screens);
            }

            UiLayersController.Activate();
        }
    }
}