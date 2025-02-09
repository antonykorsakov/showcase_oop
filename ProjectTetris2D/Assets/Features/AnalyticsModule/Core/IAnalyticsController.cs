using System;
using System.Collections.Generic;

namespace Features.AnalyticsModule.Core
{
    public interface IAnalyticsController
    {
        bool IsFirebaseInitialized { get; }
        event Action FirebaseInitializedEvent;

        void Initialize();
        void LogEvent(string name, Dictionary<string, object> parameters);
    }
}