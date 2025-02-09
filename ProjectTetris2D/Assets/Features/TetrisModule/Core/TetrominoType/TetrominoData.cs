using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.TetrominoType
{
    /// <summary>
    /// An abstract definition of a Tetromino
    /// </summary>
    public abstract class TetrominoData
    {
        public int ShapesCount => Shapes.Length;
        public abstract CellState Type { get; }
        protected abstract ShapeData[] Shapes { get; }

        public ShapeData GetShape(int rotateState) => Shapes[rotateState];
    }
}