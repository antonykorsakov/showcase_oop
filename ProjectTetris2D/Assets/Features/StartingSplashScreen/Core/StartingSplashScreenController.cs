using System.Collections;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace Features.StartingSplashScreen.Core
{
    public class StartingSplashScreenController : IStartingSplashScreenController
    {
        private readonly IStartingSplashScreenConfig _config;
        private StartingSplashScreen _splashScreen;

        public StartingSplashScreenController(IStartingSplashScreenConfig config)
        {
            _config = config;
        }

        public bool FindSplashScreen()
        {
            _splashScreen = UObject.FindAnyObjectByType<StartingSplashScreen>(FindObjectsInactive.Exclude);
            return _splashScreen != null;
        }

        public void FadeOutSplashScreen()
        {
            if (_splashScreen != null)
                _splashScreen.StartCoroutine(FadeOutCoroutine());
        }

        private IEnumerator FadeOutCoroutine()
        {
            float duration = _config.FadeOutDuration / 1000f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                _splashScreen.CanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                yield return null;
            }

            _splashScreen.CanvasGroup.alpha = 0f;
            _splashScreen.gameObject.SetActive(false);
        }
    }
}