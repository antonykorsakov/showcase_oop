using System;
using Features.Common.Core;
using Features.TetrisModule.Core.Data;
using TMPro;

namespace Features.UiViewTetrisGameScreenModule.Core
{
    public class UiViewTetrisGameScreenController : UiScreenController<UiViewTetrisGameScreen>,
        IUiViewTetrisGameScreenController
    {
        private readonly IUiViewTetrisGameScreenConfig _config;

        public UiViewTetrisGameScreenController(IUiViewTetrisGameScreenConfig config)
        {
            _config = config;
        }

        public UiViewTetrisGameScreen UiViewScreen => Screen;

        public event Action PauseBtnClickEvent;
        public event Action DropBtnClickEvent;
        public event Action RotateClockwiseBtnClickEvent;
        public event Action MoveLeftBtnClickEvent;
        public event Action MoveRightBtnClickEvent;

        protected override void SetupScreen(UiViewTetrisGameScreen screen)
        {
            screen.UiNextTetromino.Initialize(_config.GridCell);

            screen.PauseBtn.onClick.AddListener(() => PauseBtnClickEvent?.Invoke());
            screen.DropBtn.onClick.AddListener(() => DropBtnClickEvent?.Invoke());
            screen.RotateClockwiseBtn.onClick.AddListener(() => RotateClockwiseBtnClickEvent?.Invoke());
            screen.MoveLeftBtn.onClick.AddListener(() => MoveLeftBtnClickEvent?.Invoke());
            screen.MoveRightBtn.onClick.AddListener(() => MoveRightBtnClickEvent?.Invoke());
        }

        public void SetNextTetromino(ShapeData value)
        {
            if (Screen == null)
                return;

            Screen.UiNextTetromino.Setup(value);
        }

        public void SetHiScore(int value) => UpdateScreen(value, panel => panel.HiScoresTComp);
        public void SetScore(int value) => UpdateScreen(value, panel => panel.ScoresTComp);
        public void SetSpeedLevel(int value) => UpdateScreen(value, panel => panel.SpeedTComp);
        public void SetComplexityLevel(int value) => UpdateScreen(value, panel => panel.LevelTComp);

        private void UpdateScreen(int value, Func<UiViewTetrisGameScreen, TMP_Text> getTComp)
        {
            if (Screen == null)
                return;

            getTComp(Screen).text = value.ToString();
        }
    }
}