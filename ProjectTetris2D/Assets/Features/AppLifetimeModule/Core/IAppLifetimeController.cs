using System;

namespace Features.AppLifetimeModule.Core
{
    public interface IAppLifetimeController
    {
        AppState AppState { get; }
        AppState NextAppState { get; }

        event Action AppStateChangingEvent;
        event Action AppStateChangedEvent;

        void SetState(AppState value);
    }
}