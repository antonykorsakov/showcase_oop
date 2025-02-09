using System;
using Cysharp.Threading.Tasks;
using Features.Common.Core;
using UnityEngine;

namespace Features.UiScreensNavigator.Core
{
    public class UiScreensNavigatorController : IUiScreensNavigatorController
    {
        public void FadeIn(UiViewScreen uiViewScreen, float duration = 0)
            => FadeAsync(uiViewScreen.MainCanvasGroup, 1, duration).Forget();

        public void FadeOut(UiViewScreen uiViewScreen, float duration = 0)
            => FadeAsync(uiViewScreen.MainCanvasGroup, 0, duration).Forget();

        private async UniTaskVoid FadeAsync(CanvasGroup canvasGroup, float targetAlpha, float duration)
        {
            if (canvasGroup == null)
                throw new ArgumentNullException(nameof(canvasGroup));

            if (duration < Mathf.Epsilon)
            {
                SwitchActiveState(canvasGroup, targetAlpha);
                return;
            }

            float startAlpha = canvasGroup.alpha;
            float elapsedTime = 0f;
            canvasGroup.gameObject.SetActive(true);

            try
            {
                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);

                    await UniTask.Yield(PlayerLoopTiming.Update);
                }

                SwitchActiveState(canvasGroup, targetAlpha);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Animation cancelled");
            }
        }

        private void SwitchActiveState(CanvasGroup canvasGroup, float targetAlpha)
        {
            canvasGroup.alpha = targetAlpha;

            bool isActive = targetAlpha > 0;
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
            canvasGroup.gameObject.SetActive(isActive);
        }
    }
}