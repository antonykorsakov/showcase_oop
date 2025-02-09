using Features.AppLifetimeModule.Core;
using Features.UiLayersModule.Core.Controller;
using Features.UiScreensNavigator.Core;
using Features.UiViewMainScreenModule.Core;
using Features.UiViewMainScreenModule.Core.Controller;
using Features.UiViewResultScreenModule.Core;
using Zenject;

namespace Features.UiScreensNavigator.Behavior
{
    public class UiScreensNavigatorBehavior : IInitializable
    {
        [Inject] private IAppLifetimeController AppLifetimeController { get; }
        [Inject] private IUiLayersController UiLayersController { get; }
        [Inject] private IUiScreensNavigatorController UiScreensNavigatorController { get; }

        [Inject] private IUiViewMainScreenController UiViewMainScreenController { get; }
        [Inject] private IUiViewResultScreenController UiViewResultScreenController { get; }

        private UiViewMainScreen MainScreen => UiViewMainScreenController.UiViewScreen;
        private UiViewResultScreen ResultScreen => UiViewResultScreenController.UiViewScreen;

        public void Initialize()
        {
            UiLayersController.ActivatedEvent += () =>
            {
                AppLifetimeController.AppStateChangedEvent += UpdateUiScreens;
                UpdateUiScreens();
            };
        }

        private void UpdateUiScreens()
        {
            switch (AppLifetimeController.AppState)
            {
                case AppState.OutOfGame:
                    UiScreensNavigatorController.FadeIn(MainScreen, 0.2f);
                    UiScreensNavigatorController.FadeOut(ResultScreen, 0f);
                    break;

                case AppState.PrepareGame:
                    UiScreensNavigatorController.FadeOut(MainScreen, 0.2f);
                    UiScreensNavigatorController.FadeOut(ResultScreen, 0f);
                    break;

                case AppState.GameOver:
                    ResultScreen.ContinueButton.gameObject.SetActive(false);
                    UiScreensNavigatorController.FadeIn(ResultScreen, 0f);
                    break;

                case AppState.Pause:
                    ResultScreen.ContinueButton.gameObject.SetActive(true);
                    UiScreensNavigatorController.FadeIn(ResultScreen, 0f);
                    break;

                case AppState.AfterPause:
                    UiScreensNavigatorController.FadeOut(ResultScreen, 0f);
                    break;
            }
        }
    }
}