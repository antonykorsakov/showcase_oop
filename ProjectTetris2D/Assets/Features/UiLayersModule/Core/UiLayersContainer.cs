using UnityEngine;

namespace Features.UiLayersModule.Core
{
    public class UiLayersContainer : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Camera _camera;

        public Canvas Canvas => _canvas;
        public Camera Camera => _camera;
    }
}