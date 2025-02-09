using System.Collections.Generic;
using Features.LogModule.Core;
using UnityEngine;

#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
using Firebase;
using Firebase.Analytics;
#endif

namespace Features.AnalyticsModule.Core
{
    public sealed class AnalyticsDeviceController : AnalyticsController
    {
        public override void Initialize()
        {
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                if (task.Result == DependencyStatus.Available)
                    SuccessInitialize();
                else
                    Log.Print("Could not resolve all Firebase dependencies: " + task.Result, LogType.Error);
            });
#endif
        }

        public override void LogEvent(string name, Dictionary<string, object> parameters)
        {
            Log.Print($"LogEvent '{name}', parameters count = {parameters.Count};");

#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
            var parameterArray = new Parameter[parameters.Count];
            int i = 0;

            foreach (var kvp in parameters)
            {
                parameterArray[i] = new Parameter(kvp.Key, kvp.Value.ToString());
                i++;
            }

            FirebaseAnalytics.LogEvent(name, parameterArray);
#endif
        }
    }
}