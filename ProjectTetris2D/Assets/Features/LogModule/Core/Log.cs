using System;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace Features.LogModule.Core
{
    // about [HideInCallstack]:
    // https://docs.unity3d.com/ScriptReference/HideInCallstackAttribute.html
    public static class Log
    {
        [HideInCallstack]
        public static void Print(object msg, LogType logType = LogType.Log)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            DebugLog(msg.ToString(), null, logType);
#endif
        }

        [HideInCallstack]
        public static void Print(object msg, UObject item, LogType logType = LogType.Log)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            DebugLog(msg.ToString(), item, logType);
#endif
        }

        [HideInCallstack]
        public static void Print(string msg, LogType logType = LogType.Log)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            DebugLog(msg, null, logType);
#endif
        }

        [HideInCallstack]
        public static void Print(string msg, UObject item, LogType logType = LogType.Log)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            DebugLog(msg, item, logType);
#endif
        }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        [HideInCallstack]
        private static void DebugLog(string msg, UObject item, LogType logType)
        {
            string moduleName = GetModuleName();

            if (!LogConfig.IsLoggingEnabled(moduleName))
                return;

            string log = $"[Tony.{moduleName}] {msg}";
            switch (logType)
            {
                case LogType.Log:
                    Debug.Log(log, item);
                    break;

                case LogType.Warning:
                    Debug.LogWarning(log, item);
                    break;

                case LogType.Error:
                case LogType.Assert:
                case LogType.Exception:
                    Debug.LogError(log, item);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
            }
        }

        private static string GetModuleName()
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var frames = stackTrace.GetFrames();

            if (frames != null)
            {
                foreach (var frame in frames)
                {
                    var method = frame.GetMethod();
                    if (method == null)
                        continue;

                    var declaringType = method.DeclaringType;
                    if (declaringType == null)
                        continue;

                    if (declaringType == typeof(Log))
                        continue;

                    var moduleName = declaringType.GetModuleName();
                    return moduleName;
                }
            }

            return "UnknownModule";
        }
#endif
    }
}