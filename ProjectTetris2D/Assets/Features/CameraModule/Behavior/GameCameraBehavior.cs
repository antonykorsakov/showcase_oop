using System.Threading;
using Cysharp.Threading.Tasks;
using Features.AppLifetimeModule.Core;
using Features.CameraModule.Core.Controller;
using Features.CameraModule.Core.Data;
using Features.LogModule.Core;
using Features.UiLayersModule.Core.Controller;
using Zenject;

namespace Features.CameraModule.Behavior
{
    public sealed class GameCameraBehavior : IInitializable
    {
        private static readonly CancellationToken NoneCancel = CancellationToken.None;
        private readonly ICameraConfig _config;

        public GameCameraBehavior(ICameraConfig config)
        {
            _config = config;
        }

        [Inject] private IAppLifetimeController AppLifetimeController { get; }
        [Inject] private ICameraStackController CameraStackController { get; }
        [Inject] private IUiLayersController UiLayersController { get; }

        public void Initialize()
        {
            AppLifetimeController.AppStateChangedEvent += CreateGameCamera;
            UiLayersController.ActivatedEvent += () => CameraStackController.SetUiCamera(UiLayersController.UiCamera);
        }

        private void CreateGameCamera()
        {
            if (AppLifetimeController.AppState == AppState.AppInitializing)
                return;

            AppLifetimeController.AppStateChangedEvent -= CreateGameCamera;
            CreateGameCameraAsync().Forget();
        }

        private async UniTaskVoid CreateGameCameraAsync()
        {
            var gameCamera = await FactoryAsync.InstantiateAsync(_config.GameCamera, null, NoneCancel);
            CameraStackController.SetGameCamera(gameCamera);
        }
    }
}