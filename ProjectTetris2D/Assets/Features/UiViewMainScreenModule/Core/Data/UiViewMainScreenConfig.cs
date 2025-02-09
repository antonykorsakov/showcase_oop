using UnityEngine;
using UnityEngine.Localization;

namespace Features.UiViewMainScreenModule.Core.Data
{
    [CreateAssetMenu(fileName = nameof(UiViewMainScreenConfig),
        menuName = "Configs/" + nameof(UiViewMainScreenConfig), order = 'U')]
    public class UiViewMainScreenConfig : ScriptableObject, IUiViewMainScreenConfig
    {
        [SerializeField] private LocalizedString _speedSettingsTitleKey;
        [SerializeField] private LocalizedString _tetrominoSettingsLocale;
        [SerializeField] private LocalizedString _gidSettingsLocale;
        [SerializeField] private LocalizedString _bonusSettingsLocale;

        public LocalizedString SpeedSettingsLocale => _speedSettingsTitleKey;
        public LocalizedString TetrominoSettingsLocale => _tetrominoSettingsLocale;
        public LocalizedString GridSettingsLocale => _gidSettingsLocale;
        public LocalizedString BonusSettingsLocale => _bonusSettingsLocale;
    }
}