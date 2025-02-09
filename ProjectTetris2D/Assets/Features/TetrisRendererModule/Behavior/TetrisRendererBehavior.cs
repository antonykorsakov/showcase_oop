using Cysharp.Threading.Tasks;
using Features.TetrisModule.Core.Controller;
using Features.TetrisRendererModule.Core;
using Zenject;

namespace Features.TetrisRendererModule.Behavior
{
    public class TetrisRendererBehavior : IInitializable
    {
        [Inject] private ITetrisGridController TetrisGridController { get; }
        [Inject] private ITetrisItemsController TetrisItemsController { get; }
        [Inject] private ITetrisGridRendererController TetrisGridRendererController { get; }

        public void Initialize() => InitializeAsync().Forget();

        private async UniTaskVoid InitializeAsync()
        {
            await TetrisGridRendererController.EnableRendererAsync();
            TetrisGridController.GridClearedEvent += PrepareGridRenderer;
            TetrisGridController.GridChangedEvent += UpdateTetrisGridRenderer;
        }

        private void PrepareGridRenderer()
        {
            int w = TetrisGridController.Width;
            int h = TetrisGridController.Height;
            var permittedStates = TetrisItemsController.FallingTetrominoTypes;

            TetrisGridRendererController.PrepareGridRenderer(w, h, permittedStates);
        }

        private void UpdateTetrisGridRenderer()
        {
            int w = TetrisGridController.Width;
            int h = TetrisGridController.Height;

            TetrisGridRendererController.UpdateGridRenderer(w, h, TetrisGridController.GetCellState);
        }
    }
}