using System.Collections.Generic;
using Features.LogModule.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace Features.AnalyticsModule.Core
{
    public sealed class AnalyticsWebController : AnalyticsController
    {
        public override void Initialize()
        {
            bool isReady = CheckFirebaseReady() == 1;
            if (isReady)
            {
                Log.Print("Firebase successfully initialized in WebGL.");
                SuccessInitialize();
            }
            else
            {
                Log.Print("Firebase initialization failed in WebGL.", LogType.Error);
            }
        }

        public override void LogEvent(string name, Dictionary<string, object> parameters)
        {
            string jsonParameters = JsonConvert.SerializeObject(parameters);
            LogFirebaseEvent(name, jsonParameters);
        }

#if UNITY_WEBGL || UNITY_EDITOR
        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern int CheckFirebaseReady();

        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern void LogFirebaseEvent(string eventName, string json);
#else
        private static int CheckFirebaseReady() => 0;

        private static void LogFirebaseEvent(string eventName, string json)
        { }
#endif
    }
}