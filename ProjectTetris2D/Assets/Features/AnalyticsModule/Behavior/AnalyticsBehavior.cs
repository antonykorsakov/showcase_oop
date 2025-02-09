using System.Collections.Generic;
using Features.AnalyticsModule.Core;
using UnityEngine;
using Zenject;

namespace Features.AnalyticsModule.Behavior
{
    public sealed class AnalyticsBehavior : IInitializable
    {
        [Inject] private IAnalyticsController AnalyticsController { get; }

        public void Initialize()
        {
            if (!AnalyticsController.IsFirebaseInitialized)
            {
                AnalyticsController.FirebaseInitializedEvent += SendFirebaseEvent;
                AnalyticsController.Initialize();
            }
            else
            {
                SendFirebaseEvent();
            }
        }

        private void SendFirebaseEvent()
        {
            if (!AnalyticsController.IsFirebaseInitialized)
                return;

            var osVersion = SystemInfo.operatingSystem;
            var osType = SystemInfo.operatingSystemFamily.ToString();
            var languageZone = Application.systemLanguage.ToString();

            var eventParams = new Dictionary<string, object>
            {
                { "os_version", osVersion },
                { "os_type", osType },
                { "language_zone", languageZone }
            };

            AnalyticsController.LogEvent("Xxx", eventParams);
        }
    }
}