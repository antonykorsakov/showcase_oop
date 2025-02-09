using UnityEngine;
using UnityEngine.UI;

namespace Features.UiViewTetrisGameScreenModule.Core
{
    [CreateAssetMenu(fileName = nameof(UiViewTetrisGameScreenConfig),
        menuName = "Configs/" + nameof(UiViewTetrisGameScreenConfig), order = 'U')]
    public class UiViewTetrisGameScreenConfig : ScriptableObject, IUiViewTetrisGameScreenConfig
    {
        [SerializeField] private Image _gridCell;

        public Image GridCell => _gridCell;
    }
}