using System;
using System.IO;
using Features.LogModule.Core;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Features.AnalyticsModule.Editor
{
    public static class WebPostProcessBuild
    {
        [PostProcessBuild]
        public static void SetupIndexHtmlOnBuildProcess(BuildTarget target, string pathToBuildProject)
        {
            if (target != BuildTarget.WebGL)
                return;

            SetupIndexHtml(pathToBuildProject);
        }

        private static void SetupIndexHtml(string pathToBuildProject)
        {
            string indexPath = Path.Combine(pathToBuildProject, "index.html");

            if (!File.Exists(indexPath))
            {
                Log.Print($"File not found: {indexPath}", LogType.Error);
                return;
            }

            string htmlContent = File.ReadAllText(indexPath);

            string firebaseScripts = @"
            <!-- Firebase Initialization -->
            <script type='module'>
                console.log('Firebase start initialize;');
                import { initializeApp } from 'https://www.gstatic.com/firebasejs/11.2.0/firebase-app.js';
                import { getAnalytics } from 'https://www.gstatic.com/firebasejs/11.2.0/firebase-analytics.js';

                const firebaseConfig = {
                    apiKey: 'AIzaSyAfFqb9CS5nC-ohoY8JPjI9jICmJVmVP3o',
                    authDomain: 'tony-showcase-game2d.firebaseapp.com',
                    projectId: 'tony-showcase-game2d',
                    storageBucket: 'tony-showcase-game2d.firebasestorage.app',
                    messagingSenderId: '39020812732',
                    appId: '1:39020812732:web:280380b7d965d6076c73b9',
                    measurementId: 'G-H7ELK02CFL'
                };

                const app = initializeApp(firebaseConfig);
                const analytics = getAnalytics(app);
                console.log('Firebase initialized with ES modules');
            </script>
            ";

            htmlContent = htmlContent.Replace("</head>", firebaseScripts + "\n</head>");

            try
            {
                File.WriteAllText(indexPath, htmlContent);
                Log.Print("Firebase scripts successfully added to index.html for WebGL.");
            }
            catch (Exception ex)
            {
                Log.Print($"Error modifying index.html: {ex.Message}", LogType.Error);
            }
        }
    }
}