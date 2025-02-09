using UnityEngine;

namespace Features.StartingSplashScreen.Core
{
    public class StartingSplashScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public CanvasGroup CanvasGroup => _canvasGroup;
    }
}