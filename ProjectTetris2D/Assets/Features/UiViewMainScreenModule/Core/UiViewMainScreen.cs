using Features.Common.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UiViewMainScreenModule.Core
{
    public sealed class UiViewMainScreen : UiViewScreen
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private SettingsItem _speedLevelSettingsItem;
        [SerializeField] private SettingsItem _tetrominoLevelSettingsItem;
        [SerializeField] private SettingsItem _gridLevelSettingsItem;
        [SerializeField] private SettingsItem _bonusLevelSettingsItem;
        [SerializeField] private Button _statisticsButton;
        [SerializeField] private Button _languageButton;

        public Button PlayButton => _playButton;
        public SettingsItem SpeedLevelSettingsItem => _speedLevelSettingsItem;
        public SettingsItem TetrominoLevelSettingsItem => _tetrominoLevelSettingsItem;
        public SettingsItem GridLevelSettingsItem => _gridLevelSettingsItem;
        public SettingsItem BonusLevelSettingsItem => _bonusLevelSettingsItem;
        public Button StatisticsButton => _statisticsButton;
        public Button LanguageButton => _languageButton;
    }
}