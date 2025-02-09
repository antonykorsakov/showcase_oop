using UnityEngine;

namespace Features.Common.Core
{
    public class UiViewScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _mainCanvasGroup;

        public CanvasGroup MainCanvasGroup => _mainCanvasGroup;
    }
}