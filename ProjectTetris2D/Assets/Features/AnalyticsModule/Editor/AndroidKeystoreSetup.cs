using System;
using System.Collections.Generic;
using System.IO;
using Features.LogModule.Core;
using UnityEditor;
using UnityEngine;

namespace Features.AnalyticsModule.Editor
{
    public static class AndroidKeystoreSetup
    {
        private const string PropertiesFilePath = "c:/keys/tonycorp.tetris2d.properties";

        private const string KeystorePathPropertyName = "keystorePath";
        private const string KeystorePassPropertyName = "keystorePass";
        private const string KeyAliasPropertyName = "keyAlias";
        private const string KeyPassPropertyName = "keyPass";

        private static readonly string[] PropertyNames =
        {
            KeystorePathPropertyName,
            KeystorePassPropertyName,
            KeyAliasPropertyName,
            KeyPassPropertyName,
        };

        [InitializeOnLoadMethod]
        public static void SetupKeystoreOnLoadUnity()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                return;

            SetupKeystore();
        }

        [MenuItem("Tetris Tools/Setup Keystore for ANDROID")]
        public static void SetupKeystoreForAndroid()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                Log.Print("Keystore setup is only applicable for Android builds. Operation aborted.", LogType.Warning);
                return;
            }

            SetupKeystore();
        }

        private static void SetupKeystore()
        {
            if (!File.Exists(PropertiesFilePath))
            {
                Log.Print($"Keystore properties file not found at {PropertiesFilePath}.", LogType.Error);
                return;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(PropertiesFilePath);
            }
            catch (Exception ex)
            {
                Log.Print($"Error reading properties file: {ex.Message}", LogType.Error);
                return;
            }

            var keystoreProperties = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                foreach (var propertyName in PropertyNames)
                {
                    if (!line.StartsWith(propertyName))
                        continue;

                    var parts = line.Split('=');
                    if (parts.Length != 2)
                    {
                        Log.Print($"Invalid line format for '{propertyName}' in file: {line}", LogType.Error);
                        return;
                    }

                    var value = parts[1].Trim();
                    keystoreProperties[propertyName] = value;
                    break;
                }
            }

            foreach (var propertyName in PropertyNames)
            {
                var contains = keystoreProperties.TryGetValue(propertyName, out var value);
                if (!contains || string.IsNullOrWhiteSpace(value))
                {
                    Log.Print($"Keystore property '{propertyName}' is missing or empty.", LogType.Error);
                    return;
                }
            }

            PlayerSettings.Android.keystoreName = keystoreProperties[KeystorePathPropertyName];
            PlayerSettings.Android.keystorePass = keystoreProperties[KeystorePassPropertyName];
            PlayerSettings.Android.keyaliasName = keystoreProperties[KeyAliasPropertyName];
            PlayerSettings.Android.keyaliasPass = keystoreProperties[KeyPassPropertyName];

            Log.Print("Keystore successfully set up for Android!");
        }
    }
}