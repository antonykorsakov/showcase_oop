using System;
using Features.TetrisModule.Core.Data;

namespace Features.UiViewTetrisGameScreenModule.Core
{
    public interface IUiViewTetrisGameScreenController
    {
        UiViewTetrisGameScreen UiViewScreen { get; }

        event Action PauseBtnClickEvent;
        event Action DropBtnClickEvent;
        event Action RotateClockwiseBtnClickEvent;
        event Action MoveLeftBtnClickEvent;
        event Action MoveRightBtnClickEvent;

        void SetHiScore(int value);
        void SetScore(int value);
        void SetNextTetromino(ShapeData value);
        void SetSpeedLevel(int value);
        void SetComplexityLevel(int value);
    }
}