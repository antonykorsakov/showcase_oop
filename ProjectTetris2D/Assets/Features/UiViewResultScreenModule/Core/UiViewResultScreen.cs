using Features.Common.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UiViewResultScreenModule.Core
{
    public class UiViewResultScreen : UiViewScreen
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private TMP_Text _item1TComp;
        [SerializeField] private TMP_Text _item2TComp;
        [SerializeField] private TMP_Text _item3TComp;

        public Button ContinueButton => _continueButton;
        public Button RestartButton => _restartButton;
        public Button ExitButton => _exitButton;

        public TMP_Text Item1TComp => _item1TComp;
        public TMP_Text Item2TComp => _item2TComp;
        public TMP_Text Item3TComp => _item3TComp;
    }
}