using UnityEngine;

namespace Features.Common.Core
{
    public abstract class UiScreenController<TScreen> : IUiScreenController
        where TScreen : UiViewScreen
    {
        protected TScreen Screen { get; private set; }

        public void SetScreen(UiViewScreen[] loadedScreens)
        {
            foreach (UiViewScreen item in loadedScreens)
            {
                Screen = item as TScreen;
                if (Screen != null)
                    break;
            }

            if (Screen == null)
            {
                Debug.LogError($"Couldn't find '{typeof(TScreen).Name}' after loaded screens");
                return;
            }

            SetupScreen(Screen);
        }

        protected abstract void SetupScreen(TScreen screen);
    }
}