using UnityEngine.Localization;

namespace Features.UiViewMainScreenModule.Core.Data
{
    public interface IUiViewMainScreenConfig
    {
        LocalizedString SpeedSettingsLocale { get; }
        LocalizedString TetrominoSettingsLocale { get; }
        LocalizedString GridSettingsLocale { get; }
        LocalizedString BonusSettingsLocale { get; }
    }
}