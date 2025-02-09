using Features.Common.Core;

namespace Features.UiScreensNavigator.Core
{
    public interface IUiScreensNavigatorController
    {
        void FadeIn(UiViewScreen uiViewScreen, float duration = 0);
        void FadeOut(UiViewScreen uiViewScreen, float duration = 0);
    }
}