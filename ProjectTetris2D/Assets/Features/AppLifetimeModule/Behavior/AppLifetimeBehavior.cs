using Features.AppLifetimeModule.Core;
using Features.TetrisModule.Core.Controller;
using Features.UiLayersModule.Core.Controller;
using Features.UiViewMainScreenModule.Core.Controller;
using Features.UiViewResultScreenModule.Core;
using Features.UiViewTetrisGameScreenModule.Core;
using Features.ZenjectExtendModule.Core;
using UnityEngine.Localization.Settings;
using Zenject;

namespace Features.AppLifetimeModule.Behavior
{
    public class AppLifetimeBehavior : IInitializable
    {
        [Inject] private IAppLifetimeController AppLifetimeController { get; }
        [Inject] private ITetrisGridController TetrisGridController { get; }
        [Inject] private IUiLayersController UiLayersController { get; }
        [Inject] private IUiViewMainScreenController UiViewMainScreenController { get; }
        [Inject] private IUiViewResultScreenController UiViewResultScreenController { get; }
        [Inject] private IUiViewTetrisGameScreenController UiViewTetrisGameScreenController { get; }
        [Inject] private IZenjectLastController ZenjectLastController { get; }

        public void Initialize()
        {
            ZenjectLastController.InitializedEvent += TryActivateGdprState;
            UiLayersController.ActivatedEvent += TryActivateOutOfGameState;

            TetrisGridController.TetrominoFailedSpawnedEvent += () => ActivateState(AppState.GameOver);

            // MainScreen
            UiViewMainScreenController.PlayButtonClickEvent += () =>
            {
                ActivateState(AppState.PrepareGame);
                ActivateState(AppState.Game);
            };

            // ResultScreen
            UiViewResultScreenController.ContinueButtonClickEvent += () =>
            {
                ActivateState(AppState.AfterPause);
                ActivateState(AppState.Game);
            };
            UiViewResultScreenController.RestartButtonClickEvent += () =>
            {
                ActivateState(AppState.PrepareGame);
                ActivateState(AppState.Game);
            };
            UiViewResultScreenController.ExitButtonClickEvent += () => ActivateState(AppState.OutOfGame);

            // TetrisGameScreen
            UiViewTetrisGameScreenController.PauseBtnClickEvent += () => ActivateState(AppState.Pause);

            ActivateState(AppState.AppInitializing);
        }

        private void TryActivateGdprState()
        {
            if (!ZenjectLastController.Initialized)
                return;

            ActivateState(AppState.Gdpr);
        }

        private void TryActivateOutOfGameState()
        {
            if (!UiLayersController.IsActivated)
                return;

            if (!LocalizationSettings.InitializationOperation.IsDone)
                return;

            ActivateState(AppState.OutOfGame);
        }

        private void ActivateState(AppState value) => AppLifetimeController.SetState(value);
    }
}