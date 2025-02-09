using System;

namespace Features.UiViewMainScreenModule.Core.Controller
{
    public interface IUiViewMainScreenController
    {
        UiViewMainScreen UiViewScreen { get; }

        event Action ScreenEnabledEvent;
        event Action PlayButtonClickEvent;
        event Action SpeedLevelDecreaseButtonClickEvent;
        event Action SpeedLevelIncreaseButtonClickEvent;
        event Action TetrominoLevelDecreaseButtonClickEvent;
        event Action TetrominoLevelIncreaseButtonClickEvent;
        event Action StatisticsButtonClickEvent;
        event Action LanguageButtonClickEvent;

        void SetSpeedLevel(int value);
        void SetTetrominoLevel(int value);
    }
}