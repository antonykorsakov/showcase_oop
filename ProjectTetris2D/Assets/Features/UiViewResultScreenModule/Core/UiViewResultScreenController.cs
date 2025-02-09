using System;
using Features.Common.Core;
using TMPro;

namespace Features.UiViewResultScreenModule.Core
{
    public class UiViewResultScreenController : UiScreenController<UiViewResultScreen>, IUiViewResultScreenController
    {
        public UiViewResultScreen UiViewScreen => Screen;

        public event Action ContinueButtonClickEvent;
        public event Action RestartButtonClickEvent;
        public event Action ExitButtonClickEvent;

        protected override void SetupScreen(UiViewResultScreen screen)
        {
            screen.ContinueButton.onClick.AddListener(() => ContinueButtonClickEvent?.Invoke());
            screen.RestartButton.onClick.AddListener(() => RestartButtonClickEvent?.Invoke());
            screen.ExitButton.onClick.AddListener(() => ExitButtonClickEvent?.Invoke());
        }

        public void SetStats(IGameSessionStats stats)
        {
            if (stats != null)
            {
                UpdatePanel(stats.BurntLinesCount, screen => screen.Item1TComp);
                UpdatePanel(stats.SpawnedTetrominoCount, screen => screen.Item2TComp);
                UpdatePanel(stats.GameDurationInMS, screen => screen.Item3TComp);
            }
            else
            {
                UpdatePanel(0, screen => screen.Item1TComp);
                UpdatePanel(0, screen => screen.Item2TComp);
                UpdatePanel(0, screen => screen.Item3TComp);
            }
        }

        private void UpdatePanel(int value, Func<UiViewResultScreen, TMP_Text> getTComp)
        {
            if (Screen == null)
                return;

            getTComp(Screen).text = value.ToString();
        }
    }
}