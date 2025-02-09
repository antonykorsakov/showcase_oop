using System;
using Features.LogModule.Core;

namespace Features.AppLifetimeModule.Core
{
    public sealed class AppLifetimeController : IAppLifetimeController
    {
        public AppState AppState { get; private set; }
        public AppState NextAppState { get; private set; }

        public event Action AppStateChangingEvent;
        public event Action AppStateChangedEvent;

        public void SetState(AppState value)
        {
            if (AppState == value)
                return;

            Log.Print($"app state to -> {value};");

            NextAppState = value;
            AppStateChangingEvent?.Invoke();
            AppState = NextAppState;
            NextAppState = AppState.Undefined;
            AppStateChangedEvent?.Invoke();
        }
    }
}