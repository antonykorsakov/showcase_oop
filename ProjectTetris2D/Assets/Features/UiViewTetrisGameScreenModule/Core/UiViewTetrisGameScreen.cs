using Features.Common.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UiViewTetrisGameScreenModule.Core
{
    public class UiViewTetrisGameScreen : UiViewScreen
    {
        [SerializeField] private TMP_Text _hiScoresTComp;
        [SerializeField] private TMP_Text _scoresTComp;
        [SerializeField] private TMP_Text _speedTComp;
        [SerializeField] private TMP_Text _levelTComp;

        [SerializeField] private Button _pauseBtn;
        [SerializeField] private Button _rotateClockwiseBtn;
        [SerializeField] private Button _dropBtn;
        [SerializeField] private Button _moveLeftBtn;
        [SerializeField] private Button _moveRightBtn;

        [SerializeField] private UiShapeGrid _uiNextTetromino;

        public TMP_Text HiScoresTComp => _hiScoresTComp;
        public TMP_Text ScoresTComp => _scoresTComp;
        public TMP_Text SpeedTComp => _speedTComp;
        public TMP_Text LevelTComp => _levelTComp;

        public Button PauseBtn => _pauseBtn;
        public Button RotateClockwiseBtn => _rotateClockwiseBtn;
        public Button DropBtn => _dropBtn;
        public Button MoveLeftBtn => _moveLeftBtn;
        public Button MoveRightBtn => _moveRightBtn;

        public UiShapeGrid UiNextTetromino => _uiNextTetromino;
    }
}