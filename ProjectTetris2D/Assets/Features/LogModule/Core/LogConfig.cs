using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.LogModule.Core
{
    public static class LogConfig
    {
        private static readonly HashSet<string> DisabledModules = new();
        private static readonly object LockObject = new();

        static LogConfig()
        {
            Debug.unityLogger.logEnabled = Application.isEditor || Debug.isDebugBuild;
        }

        public static void DisableLogging(Type type)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            var moduleName = type.GetModuleName();
            if (moduleName == null)
                return;

            lock (LockObject)
            {
                DisabledModules.Add(moduleName);
            }
#endif
        }

        internal static bool IsLoggingEnabled(string moduleName)
        {
            lock (LockObject)
            {
                return !DisabledModules.Contains(moduleName);
            }
        }
    }
}