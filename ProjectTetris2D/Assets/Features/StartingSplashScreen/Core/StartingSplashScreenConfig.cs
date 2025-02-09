using UnityEngine;

namespace Features.StartingSplashScreen.Core
{
    [CreateAssetMenu(fileName = nameof(StartingSplashScreenConfig),
        menuName = "Configs/" + nameof(StartingSplashScreenConfig), order = 'S')]
    public class StartingSplashScreenConfig : ScriptableObject, IStartingSplashScreenConfig
    {
        [SerializeField, Range(0, 4000)] private int _delay = 1000;
        [SerializeField, Range(0, 4000)] private int _fadeOutDuration = 2000;

        public int Delay => _delay;
        public int FadeOutDuration => _fadeOutDuration;
    }
}