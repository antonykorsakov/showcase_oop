using System;
using Cysharp.Threading.Tasks;
using Features.LogModule.Core;
using Features.StartingSplashScreen.Core;
using Features.UiLayersModule.Core.Controller;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Zenject;

namespace Features.StartingSplashScreen.Behavior
{
    public class StartingSplashScreenBehavior : IInitializable
    {
        private readonly IStartingSplashScreenConfig _config;

        public StartingSplashScreenBehavior(IStartingSplashScreenConfig config)
        {
            _config = config;
        }

        [Inject] private IStartingSplashScreenController StartingSplashScreenController { get; }
        [Inject] private IUiLayersController UiLayersController { get; }

        public void Initialize()
        {
            UiLayersController.ActivatedEvent += FadeOut;
            LocalizationSettings.InitializationOperation.Completed += _ => FadeOut();

            FadeOut();
        }

        private void FadeOut()
        {
            if (!UiLayersController.IsActivated)
                return;

            if (!LocalizationSettings.InitializationOperation.IsDone)
                return;

            FadeOutAsync().Forget();
        }

        private async UniTaskVoid FadeOutAsync()
        {
            bool found = StartingSplashScreenController.FindSplashScreen();
            if (!found)
                return;

            try
            {
                await UniTask.Delay(_config.Delay);
            }
            catch (Exception e)
            {
                Log.Print($"An error occurred: {e.Message}\n{e.StackTrace}", LogType.Error);
            }

            StartingSplashScreenController.FadeOutSplashScreen();
        }
    }
}