using Features.Common.Core;
using UnityEngine;

namespace Features.UiLayersModule.Core.Data
{
    [CreateAssetMenu(fileName = nameof(UiLayersConfig),
        menuName = "Configs/" + nameof(UiLayersConfig), order = 'U')]
    public class UiLayersConfig : ScriptableObject, IUiLayersConfig
    {
        [SerializeField] private UiLayersContainer _uiLayersContainer;
        [SerializeField] private UiViewScreen[] _screensData;

        public UiLayersContainer UiLayersContainer => _uiLayersContainer;
        public UiViewScreen[] Screens => _screensData;
    }
}