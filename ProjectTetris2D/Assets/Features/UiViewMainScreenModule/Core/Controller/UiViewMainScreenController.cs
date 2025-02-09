using System;
using Features.Common.Core;
using Features.UiViewMainScreenModule.Core.Data;
using TMPro;

namespace Features.UiViewMainScreenModule.Core.Controller
{
    public class UiViewMainScreenController : UiScreenController<UiViewMainScreen>, IUiViewMainScreenController
    {
        private readonly IUiViewMainScreenConfig _config;

        public UiViewMainScreenController(IUiViewMainScreenConfig config)
        {
            _config = config;
        }

        public UiViewMainScreen UiViewScreen => Screen;

        public event Action ScreenEnabledEvent;
        public event Action PlayButtonClickEvent;
        public event Action SpeedLevelDecreaseButtonClickEvent;
        public event Action SpeedLevelIncreaseButtonClickEvent;
        public event Action TetrominoLevelDecreaseButtonClickEvent;
        public event Action TetrominoLevelIncreaseButtonClickEvent;
        public event Action StatisticsButtonClickEvent;
        public event Action LanguageButtonClickEvent;

        protected override void SetupScreen(UiViewMainScreen screen)
        {
            if (screen == null)
                return;

            screen.SpeedLevelSettingsItem.Setup(_config.SpeedSettingsLocale, -1,
                SpeedLevelDecreaseButtonClickEvent, SpeedLevelIncreaseButtonClickEvent);
            screen.TetrominoLevelSettingsItem.Setup(_config.TetrominoSettingsLocale, -1,
                TetrominoLevelDecreaseButtonClickEvent, TetrominoLevelIncreaseButtonClickEvent);
            screen.GridLevelSettingsItem.Setup(_config.GridSettingsLocale);
            screen.BonusLevelSettingsItem.Setup(_config.BonusSettingsLocale);

            screen.PlayButton.onClick.AddListener(() => PlayButtonClickEvent?.Invoke());
            screen.StatisticsButton.onClick.AddListener(() => StatisticsButtonClickEvent?.Invoke());
            screen.LanguageButton.onClick.AddListener(() => LanguageButtonClickEvent?.Invoke());

            ScreenEnabledEvent?.Invoke();
        }
        
        public void SetSpeedLevel(int value) => UpdateScreen(value, screen => screen.SpeedLevelSettingsItem.CountTComp);
        public void SetTetrominoLevel(int value) => UpdateScreen(value, screen => screen.TetrominoLevelSettingsItem.CountTComp);

        private void UpdateScreen(int value, Func<UiViewMainScreen, TMP_Text> getTComp)
        {
            if (Screen == null)
                return;

            getTComp(Screen).text = value.ToString();
        }
    }
}