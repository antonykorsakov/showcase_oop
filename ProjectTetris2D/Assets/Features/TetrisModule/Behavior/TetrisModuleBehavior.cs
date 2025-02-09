using Features.AppLifetimeModule.Core;
using Features.TetrisModule.Core.Controller;
using Features.UiViewMainScreenModule.Core.Controller;
using Features.UiViewTetrisGameScreenModule.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Features.TetrisModule.Behavior
{
    public sealed class TetrisModuleBehavior : IInitializable, ITickable
    {
        private readonly InputActionAsset _inputActionAsset;
        private InputAction _moveInputAction;

        [Inject] private IAppLifetimeController AppLifetimeController { get; }
        [Inject] private ITetrisManager TetrisManager { get; }
        [Inject] private ITetrisItemsController TetrisItemsController { get; }
        [Inject] private ITetrisSpeedController TetrisSpeedController { get; }
        [Inject] private IUiViewMainScreenController UiViewMainScreenController { get; }
        [Inject] private IUiViewTetrisGameScreenController UiViewTetrisGameScreenController { get; }

        public TetrisModuleBehavior(InputActionAsset inputActionAsset)
        {
            _inputActionAsset = inputActionAsset;
        }

        public void Initialize()
        {
            SubscribeMainScreenButtons();
            SubscribeInputControl();
            SubscribeGameControl();
        }

        public void Tick()
        {
            TetrisSpeedController.Tick(Time.deltaTime);
        }

        private void SubscribeMainScreenButtons()
        {
            UiViewMainScreenController.SpeedLevelDecreaseButtonClickEvent +=
                TetrisSpeedController.Level.DecreaseValue;
            UiViewMainScreenController.SpeedLevelIncreaseButtonClickEvent +=
                TetrisSpeedController.Level.IncreaseValue;

            UiViewMainScreenController.TetrominoLevelDecreaseButtonClickEvent +=
                TetrisItemsController.DifficultyLevel.DecreaseValue;
            UiViewMainScreenController.TetrominoLevelIncreaseButtonClickEvent +=
                TetrisItemsController.DifficultyLevel.IncreaseValue;
        }

        private void SubscribeInputControl()
        {
            _inputActionAsset.FindAction("Move").performed += context =>
            {
                Vector2 value = context.ReadValue<Vector2>();
                TetrisManager.GestureHandling(value);
            };

            UiViewTetrisGameScreenController.RotateClockwiseBtnClickEvent +=
                () => TetrisManager.GestureHandling(Vector2.up);
            UiViewTetrisGameScreenController.DropBtnClickEvent +=
                () => TetrisManager.GestureHandling(Vector2.down);
            UiViewTetrisGameScreenController.MoveLeftBtnClickEvent +=
                () => TetrisManager.GestureHandling(Vector2.left);
            UiViewTetrisGameScreenController.MoveRightBtnClickEvent +=
                () => TetrisManager.GestureHandling(Vector2.right);
        }

        private void SubscribeGameControl()
        {
            AppLifetimeController.AppStateChangedEvent += () =>
            {
                switch (AppLifetimeController.AppState)
                {
                    case AppState.OutOfGame:
                        _inputActionAsset.Disable();
                        break;

                    case AppState.PrepareGame:
                        _inputActionAsset.Enable();
                        TetrisItemsController.PrepareFirstTetromino();
                        TetrisManager.ReStartGame();
                        break;

                    case AppState.Game:
                        TetrisSpeedController.Resume();
                        break;

                    case AppState.GameOver:
                        TetrisSpeedController.Pause();
                        break;

                    case AppState.Pause:
                        TetrisSpeedController.Pause();
                        break;
                }
            };
        }
    }
}