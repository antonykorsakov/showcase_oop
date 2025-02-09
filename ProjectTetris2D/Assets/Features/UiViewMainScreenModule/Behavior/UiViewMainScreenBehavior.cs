using Features.TetrisModule.Core.Controller;
using Features.UiViewMainScreenModule.Core.Controller;
using Zenject;

namespace Features.UiViewMainScreenModule.Behavior
{
    public class UiViewMainScreenBehavior : IInitializable
    {
        [Inject] private ITetrisItemsController TetrisItemsController { get; }
        [Inject] private ITetrisSpeedController TetrisSpeedController { get; }
        [Inject] private IUiViewMainScreenController UiViewMainScreenController { get; }

        public void Initialize()
        {
            TetrisSpeedController.Level.ValueChangedEvent += SetSpeedLevel;
            TetrisItemsController.DifficultyLevel.ValueChangedEvent += SetTetrominoLevel;
            UiViewMainScreenController.ScreenEnabledEvent += SetScreen;
            SetScreen();
        }

        private void SetScreen()
        {
            SetSpeedLevel();
            SetTetrominoLevel();
        }

        private void SetSpeedLevel()
        {
            var value = TetrisSpeedController.Level.Value;
            UiViewMainScreenController.SetSpeedLevel(value);
        }

        private void SetTetrominoLevel()
        {
            var value = TetrisItemsController.DifficultyLevel.Value;
            UiViewMainScreenController.SetTetrominoLevel(value);
        }
    }
}