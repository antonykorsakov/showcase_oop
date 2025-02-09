using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Features.TetrisModule.Core.Data;

namespace Features.TetrisRendererModule.Core
{
    public interface ITetrisGridRendererController
    {
        UniTask EnableRendererAsync();
        void PrepareGridRenderer(int width, int height, IReadOnlyList<CellState> permittedStates);
        void UpdateGridRenderer(int width, int height, Func<int, int, CellState> getCellState);
    }
}