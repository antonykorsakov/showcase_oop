using System;
using System.Collections.Generic;
using Features.LogModule.Core;

namespace Features.AnalyticsModule.Core
{
    public abstract class AnalyticsController : IAnalyticsController
    {
        public bool IsFirebaseInitialized { get; private set; }
        public event Action FirebaseInitializedEvent;

        public abstract void Initialize();
        public abstract void LogEvent(string name, Dictionary<string, object> parameters);

        protected void SuccessInitialize()
        {
            IsFirebaseInitialized = true;
            Log.Print("Firebase initialized;");

            FirebaseInitializedEvent?.Invoke();
        }
    }
}